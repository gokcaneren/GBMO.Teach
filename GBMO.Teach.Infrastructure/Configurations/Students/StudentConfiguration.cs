using GBMO.Teach.Core.Entities.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GBMO.Teach.Infrastructure.Configurations.Students;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.FirstName).IsRequired(true);
        builder.Property(c => c.LastName).IsRequired(true);
        builder.Property(c => c.Email).IsRequired(true);
        builder.Property(c => c.PasswordHash).IsRequired(true);
        
    }
}