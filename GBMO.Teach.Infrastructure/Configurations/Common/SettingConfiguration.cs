using GBMO.Teach.Core.Entities.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GBMO.Teach.Infrastructure.Configurations.Common
{
    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Key).IsRequired(true);
            builder.Property(c => c.Value).IsRequired(true);

            builder.HasQueryFilter(c => c.IsDeleted == false);
        }
    }
}
