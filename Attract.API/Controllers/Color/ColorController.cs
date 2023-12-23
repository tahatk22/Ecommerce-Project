using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Color;
using Attract.Service.IService;
using Attract.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Attract.API.Controllers.Color
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<BaseCommandResponse>> AddColor(AddColorDTO viewModel)
        {
            return Ok(await _colorService.AddColor(viewModel));
        }
        [HttpGet("Get")]
        public async Task<ActionResult<BaseCommandResponse>> GetAll()
        {
            return Ok(await _colorService.GetColors());
        }
        [HttpPut("Update")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateColor(UpdateColorDTO viewModel)
        {
            return Ok(await _colorService.UpdateColor(viewModel));
        }
        [HttpDelete("Del/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> DelColor(int Id)
        {
            return Ok(await _colorService.DeleteColor(Id));
        }
    }
}
