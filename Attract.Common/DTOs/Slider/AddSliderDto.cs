using Attract.Common.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Common.DTOs.Slider
{
    public class AddSliderDto
    {
        public string Title { get; set; }

        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png", ".gif" })]
        public IFormFile Image { get; set; }
    }
    public class UpdateSliderDto : AddSliderDto
    {
        public int Id { get; set; }
    }
}
