using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Image;
using Attract.Common.DTOs.Product;
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
        Task<BaseCommandResponse> GetAllSubCategoryProducts(int subCategoryId);
        Task<BaseCommandResponse> AddProductImageAsync(AddProductWithImageDTO addProductImageDTO);
        Task<BaseCommandResponse> EditProductWithImageAsync(EditProductWithImageDTO editProductWithImagesDTO);
    }
}
