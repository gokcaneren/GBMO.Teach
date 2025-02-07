using GBMO.Teach.Core.Entities.Teachers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GBMO.Teach.Infrastructure.Configurations.Teachers;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Bio).IsRequired(false);
        builder.Property(c => c.HourlyRate).IsRequired(false);

        builder.HasQueryFilter(c => c.IsDeleted == false);

        builder.HasMany(c => c.ClassBookings)
            .WithOne(x => x.Teacher)
            .HasForeignKey(z => z.TeacherId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.TeacherSchedules)
            .WithOne(x => x.Teacher)
            .HasForeignKey(z => z.TeacherId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.TeacherStudentConnections)
            .WithOne(c => c.Teacher)
            .HasForeignKey(z => z.TeacherId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}