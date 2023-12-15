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
    public class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
    {

        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {
           builder
            .HasKey(cp => cp.Id);

            builder
                .HasOne(cp => cp.ProductQuantity)
                .WithMany(p => p.CartProducts)
                .HasForeignKey(cp => cp.ProductQuantityId);

            builder
                .HasOne(cp => cp.Cart)
                .WithMany(c => c.CartProducts)
                .HasForeignKey(cp => cp.CartId);
        }
    }
}
