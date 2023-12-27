using Attract.Common.BaseResponse;
using Attract.Common.DTOs.AvailableSize;
using Attract.Common.DTOs.Category;
using Attract.Common.DTOs.Color;
using Attract.Common.DTOs.Product;
using Attract.Common.DTOs.Tag;
using Attract.Common.Helpers;
using Attract.Common.Helpers.ProductHelper;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Attract.Service.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IAuthService authService;

        public ProductService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHostingEnvironment hostingEnvironment,
            IHttpContextAccessor httpContextAccessor,
            IAuthService authService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.hostingEnvironment = hostingEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.authService = authService;
        }
        public async Task<BaseCommandResponse> DeleteProduct(int id)
        {
            var response = new BaseCommandResponse();
            var product = await unitOfWork.GetRepository<Product>().GetFirstOrDefaultAsync(predicate: s => s.Id == id);
            if (product != null)
            {
                unitOfWork.GetRepository<Product>().Delete(product);
                await unitOfWork.SaveChangesAsync();
                response.Success = true;
                response.Message = "Deleted Successfully!";
                return response;
            }
            response.Success = false;
            response.Message = "Failed to delete the product!";
            return response;
        }
        public async Task<BaseCommandResponse> AddProduct(AddProductDTO viewModel)
        {
            var response = new BaseCommandResponse();

            try
            {
                var newProduct = await AddProductAsync(viewModel);
                AddProductTags(newProduct.Id, viewModel.tags);
                await CreateProductDirectoryAsync(newProduct);

                
                await AddProductQuantities(newProduct.Id, viewModel);
                await unitOfWork.SaveChangesAsync();

                response.Success = true;
                response.Data = newProduct.Id;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while adding the product and images.";
            }

            return response;
        }
        public async Task<BaseCommandResponse> GetAllProducts(ProductPagination productPagination)
        {
            var response = new BaseCommandResponse();
            string userId = authService.GetCurrentUserId();
            IQueryable<Product> products;
            try
            {
                products = unitOfWork.GetRepository<Product>().GetAll()
                    .Include(s => s.ProductQuantities).ThenInclude(x => x.Color)
                   .Include(s => s.ProductQuantities).ThenInclude(x => x.AvailableSize);

                if (products == null || !products.Any())
                {
                    response.Success = false;
                    response.Message = "Not Found";
                    return response;
                }
                products = await filterProducts(productPagination, products);

                var result = mapper.Map<IList<ProductDTO>>(products);
                var hostValue = httpContextAccessor.HttpContext.Request.Host.Value;

                foreach (var product in result.SelectMany(product => product.ProductQuantities))
                {
                        //Update each ImageDTO in the collection
                        var imageUrl = $"https://{hostValue}/Images/Product/{product.ImageName}";
                        product.Image = imageUrl;
                }
                response.Success = true;
                response.Data = new { Products = result, ProductCount = result.Count };
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
        public async Task<BaseCommandResponse> EditProduct(EditProductDTO viewModel)
        {
            var response = new BaseCommandResponse();

            try
            {
                // Update the product
                var updatedProduct = await UpdateProductAsync(viewModel);

                // Update the product images
                await EditProductQuantities(viewModel.Id, viewModel.ProductQuantities);
                await EditProductTags(viewModel.Id, viewModel.Tags);

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

        private async Task AddProductTags(int productId, List<TagDTO> tags)
        {
            foreach (var tag in tags)
            {
                var productTag = new ProductTag
                {
                    ProductId = productId,
                    TagId = tag.Id
                };
                await unitOfWork.GetRepository<ProductTag>().InsertAsync(productTag);
            }
        }
        private async Task<AvailableSize> GetOrCreateSizeAsync(string size)
        {
            var existingSize = await unitOfWork.GetRepository<AvailableSize>().GetFirstOrDefaultAsync(predicate: c => c.Name == size);

            if (existingSize != null)
            {
                return existingSize;
            }

            var newSize = new AvailableSize { Name = size };
            await unitOfWork.GetRepository<AvailableSize>().InsertAsync(newSize);
            await unitOfWork.SaveChangesAsync();

            return newSize;
        }
        private async Task<Color> GetOrCreateColorAsync(string colorName)
        {
            var existingColor = await unitOfWork.GetRepository<Color>().GetFirstOrDefaultAsync(predicate: c => c.Name == colorName);

            if (existingColor != null)
            {
                return existingColor;
            }

            var newColor = new Color { Name = colorName };
            await unitOfWork.GetRepository<Color>().InsertAsync(newColor);
            await unitOfWork.SaveChangesAsync();

            return newColor;
        }
        private async Task CreateProductDirectoryAsync(Product product)
        {
            var productDirectoryPath = Path.Combine("wwwroot", "Images", "Product");
            if (!Directory.Exists(productDirectoryPath))
            {
                Directory.CreateDirectory(productDirectoryPath);
            }
        }

        private async Task AddProductQuantities(int productId, AddProductDTO product)
        {
            foreach (var item in product.productQuantities)
            {
                var productDirectoryPath = GetProductDirectoryPath();
                var imagePath = Path.Combine(productDirectoryPath, item.Image.FileName);
                // Save the image file
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await item.Image.CopyToAsync(fileStream);
                }
                var productQuantity = new ProductQuantity
                {
                    ProductId = productId,
                    AvailableSizeId = item.Size.Id,
                    ColorId = item.Color.Id,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    ImageName = item.Image.FileName
                };
                await unitOfWork.GetRepository<ProductQuantity>().InsertAsync(productQuantity);
            }            
        }

        private async Task EditProductQuantities(int productId, List<EditProductQty> productQuantities)
        {
            var existingQuantities = await unitOfWork.GetRepository<ProductQuantity>().GetAllAsync(predicate: s => s.Id == productId);

            foreach (var item in existingQuantities)
            {
                if (!productQuantities.Select(x => x.Id).Contains(item.Id))
                {
                    unitOfWork.GetRepository<ProductQuantity>().Delete(item.Id);
                }
            }
            foreach (var item in productQuantities.Where(x => x.Id == 0))
            {
                var productQuantity = new ProductQuantity
                {
                    ProductId = productId,
                    ColorId = item.ColorId,
                    AvailableSizeId = item.AvailableSizeId,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
                await unitOfWork.GetRepository<ProductQuantity>().InsertAsync(productQuantity);
            }

            foreach (var item in productQuantities)
            {
                //var productDirectoryPath = GetProductDirectoryPath();
                //var imagePath = Path.Combine(productDirectoryPath, item.ImageFile.FileName);
                //// Save the image file
                //using (var fileStream = new FileStream(imagePath, FileMode.Create))
                //{
                //    await item.ImageFile.CopyToAsync(fileStream);
                //}
                var existingProduct = await unitOfWork.GetRepository<ProductQuantity>().GetFirstOrDefaultAsync(predicate: s => s.Id == item.Id);

                if (existingProduct == null)
                {
                    // Handle case where the product to update is not found
                    throw new NotFoundException("Product not found");
                }

                // Update the existing product with new data
                mapper.Map(item, existingProduct);
                existingProduct.ProductId = productId;
                //existingProduct.ImageName = Path.GetFullPath(item.ImageFile.FileName);

                // Mark the product entity as modified (if necessary)
                unitOfWork.GetRepository<ProductQuantity>().UpdateAsync(existingProduct);
            }
        }

        private async Task EditProductTags(int productId, List<TagDTO> productTags)
        {
            var existingTags = await unitOfWork.GetRepository<ProductTag>().GetAllAsync(predicate: s => s.Id == productId);
            var incomingTagsId = productTags.Select(x => x.Id);
            foreach (var item in existingTags)
            {
                if (!incomingTagsId.Contains(item.TagId.Value))
                {
                    unitOfWork.GetRepository<ProductTag>().Delete(item.Id);
                }
            }
            foreach (var item in incomingTagsId)
            {
                if (!existingTags.Select(x => x.TagId).Contains(item))
                {
                    var productTag = new ProductTag
                    {
                        ProductId = productId,
                        TagId = item
                    };
                    await unitOfWork.GetRepository<ProductTag>().InsertAsync(productTag);
                }
            }
        }

        private string GetProductDirectoryPath()
        {
            return Path.Combine("wwwroot", "Images", "Product");
        }

        private string SanitizeDirectoryName(string input)
        {
            return input.Replace(" ", "_");
        }
        private async Task<Product> UpdateProductAsync(EditProductDTO productDTO)
        {
            var existingProduct = await unitOfWork.GetRepository<Product>().GetFirstOrDefaultAsync(predicate: s => s.Id == productDTO.Id);

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
                .GetAllAsync(predicate: s => s.ProductId == product.Id);

            // Determine which images need to be deleted
            var imagesToDelete = existingImages
                .Where(existingImage => !newImageFiles.Any(newImage => newImage.FileName == existingImage.ImageFileName))
                .ToList();

            // Delete images that are no longer associated with the product
            foreach (var imageToDelete in imagesToDelete)
            {
                var imagePathToDelete = Path.Combine(GetProductDirectoryPath(), imageToDelete.ImageFileName);

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
                var imagePath = Path.Combine(GetProductDirectoryPath(), newImageFile.FileName);

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await newImageFile.CopyToAsync(fileStream);
                }
            }
        }
        private async Task<IQueryable<Product>> filterProducts(ProductPagination PagingParams, IQueryable<Product> products)
        {
            if (!string.IsNullOrEmpty(PagingParams.SearchString))
            {
                products = products.Where(s => s.Name.ToLower().Contains(PagingParams.SearchString.ToLower()) ||
                s.Description.ToLower().Contains(PagingParams.SearchString.ToLower()) ||
                /*s.Price.ToString().Contains(PagingParams.SearchString) ||*/ s.Brand.Contains(PagingParams.SearchString));

            }
            return products;
        }
        #endregion
    }
}
