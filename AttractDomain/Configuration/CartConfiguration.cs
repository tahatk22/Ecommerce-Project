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
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {

        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .HasOne(pc => pc.User)
                .WithMany(p => p.Carts)
                .HasForeignKey(pc => pc.UserId);
        }
    }
}
