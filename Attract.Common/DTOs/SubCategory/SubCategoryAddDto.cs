using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Common.DTOs.SubCategory
{
    public class SubCategoryAddDto
    {
        public string SubCategoryName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
