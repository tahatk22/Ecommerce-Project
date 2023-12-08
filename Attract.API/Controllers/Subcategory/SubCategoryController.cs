using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Category;
using Attract.Common.DTOs.SubCategory;
using Attract.Service.IService;
using Attract.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Attract.API.Controllers.Subcategory
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService subCategoryService;
        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            this.subCategoryService = subCategoryService;  
        }
        [HttpGet("GetAllSubCategories")]
        public async Task<ActionResult<BaseCommandResponse>> GetAll()
        {
            return Ok(await subCategoryService.GetAllSubCategories());
        }
        [HttpGet("GetSubCategory/{subCtgryId}")]
        public async Task<ActionResult<BaseCommandResponse>> GetSubCategory(int subCtgryId)
        {
            return Ok(await subCategoryService.GetSubCategory(subCtgryId));
        }
        [HttpPost("AddSubCtgry")]
        public async Task<ActionResult<BaseCommandResponse>> AddSubCtgry(SubCategoryAddDto subCategoryAddDto)
        {
            return Ok(await subCategoryService.AddSubCategory(subCategoryAddDto));
        }
        [HttpPut("UpdSubCtgry")]
        public async Task<ActionResult<BaseCommandResponse>> UpdSubCtgry(SubCategoryUpdDto subCategoryUpdDto)
        {
            return Ok(await subCategoryService.UpdSubCategory(subCategoryUpdDto));
        }

    }
}
