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
    public class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
    {

        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {
            builder
                .HasKey(cp => cp.Id);

            builder
                .HasOne(pc => pc.Product)
                .WithMany(p => p.CartProducts)
                .HasForeignKey(pc => pc.ProductId);

            builder
                .HasOne(pc => pc.Cart)
                .WithMany(c => c.CartProducts)
                .HasForeignKey(pc => pc.CartId);

            builder
                .HasOne(pc => pc.ProductAvailableSize)
                .WithMany(p => p.CartProducts)
                .HasForeignKey(pc => pc.ProductAvailableSizeId);

            builder
                .HasOne(pc => pc.ProductColor)
                .WithMany(p => p.CartProducts)
                .HasForeignKey(pc => pc.ProductColorId);
        }
    }
}
