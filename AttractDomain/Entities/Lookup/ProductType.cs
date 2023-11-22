using Attract.Domain.Entities.Attract;
using Attract.Framework.Entity;
using Attract.Framework.UoW;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Domain.Entities.Lookup
{
    [Table("ProductType", Schema = "Lookup")]

    public class ProductType:EntityBase
    {
        public ProductType()
        {
            Products = new HashSet<Product>();
        }
        public int Id { get; set; }
        public string Type { get; set; }
        public int InternalRef { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
