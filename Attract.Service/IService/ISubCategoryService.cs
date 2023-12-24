using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Category;
using Attract.Common.DTOs.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.IService
{
    public interface ISubCategoryService
    {
        Task<BaseCommandResponse> GetAllSubCategories();
        Task<BaseCommandResponse> GetSubCategory(int subCategoryId);
        Task<BaseCommandResponse> AddSubCategory(SubCategoryAddDto subCategoryAddDto);
        Task<BaseCommandResponse> UpdSubCategory(SubCategoryUpdDto subCategoryUpdDto);
        Task<BaseCommandResponse> DeleteSubCategory(int id);

    }
}
