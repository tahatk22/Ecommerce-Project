using Attract.Common.DTOs.AvailableSize;
using Attract.Common.DTOs.Color;
using Attract.Common.DTOs.Image;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Common.DTOs.Product
{
    public class AddProductDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int? SubCategoryId { get; set; }

        public class AddProductWithImageDTO
        {
            public ColorDTO ColorDTO { get; set; }
            public AvailableSizeDTO AvailableSizeDTO { get; set; }
            public AddProductDTO ProductDTO { get; set; }
            public AddProductImageDTO ProductImageDTO { get; set; }
        }
    }
}
