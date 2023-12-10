using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Common.DTOs.Cart
{
    public class CartProductsDTO
    {
        public List<CartProductItemsForGet> CartProducts { get; set; }
        public decimal? Total => CartProducts.Sum(x => x.ProductPrice * x.Quantity);
    }
}
