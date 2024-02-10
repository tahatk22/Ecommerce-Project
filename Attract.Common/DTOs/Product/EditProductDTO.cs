using Attract.Common.DTOs.Tag;
using Attract.Common.Validation;
using AttractDomain.Enums;
using Microsoft.AspNetCore.Http;
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
        public string Description { get; set; }
        public string Brand { get; set; }
        public bool IsArchived { get; set; }
        public DiscountOption? DiscountOption { get; set; }
        public decimal Discount { get; set; }
        public ProductTypeOption ProductTypeOption { get; set; }
        public int? SubCategoryId { get; set; }

        public List<TagDTO> Tags { get; set; }
        public List<EditProductQty> ProductQuantities { get; set; }
    }
    public class EditProductQty
    {
        public int Id { get; set; }
        public int ColorId { get; set; }
        public int AvailableSizeId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile Image { get; set; }
    }
}
