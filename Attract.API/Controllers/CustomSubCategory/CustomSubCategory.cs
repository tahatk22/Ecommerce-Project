using Attract.Common.BaseResponse;
using Attract.Common.DTOs.CustomSubCategory;
using Attract.Common.DTOs.SubCategory;
using Attract.Service.IService;
using Attract.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Attract.API.Controllers.CustomSubCategory
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomSubCategory : ControllerBase
    {
        private readonly ICustomSubCategoryService customSubCategoryService;
        public CustomSubCategory(ICustomSubCategoryService customSubCategoryService)
        {
            this.customSubCategoryService = customSubCategoryService;
        }
        [HttpPost("AddCustomSubCtgry")]
        public async Task<ActionResult<BaseCommandResponse>> AddCustomSubCtgry([FromForm] CustomSubCategoryAddDto customSubCategoryDto)
        {
            return Ok(await customSubCategoryService.AddCustomSubCategory(customSubCategoryDto));
        }
        [HttpGet("GetAllCustomSubCategories")]
        public async Task<ActionResult<BaseCommandResponse>> GetAll()
        {
            return Ok(await customSubCategoryService.GetAllCustomSubCategories());
        }
    }
}
