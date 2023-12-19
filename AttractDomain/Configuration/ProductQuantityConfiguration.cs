using AttractDomain.Entities.Attract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AttractDomain.Configuration
{
    public class ProductQuantityConfiguration : IEntityTypeConfiguration<ProductQuantity>
    {

        public void Configure(EntityTypeBuilder<ProductQuantity> builder)
        {
           builder
            .HasKey(cp => cp.Id);

            builder
                .HasOne(cp => cp.Product)
                .WithMany(p => p.ProductQuantities)
                .HasForeignKey(cp => cp.ProductId);

            builder
                .HasOne(cp => cp.AvailableSize)
                .WithMany(p => p.ProductQuantities)
                .HasForeignKey(cp => cp.AvailableSizeId);

            builder
                .HasOne(cp => cp.Color)
                .WithMany(p => p.ProductQuantities)
                .HasForeignKey(cp => cp.ColorId);
        }
    }
}
