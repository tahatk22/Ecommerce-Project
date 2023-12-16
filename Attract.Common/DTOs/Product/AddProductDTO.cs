using Attract.Common.DTOs.AvailableSize;
using Attract.Common.DTOs.Color;
using Attract.Common.DTOs.Image;
using Attract.Common.DTOs.Tag;
using Attract.Common.Validation;
using AttractDomain.Enums;
using Microsoft.AspNetCore.Http;
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
        public string Description { get; set; }
        public string Brand { get; set; }
        public bool IsArchived { get; set; } = false;
        public DiscountOption? DiscountOption { get; set; }
        public decimal? Discount { get; set; }
        public int? SubCategoryId { get; set; }
        public List<TagDTO> Tags { get; set; }
        public List<ProductQty> productQuantities { get; set; }
        //public class AddProductWithImageDTO
        //{
        //    public ColorDTO ColorDTO { get; set; }
        //    public AvailableSizeDTO AvailableSizeDTO { get; set; }
        //    public AddProductDTO ProductDTO { get; set; }
        //    public AddProductImageDTO ProductImageDTO { get; set; }
        //}
    }

    public class ProductQty
    {
        public ColorDTO Color { get; set; }
        public AvailableSizeDTO Size { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile ImageFile { get; set; }
    }
}
