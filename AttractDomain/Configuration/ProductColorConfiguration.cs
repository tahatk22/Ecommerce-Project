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
    public class ProductColorConfiguration :IEntityTypeConfiguration<ProductColor>
    {

        public void Configure(EntityTypeBuilder<ProductColor> builder)
        {
            builder
        .HasKey(pc => new { pc.ProductId, pc.ColorId });

            builder
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductColors)
                .HasForeignKey(pc => pc.ProductId);

            builder
                .HasOne(pc => pc.Color)
                .WithMany(c => c.ProductColors)
                .HasForeignKey(pc => pc.ColorId);
        }
    }
}
