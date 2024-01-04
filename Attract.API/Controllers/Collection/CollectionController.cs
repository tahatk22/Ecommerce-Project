using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Collection;
using Attract.Common.Helpers;
using Attract.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Attract.API.Controllers.Collection
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionService collectionService;

        public CollectionController(ICollectionService collectionService)
        {
            this.collectionService = collectionService;
        }

        [HttpPost("AddCollection")]
        public async Task<ActionResult<BaseCommandResponse>> AddCollection(AddCollectionDTO addCollectionDTO)
        {
            var response=await collectionService.AddCollection(addCollectionDTO);
            return Ok(response);
        }

        [HttpGet("GetAllCollections")]
        public async Task<ActionResult<BaseCommandResponse>> GetAllCollections([FromQuery]PagingParams pagingParams)
        {
            var response = await collectionService.GetAllCollections(pagingParams);
            return Ok(response);
        }
    }
}
