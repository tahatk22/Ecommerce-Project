using Attract.Common.DTOs.Category;
using Attract.Common.DTOs.SubCategory;
using AttractDomain.Entities.Attract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Attract.Common.DTOs
{
    public class CategoryDto:CategoryAddDto
    {
        public int Id { get; set; }
        public IList<SubCategoryDto> SubCategories { get; set; }
    }
}
