using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.IService
{
    public interface IProductService
    {
        Task<BaseCommandResponse> GetAllSubCategoryProducts(int subCategoryId);
        Task<BaseCommandResponse> AddProduct(AddProductDTO addProductDTO);
    }
}
