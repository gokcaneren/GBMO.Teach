using GBMO.Teach.Core.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GBMO.Teach.Infrastructure.Configurations.Common
{
    internal class TeacherStudentConnectionConfiguration : IEntityTypeConfiguration<TeacherStudentConnection>
    {
        public void Configure(EntityTypeBuilder<TeacherStudentConnection> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.ConnectionTime).IsRequired(true);

            builder.HasQueryFilter(c => c.IsDeleted == false);
        }
    }
}
