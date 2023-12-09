using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Image;
using Attract.Common.DTOs.Product;
using Attract.Common.Helpers;
using Attract.Common.Helpers.ProductHelper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Attract.Common.DTOs.Product.AddProductDTO;

namespace Attract.Service.IService
{
    public interface IProductService
    {
        Task<BaseCommandResponse> GetAllProducts(ProductPagination productPagination);
        Task<BaseCommandResponse> AddProductImageAsync(AddProductWithImageDTO addProductImageDTO);
        Task<BaseCommandResponse> EditProductWithImageAsync(EditProductWithImageDTO editProductWithImagesDTO);
    }
}
