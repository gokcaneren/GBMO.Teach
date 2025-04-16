using GBMO.Teach.Core.Entities.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GBMO.Teach.Infrastructure.Configurations.Students;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasQueryFilter(c => c.IsDeleted == false);

        builder.HasMany(c => c.TeacherSchedules)
            .WithOne(x => x.Student)
            .HasForeignKey(z => z.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.TeacherStudentConnections)
            .WithOne(x => x.Student)
            .HasForeignKey(z => z.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}