using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Common.DTOs.Country
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //[AllowedExtensions(new[] { ".jpg", ".jpeg", ".png", ".gif" })]
        public string CountryFlag { get; set; }
    }
}
