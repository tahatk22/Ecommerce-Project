using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Product;
using Attract.Common.Helpers.ProductHelper;

namespace Attract.Service.IService
{
    public interface IProductService
    {
        Task<BaseCommandResponse> GetAllProducts(ProductPagination productPagination);
        Task<BaseCommandResponse> DeleteProduct(int id);
        Task<BaseCommandResponse> AddProduct(AddProductDTO addProductImageDTO);
        Task<BaseCommandResponse> EditProductWithImageAsync(EditProductWithImageDTO editProductWithImagesDTO);
    }
}
