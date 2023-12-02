using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Common.DTOs.Image
{
    public class ProductImageDTO
    {
        public List<IFormFile> ImageFiles { get; set; }

    }
}
