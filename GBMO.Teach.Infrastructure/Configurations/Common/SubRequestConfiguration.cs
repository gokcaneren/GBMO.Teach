using GBMO.Teach.Core.Entities.Common;
using GBMO.Teach.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GBMO.Teach.Infrastructure.Configurations.Common
{
    public class SubRequestConfiguration : IEntityTypeConfiguration<SubsRequest>
    {
        public void Configure(EntityTypeBuilder<SubsRequest> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x=>x.StudenId).IsRequired();
            builder.Property(x=>x.TeacherId).IsRequired();
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(SubRequestStatusses.Sent);
        }
    }
}
