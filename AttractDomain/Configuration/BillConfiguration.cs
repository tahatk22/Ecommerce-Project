using AttractDomain.Entities.Attract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AttractDomain.Configuration
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasOne(s => s.Order).WithOne(a => a.Bill).HasForeignKey<Bill>(s => s.OrderId);
            builder.HasOne(s => s.User).WithMany(a => a.Bills).HasForeignKey(s=>s.UserId);
        }
    }
}
