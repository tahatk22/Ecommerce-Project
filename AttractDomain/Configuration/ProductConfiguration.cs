using Attract.Domain.Entities.Attract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AttractDomain.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .HasOne(s => s.SubCategory)
                .WithMany(a => a.Products)
                .HasForeignKey(s => s.SubCategoryId);
        }
    }
}
