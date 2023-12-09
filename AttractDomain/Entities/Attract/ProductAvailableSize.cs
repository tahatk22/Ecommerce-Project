using Attract.Domain.Entities.Attract;
using Attract.Framework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttractDomain.Entities.Attract
{
    [Table("ProductAvailableSize", Schema = "Attract")]

    public class ProductAvailableSize:EntityBase
    {
        public int ProductId { get; set; }
        public int AvailableSizeId { get; set; }

        public Product Product { get; set; }
        public AvailableSize AvailableSize { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }
    }
}
