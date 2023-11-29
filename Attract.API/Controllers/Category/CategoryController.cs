using Attract.Common.BaseResponse;
using Attract.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Attract.API.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICatgeoryService catgeoryService;

        public CategoryController(ICatgeoryService catgeoryService)
        {
            this.catgeoryService = catgeoryService;
        }

        [HttpGet("GetAllCategories")]
        public async Task<ActionResult<BaseCommandResponse>> GetAll()
        {
            return Ok(await catgeoryService.GetAllCategories());
        }
        [HttpGet("GetCategory/{ctgryId}")]
        public async Task<ActionResult<BaseCommandResponse>> GetCategory(int ctgryId)
        {
            return Ok(await catgeoryService.GetCategory(ctgryId));
        }
    }
}
