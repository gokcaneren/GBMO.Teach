using GBMO.Teach.Core.Entities.Teachers;
using GBMO.Teach.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GBMO.Teach.Infrastructure.Configurations.Teachers
{
    public class TeacherScheduleConfiguration : IEntityTypeConfiguration<TeacherSchedule>
    {
        public void Configure(EntityTypeBuilder<TeacherSchedule> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.ClassStartDate).IsRequired(true);
            builder.Property(c => c.ClassEndDate).IsRequired(true);
            builder.Property(c => c.ClassStatusses).IsRequired(true).HasDefaultValue(ClassStatusses.NotBooked);

            builder.HasQueryFilter(c => c.IsDeleted == false);
        }
    }
}
