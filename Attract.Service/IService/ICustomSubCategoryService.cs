using Attract.Common.BaseResponse;
using Attract.Common.DTOs.CustomSubCategory;
using Attract.Common.DTOs.SubCategory;
using Attract.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.IService
{
    public interface ICustomSubCategoryService
    {
        Task<BaseCommandResponse> AddCustomSubCategory(CustomSubCategoryAddDto customSubCategoryDto);
        Task<BaseCommandResponse> GetAllCustomSubCategories();
    }
}
