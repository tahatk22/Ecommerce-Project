using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Common.DTOs.Slider
{
    public class SliderDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        //[AllowedExtensions(new[] { ".jpg", ".jpeg", ".png", ".gif" })]
        public string Image { get; set; }
    }
}
