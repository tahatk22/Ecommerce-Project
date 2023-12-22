using Attract.Domain.Entities.Attract;
using Attract.Framework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttractDomain.Entities.Attract
{
    [Table("ProductQuantity", Schema = "Attract")]
    public class ProductQuantity : EntityBase
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? AvailableSizeId { get; set; }
        public int? ColorId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string ImageName { get; set; }

        public virtual Product Product { get; set; }
        public virtual AvailableSize AvailableSize { get; set; }
        public virtual Color Color { get; set; }
        public virtual ICollection<CartProduct> CartProducts { get; set;}
    }
}
