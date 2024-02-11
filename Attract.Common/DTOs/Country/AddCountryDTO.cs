using Attract.Common.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Common.DTOs.Country
{
    public class AddCountryDTO
    {
        public string Name { get; set; }

        //[AllowedExtensions(new[] { ".jpg", ".jpeg", ".png", ".gif" })]
        public IFormFile CountryFlag { get; set; }
    }

    public class CountryDTO:AddCountryDTO
    {
        public int Id { get; set; }
    }
}
