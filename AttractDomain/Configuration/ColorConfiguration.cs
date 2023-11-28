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
    public class ColorConfiguration :IEntityTypeConfiguration<Color>
    {

        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.HasOne(s => s.Product).WithMany(a => a.Colors).HasForeignKey(s => s.ProductId);
        }
    }
}
