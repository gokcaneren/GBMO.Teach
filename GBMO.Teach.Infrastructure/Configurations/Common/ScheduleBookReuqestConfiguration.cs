using GBMO.Teach.Core.Entities.Common;
using GBMO.Teach.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GBMO.Teach.Infrastructure.Configurations.Common
{
    public class ScheduleBookReuqestConfiguration : IEntityTypeConfiguration<ScheduleBookRequest>
    {
        public void Configure(EntityTypeBuilder<ScheduleBookRequest> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(x => x.StudenId).IsRequired();
            builder.Property(x => x.TeacherId).IsRequired();
            builder.Property(x => x.ScheduleId).IsRequired();
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(RequestStatusses.Sent);
        }
    }
}
