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
    public class CustomSubCategoryDto
    {
        public int Id { get; set; } 

        public string CategoryName { get; set; }

        public string SubCategoryName { get; set; }

        public string Title { get; set; }


        public string ImgNm { get; set; }
    }
}
