using GBMO.Teach.Core.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GBMO.Teach.Infrastructure.Configurations.Common
{
    public class ClassBookingConfiguration : IEntityTypeConfiguration<ClassBooking>
    {
        public void Configure(EntityTypeBuilder<ClassBooking> builder)
        {
            builder.HasKey(x=>x.Id);

            builder.Property(c => c.Status).IsRequired(true);
            builder.Property(c=>c.StartTime).IsRequired(true);
            builder.Property(c=>c.EndTime).IsRequired(true);

            builder.HasQueryFilter(c => c.IsDeleted == false);
        }
    }
}
