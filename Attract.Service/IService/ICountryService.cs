using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Country;
using Attract.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.IService
{
    public interface ICountryService
    {
        Task<BaseCommandResponse> AddCountry(AddCountryDTO addCountryDTO);
        Task<BaseCommandResponse> GetAllCountries();
    }
}
