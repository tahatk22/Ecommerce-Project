using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Attract.Common.DTOs.Cart
{
    public class CartProductItemsForGet
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductColorId { get; set; }
        public string ProductColorName { get; set; }
        public int ProductAvailableSizeId { get; set; }
        public string ProductAvailableSizeName { get; set; }
        public int Quantity { get; set; }
    }
}
