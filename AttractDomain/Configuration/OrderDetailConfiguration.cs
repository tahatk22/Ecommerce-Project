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
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            //builder.HasOne(s=>s.Product).WithMany(a=>a.OrderDetails).HasForeignKey(a=>a.ProductId);
            builder.HasOne(s=>s.Order).WithMany(a=>a.OrderDetails).HasForeignKey(a=>a.OrderId);
        }
    }
}
