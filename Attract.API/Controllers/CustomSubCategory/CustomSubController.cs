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
        [HttpPut("UpdCustomSubCtgry")]
        public async Task<ActionResult<BaseCommandResponse>> UpdCustomSubCtgry([FromForm] CustomSubCategoryUpdDto customSubCategoryUpdDto)
        {
            return Ok(await customSubCategoryService.UpdCustomSubCategory(customSubCategoryUpdDto));
        }
        [HttpDelete("DelCustomSubCtgry/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> DelCustomSubCtgry(int id)
        {
            return Ok(await customSubCategoryService.DeleteCustomSubCategory(id));
        }
        [HttpGet("GetCustomSubCategory/{customSubCtgryId}")]
        public async Task<ActionResult<BaseCommandResponse>> GetCustomSubCategory(int customSubCtgryId)
        {
            return Ok(await customSubCategoryService.GetCustomSubCategory(customSubCtgryId));
        }
    }
}
