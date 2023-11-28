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
    public class AvailableSizeConfiguration : IEntityTypeConfiguration<AvailableSize>
    {
        public void Configure(EntityTypeBuilder<AvailableSize> builder)
        {
            builder.HasOne(s => s.Product).WithMany(a => a.AvailableSizes).HasForeignKey(s => s.ProductId);
        }
    }
}
