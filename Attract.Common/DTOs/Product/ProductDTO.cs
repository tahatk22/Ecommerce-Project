using Attract.Common.DTOs.AvailableSize;
using Attract.Common.DTOs.Color;
using Attract.Common.DTOs.Image;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Attract.Common.DTOs.Product
{
    public class ProductDTO:AddProductDTO
    {
        public int Id { get; set; }
        [Required]
        public IList<string> AvailableSizes { get; set; } 
        public List<string> Colors { get; set; }
        public List<string> ImagePaths { get; set; }

    }
}
