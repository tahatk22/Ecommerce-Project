using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Color;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.IService
{
    public interface IColorService
    {
        Task<IEnumerable<ColorDTO>> GetColors();
        Task<int> AddColor(AddColorDTO viewModel);
        Task<BaseCommandResponse> UpdateColor(UpdateColorDTO viewModel);
        Task<BaseCommandResponse> DeleteColor(int id);
        Task<BaseCommandResponse> AddColorRange(List<AddColorDTO> addColorDTOs);
    }
}
