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
    public class CategoryDto:categoryAddDto
    {
        public int Id { get; set; }
        public IList<string> SubCategories { get; set; }
    }
}
