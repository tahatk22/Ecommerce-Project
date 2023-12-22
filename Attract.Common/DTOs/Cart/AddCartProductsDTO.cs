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
        public int CartId { get; set; }
        [Required]
        public int ProductQuantityId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
