using GBMO.Teach.Core.Entities.Teachers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GBMO.Teach.Infrastructure.Configurations.Teachers
{
    public class TeacherScheduleConfiguration : IEntityTypeConfiguration<TeacherSchedule>
    {
        public void Configure(EntityTypeBuilder<TeacherSchedule> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c=>c.DayOfWeek).IsRequired(true);
            builder.Property(c=>c.StartTime).IsRequired(true);
            builder.Property(c=>c.EndTime).IsRequired(true);
            builder.Property(c=>c.IsBooked).IsRequired(true);

            builder.HasQueryFilter(c => c.IsDeleted == false);
        }
    }
}
