using GBMO.Teach.Core.Entities.Teachers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GBMO.Teach.Infrastructure.Configurations.Teachers;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.FirstName).IsRequired(true);
        builder.Property(c => c.LastName).IsRequired(true);
        builder.Property(c => c.Email).IsRequired(true);
        builder.Property(c => c.PasswordHash).IsRequired(true);
        builder.Property(c => c.Bio).IsRequired(false);
    }
}