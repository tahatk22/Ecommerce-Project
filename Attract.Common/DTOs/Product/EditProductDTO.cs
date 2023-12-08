using Attract.Common.DTOs.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Common.DTOs.Product
{
    public class EditProductDTO
    {
        public int Id { get; set; }  
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int? SubCategoryId { get; set; }
    }
    public class EditProductWithImageDTO
    {
        public EditProductDTO ProductDTO { get; set; }
        public AddProductImageDTO ProductImageDTO { get; set; }
    }
}
