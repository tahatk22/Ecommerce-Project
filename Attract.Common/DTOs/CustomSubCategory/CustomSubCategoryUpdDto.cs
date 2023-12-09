﻿using Attract.Common.Validation;
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
        [Required]
        public int id { get; set; }
        [Required]
        public string categoryName { get; set; }
        [Required]
        public string subCategoryName { get; set; }
        [Required]
        public string title { get; set; }

        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png", ".gif" })]

        public IFormFile? imgNm { get; set; }
    }
}
