using Attract.Common.BaseResponse;
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
using static Attract.Common.DTOs.Product.AddProductDTO;

namespace Attract.Service.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHostingEnvironment hostingEnvironment;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper,IHostingEnvironment hostingEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.hostingEnvironment = hostingEnvironment;
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
        private string GetImagePath(string imageFileName)
        {
            return Path.Combine("wwwroot", "Images", imageFileName);
        }
        private string SanitizeDirectoryName(string input)
        {
            return input.Replace(" ", "_");
        }
        #endregion
    }
}
