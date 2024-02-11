using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Slider;
using Attract.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Attract.API.Controllers.Slider
{
    [Route("api/[controller]")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        private readonly ISliderService sliderService;

        public SliderController(ISliderService sliderService)
        {
            this.sliderService = sliderService;
        }

        [HttpPost("AddSlider")]
        public async Task<ActionResult<BaseCommandResponse>> AddSlider(AddSliderDto addSliderDto)
        {
            var slider = await sliderService.AddSlider(addSliderDto);
            return Ok(slider);
        }

        [HttpPost("GetAllSlider")]
        public async Task<ActionResult<BaseCommandResponse>> GetAllSlider()
        {
            var countries = await sliderService.GetAllSlider();
            return Ok(countries);
        }
    }
}
