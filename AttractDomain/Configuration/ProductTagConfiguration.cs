using Attract.Domain.Entities.Attract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AttractDomain.Configuration
{
    public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductTag> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .HasOne(s => s.Product)
                .WithMany(a => a.ProductTags)
                .HasForeignKey(s => s.ProductId);

            builder
                .HasOne(s => s.Tag)
                .WithMany(a => a.ProductTags)
                .HasForeignKey(s => s.TagId);
        }
    }
}
