using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Attract.Common.DTOs.Cart
{
    public class AddCartProductsDTO
    {
        [JsonIgnore]
        public int? Id { get; set; }
        [JsonIgnore]
        public int CartId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int ProductColorId { get; set; }
        [Required]
        public int ProductAvailableSizeId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
