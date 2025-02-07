using GBMO.Teach.Core.Entities.Auth;
using GBMO.Teach.Core.Entities.Students;
using GBMO.Teach.Core.Entities.Teachers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GBMO.Teach.Infrastructure.Configurations.Auth
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.FirstName).IsRequired(true);
            builder.Property(c => c.LastName).IsRequired(true);
            builder.Property(c => c.Email).IsRequired(true);
            builder.Property(c => c.PasswordHash).IsRequired(true);

            builder.HasQueryFilter(c => c.IsDeleted == false);

            builder.HasOne(c => c.Role)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoleId);

            builder.HasOne(c => c.Teacher)
                .WithOne(x => x.User)
                .HasForeignKey<Teacher>(z => z.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Student)
                .WithOne(x => x.User)
                .HasForeignKey<Student>(z => z.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
