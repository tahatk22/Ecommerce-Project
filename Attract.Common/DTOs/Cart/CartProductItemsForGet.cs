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
        public int ProductQuantityId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ColorId { get; set; }
        public string ColorName { get; set; }
        public int AvailableSizeId { get; set; }
        public string AvailableSizeName { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
    }
}
