using Attract.Domain.Entities.Lookup;
using Attract.Framework.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attract.Domain.Entities.Attract
{
    [Table("ProductType", Schema = "Attract")]

    public class Product: EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        //public int? ProductTypeId { get; set; }
        //public virtual ProductType ProductType { get; set; }
        //public int? CategoryId { get; set; }
        //public virtual Category Category { get; set; }
        //public ICollection<string> AvailableSize { get; set; }
    }
}
