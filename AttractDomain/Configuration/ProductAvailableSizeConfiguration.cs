using AttractDomain.Entities.Attract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttractDomain.Configuration
{
    public class ProductAvailableSizeConfiguration : IEntityTypeConfiguration<ProductAvailableSize>
    {
        public void Configure(EntityTypeBuilder<ProductAvailableSize> builder)
        {
            builder
           .HasKey(sc => new { sc.ProductId, sc.AvailableSizeId });
            builder
                .HasOne(pas => pas.Product)
                .WithMany(p => p.ProductAvailableSizes)
                .HasForeignKey(pas => pas.ProductId);

            builder
                .HasOne(pas => pas.AvailableSize)
                .WithMany(s => s.ProductAvailableSizes)
                .HasForeignKey(pas => pas.AvailableSizeId);
        }
    }
}
