﻿using AttractDomain.Entities.Attract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttractDomain.Configuration
{
    public class ImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            //builder.HasOne(s => s.Product).WithMany(a => a.Images).HasForeignKey(s => s.ProductId);
        }
    }
}
