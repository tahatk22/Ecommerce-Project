using Attract.Common.BaseResponse;
using Attract.Common.DTOs.AvailableSize;
using Attract.Service.IService;
using Attract.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Attract.API.Controllers.AvailableSize
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailableSizeController : ControllerBase
    {
        private readonly IAvailableSizeService _AvailableSizeService;

        public AvailableSizeController(IAvailableSizeService AvailableSizeService)
        {
            _AvailableSizeService = AvailableSizeService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<BaseCommandResponse>> AddAvailableSize(AddAvailableSizeDTO viewModel)
        {
            return Ok(await _AvailableSizeService.AddAvailableSize(viewModel));
        }
        [HttpGet("Get")]
        public async Task<ActionResult<BaseCommandResponse>> GetAll()
        {
            return Ok(await _AvailableSizeService.GetAvailableSizes());
        }
        [HttpPut("Update")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateAvailableSize(UpdateAvailableSizeDTO viewModel)
        {
            return Ok(await _AvailableSizeService.UpdateAvailableSize(viewModel));
        }
        [HttpDelete("Del/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> DelAvailableSize(int Id)
        {
            return Ok(await _AvailableSizeService.DeleteAvailableSize(Id));
        }
    }
}
