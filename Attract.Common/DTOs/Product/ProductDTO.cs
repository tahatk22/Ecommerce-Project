﻿using Attract.Common.DTOs.AvailableSize;
using Attract.Common.DTOs.Color;
using AttractDomain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Attract.Common.DTOs.Product
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public bool IsArchived { get; set; }
        public string DiscountOption { get; set; }
        public int Discount { get; set; }
        public int SubCategoryId { get; set; }
        public List<ProductQuantityDTO> ProductQuantities { get; set; }
    }


    public class ProductQuantityDTO
    {
        public ColorDTO Color { get; set; }
        public SizeDTO Size { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class SizeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
