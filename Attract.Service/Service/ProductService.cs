using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Category;
using Attract.Common.DTOs.Image;
using Attract.Common.DTOs.Product;
using Attract.Domain.Entities.Attract;
using Attract.Framework.UoW;
using Attract.Service.IService;
using AttractDomain.Entities.Attract;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OpenQA.Selenium;
using static Attract.Common.DTOs.Product.AddProductDTO;

namespace Attract.Service.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IAuthService authService;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper,IHostingEnvironment hostingEnvironment,IAuthService authService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.hostingEnvironment = hostingEnvironment;
            this.authService = authService;
        }
        public async Task<BaseCommandResponse> AddProductImageAsync(AddProductWithImageDTO addProductWithImagesDTO)
        {
            var response = new BaseCommandResponse();

            try
            {
                // Map and insert the product
                var newProduct = await AddProductAsync(addProductWithImagesDTO.ProductDTO);

                // Create a directory for the product if it doesn't exist
                await CreateProductDirectoryAsync(newProduct);

                // Map and insert the product images with the correct product ID
                await AddProductImagesAsync(newProduct, addProductWithImagesDTO.ProductImageDTO.ImageFiles);

                // Save changes to the database
                await unitOfWork.SaveChangesAsync();

                response.Success = true;
                response.Data = newProduct.Id;  // You can return the ID of the newly created product
            }
            catch (Exception ex)
            {
                // Handle exceptions, log them, etc.
                response.Success = false;
                response.Message = "An error occurred while adding the product and images.";
                // Optionally log the exception details
                // logger.LogError(ex, "Error adding product and images");
            }

            return response;
        }
        public async Task<BaseCommandResponse> GetAllSubCategoryProducts(int subCategoryId)
        {
            var response = new BaseCommandResponse();
            var userId = authService.GetCurrentUserId();
            try
            {
                var products = await unitOfWork.GetRepository<Product>()
                    .GetAllAsync(
                        s => s.SubCategoryId == subCategoryId,
                        include: s => s
                            .Include(p => p.ProductAvailableSizes)
                            .ThenInclude(pas => pas.AvailableSize)
                            .Include(p => p.ProductColors)
                            .ThenInclude(pc => pc.Color)
                            .Include(p=>p.OrderDetails)
                            .Include(w=>w.Images)
                    );

                if (products == null || !products.Any())
                {
                    response.Success = false;
                    response.Message = "Not Found";
                    return response;
                }
                var result = mapper.Map<IList<ProductDTO>>(products);
                response.Success = true;
                response.Data = result;

                return response;
            }
            catch (Exception ex)
            {
                // Handle exceptions and log if necessary
                response.Success = false;
                response.Message = "An error occurred while fetching products.";
                return response;
            }
        }
        public async Task<BaseCommandResponse> EditProductWithImageAsync(EditProductWithImageDTO editProductWithImagesDTO)
        {
            var response = new BaseCommandResponse();

            try
            {
                // Update the product
                var updatedProduct = await UpdateProductAsync(editProductWithImagesDTO.ProductDTO);

                // Update the product images
                await UpdateProductImagesAsync(updatedProduct, editProductWithImagesDTO.ProductImageDTO.ImageFiles);

                // Save changes to the database
                await unitOfWork.SaveChangesAsync();

                response.Success = true;
                response.Data = updatedProduct.Id;
            }
            catch (Exception ex)
            {
                // Handle exceptions, log them, etc.
                response.Success = false;
                response.Message = "An error occurred while editing the product and images.";
                // Optionally log the exception details
                // logger.LogError(ex, "Error editing product and images");
            }

            return response;
        }

        #region private methods
        private async Task<Product> AddProductAsync(AddProductDTO productDTO)
        {
            var newProduct = mapper.Map<Product>(productDTO);
            await unitOfWork.GetRepository<Product>().InsertAsync(newProduct);
            await unitOfWork.SaveChangesAsync();
            return newProduct;
        }

        private async Task CreateProductDirectoryAsync(Product product)
        {
            var productDirectoryName = SanitizeDirectoryName(product.Name);
            var productDirectoryPath = Path.Combine("wwwroot", "Images", productDirectoryName);
            if (!Directory.Exists(productDirectoryPath))
            {
                Directory.CreateDirectory(productDirectoryPath);
            }
        }

        private async Task AddProductImagesAsync(Product product, List<IFormFile> imageFiles)
        {
            foreach (var imageFile in imageFiles)
            {
                var productImage = new ProductImage
                {
                    Name = Path.GetFullPath(imageFile.FileName),
                    ProductId = product.Id,  // Use the newly created product's ID
                    ImageFileName = imageFile.FileName
                };

                await unitOfWork.GetRepository<ProductImage>().InsertAsync(productImage);

                var productDirectoryPath = GetProductDirectoryPath(product);
                var imagePath = Path.Combine(productDirectoryPath, imageFile.FileName);

                // Save the image file
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
            }
        }

        private string GetProductDirectoryPath(Product product)
        {
            return Path.Combine("wwwroot", "Images", SanitizeDirectoryName(product.Name));
        }

        private string SanitizeDirectoryName(string input)
        {
            return input.Replace(" ", "_");
        }
        private string GetImagePath(string imageFileName)
        {
            return Path.Combine("wwwroot", "Images", imageFileName);
        }
        private async Task<Product> UpdateProductAsync(EditProductDTO productDTO)
        {
            var existingProduct = await unitOfWork.GetRepository<Product>().GetFirstOrDefaultAsync(predicate:s=>s.Id==productDTO.Id);

            if (existingProduct == null)
            {
                // Handle case where the product to update is not found
                throw new NotFoundException("Product not found");
            }

            // Update the existing product with new data
            mapper.Map(productDTO, existingProduct);

            // Mark the product entity as modified (if necessary)
             unitOfWork.GetRepository<Product>().UpdateAsync(existingProduct);

            // No need to save changes immediately; just return the updated product
            return existingProduct;
        }
        private async Task UpdateProductImagesAsync(Product product, List<IFormFile> newImageFiles)
        {
            // Get existing images for the product
            var existingImages = await unitOfWork.GetRepository<ProductImage>()
                .GetAllAsync(predicate:s=>s.ProductId==product.Id);

            // Determine which images need to be deleted
            var imagesToDelete = existingImages
                .Where(existingImage => !newImageFiles.Any(newImage => newImage.FileName == existingImage.ImageFileName))
                .ToList();

            // Delete images that are no longer associated with the product
            foreach (var imageToDelete in imagesToDelete)
            {
                var imagePathToDelete = Path.Combine(GetProductDirectoryPath(product), imageToDelete.ImageFileName);

                // Delete the image file
                if (File.Exists(imagePathToDelete))
                {
                    File.Delete(imagePathToDelete);
                }

                // Delete the image record from the database
                unitOfWork.GetRepository<ProductImage>().Delete(imageToDelete);
            }

            // Add or update the new images
            foreach (var newImageFile in newImageFiles)
            {
                var productImage = existingImages.FirstOrDefault(pi => pi.ImageFileName == newImageFile.FileName);

                if (productImage == null)
                {
                    // Image is new, create a new record
                    productImage = new ProductImage
                    {
                        ProductId = product.Id,
                        ImageFileName = newImageFile.FileName,
                        // Other properties if needed
                    };

                    await unitOfWork.GetRepository<ProductImage>().InsertAsync(productImage);
                }
                else
                {
                    // Image already exists, update properties if needed
                    productImage.ModifyOn = DateTime.UtcNow;
                }

                // Save the image file or update it
                var imagePath = Path.Combine(GetProductDirectoryPath(product), newImageFile.FileName);

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await newImageFile.CopyToAsync(fileStream);
                }
            }
        }
        #endregion
    }
}
