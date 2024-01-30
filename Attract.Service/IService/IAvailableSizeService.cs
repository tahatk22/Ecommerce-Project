using Attract.Common.BaseResponse;
using Attract.Common.DTOs.AvailableSize;
using Attract.Common.DTOs.Color;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.IService
{
    public interface IAvailableSizeService
    {
        Task<BaseCommandResponse> GetAvailableSizes();
        Task<int> AddAvailableSize(AddAvailableSizeDTO viewModel);
        Task<BaseCommandResponse> UpdateAvailableSize(UpdateAvailableSizeDTO viewModel);
        Task <BaseCommandResponse> DeleteAvailableSize(int id);
    }
}
