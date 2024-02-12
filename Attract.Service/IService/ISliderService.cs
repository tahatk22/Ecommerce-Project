using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Country;
using Attract.Common.DTOs.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.IService
{
    public interface ISliderService
    {
        Task<BaseCommandResponse> AddSlider(AddSliderDto addSliderDto);
        Task<BaseCommandResponse> GetAllSlider();
        Task<BaseCommandResponse> SetSliderVal(bool sliderValue);
    }
}
