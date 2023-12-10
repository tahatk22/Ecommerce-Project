using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Attract.Common.DTOs.Cart
{
    public class UpdateCartProductsDTO
    {
        [Required]
        public int CartId { get; set; }
        public List<CartProductItemForUpdate> CartProducts { get; set; }
    }
}
