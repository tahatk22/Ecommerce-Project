using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Slider;
using Attract.Service.IService;
using Attract.Service.Service;
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

        [HttpPost("setSliderVal")]
        public async Task<ActionResult<BaseCommandResponse>> SetSliderVal(bool value)
        {
            var slider = await sliderService.SetSliderVal(value);
            return Ok(slider);
        }
        [HttpGet("GetCurrentSliderValue")]
        public async Task<ActionResult<BaseCommandResponse>> GetCurrentSliderValue()
        {
            return Ok(await sliderService.GetCurrentSliderValue());
        }
        [HttpPut("UpdateSlider")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateSlider(UpdateSliderDto updateSliderDto)
        {
            var UpdatedSlider = await sliderService.UpdateSlider(updateSliderDto);
            return Ok(UpdatedSlider);
        }
        [HttpDelete("DeleteSlider/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> DeleteSlider(int id)
        {
            var DeletedSlider = await sliderService.DeleteSlider(id);
            return Ok(DeletedSlider);
        }
    }
}
