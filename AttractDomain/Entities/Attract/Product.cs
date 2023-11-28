using Attract.Framework.Entity;
using AttractDomain.Entities.Attract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attract.Domain.Entities.Attract
{
    [Table("Product", Schema = "Attract")]

    public class Product: EntityBase
    {
        public Product()
        {
            Images = new HashSet<ProductImage>();
            OrderDetails=new HashSet<OrderDetail>();
        }
        public int Id { get; set; }
        [MaxLength(250)]
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }

        public ICollection<Color> Colors { get; set; }
        public int? SubCategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

        public ICollection<AvailableSize> AvailableSizes { get; set; }
    }
}
