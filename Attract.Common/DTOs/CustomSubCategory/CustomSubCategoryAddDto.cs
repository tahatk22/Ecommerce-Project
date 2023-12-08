using Attract.Common.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Common.DTOs.CustomSubCategory
{
    public class CustomSubCategoryAddDto
    {
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string SubCategoryName { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png", ".gif" })]

        public IFormFile ImgNm { get; set; }
    }
}
