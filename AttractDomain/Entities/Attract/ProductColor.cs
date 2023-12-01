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
    [Table("ProductColor", Schema = "Attract")]

    public class ProductColor:EntityBase
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }

        // Navigation properties
        public Product Product { get; set; }
        public Color Color { get; set; }
    }
}
