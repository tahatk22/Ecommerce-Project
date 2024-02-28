using Attract.Framework.Entity;
using AttractDomain.Entities.Attract;
using AttractDomain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attract.Domain.Entities.Attract
{
    [Table("Product", Schema = "Attract")]

    public class Product: EntityBase
    {
        public int Id { get; set; }
        [MaxLength(250)]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public bool IsArchived { get; set; } = false;
        public DiscountOption DiscountOption { get; set; }
        public decimal Discount { get; set; } = 0;
        //public bool RecommendedProducts { get; set; } = false;
        //public bool FeaturedProducts { get; set; } = false;
        //public bool TrendingProducts { get; set; } = false;
        public ProductTypeOption ProductTypeOption { get; set; }
        public int SaleCount { get; set; } = 0;
        public int? SubCategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        //public ICollection<OrderDetail> OrderDetails { get; set; }
        //public ICollection<ProductAvailableSize> ProductAvailableSizes { get; set; }
        //public ICollection<ProductColor> ProductColors { get; set; }
        public ICollection<ProductQuantity> ProductQuantities { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; }
        public ICollection<ProductImage>? Images { get; set; }

    }
}