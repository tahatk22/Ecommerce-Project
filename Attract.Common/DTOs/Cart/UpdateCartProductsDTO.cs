using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Common.DTOs.Cart
{
    public class UpdateCartProductsDTO
    {
        public List<AddCartProductsDTO> CartProducts { get; set; }
    }
}
