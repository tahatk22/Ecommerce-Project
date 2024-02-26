using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Country;
using Attract.Common.Helpers;
using Attract.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Attract.API.Controllers.Country
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService countryService;

        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpPost("AddCountry")]
        public async Task<ActionResult<BaseCommandResponse>>AddCountry(AddCountryDTO addCountryDTO)
        {
            var country=await countryService.AddCountry(addCountryDTO);
            return Ok(country);
        }

        [HttpPost("GetAllCountries")]
        public async Task<ActionResult<BaseCommandResponse>> GetAllCountries()
        {
            var countries = await countryService.GetAllCountries();
            return Ok(countries);
        }
        [HttpPut("UpdateCountry")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateCountry(UpdateCountryDto updateCountryDto)
        {
            var UpdatedCountry = await countryService.UpdateCountry(updateCountryDto);
            return Ok(UpdatedCountry);
        }
        [HttpDelete("DeleteCountry/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> DeleteCountry(int id)
        {
            var DeletedCountry = await countryService.DeleteCountry(id);
            return Ok(DeletedCountry);
        }
    }
}
