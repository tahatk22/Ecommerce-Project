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
    public class CustomSubCategoryUpdDto
    {

        public int? id { get; set; }

        public string? categoryName { get; set; }

        public string? subCategoryName { get; set; }

        public string? title { get; set; }

        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png", ".gif" })]

        public IFormFile? imgNm { get; set; }
    }
}
