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
    public class ProductColorConfiguration : IEntityTypeConfiguration<ProductColor>
    {

        public void Configure(EntityTypeBuilder<ProductColor> builder)
        {
            builder
           .HasKey(sc => new { sc.ProductQuantityId, sc.ColorId});

            //builder
            //    .HasOne(pc => pc.Product)
            //    .WithMany(p => p.ProductQuantities)
            //    .HasForeignKey(pc => pc.ProductQuantityId);

            builder
                .HasOne(pc => pc.Color)
                .WithMany(p => p.ProductColors)
                .HasForeignKey(pc => pc.ColorId);
        }
    }
}
