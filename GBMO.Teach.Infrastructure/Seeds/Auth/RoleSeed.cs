using GBMO.Teach.Core.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GBMO.Teach.Infrastructure.Seeds.Auth
{
    public class RoleSeed : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(new Role[]
            {
                new Role{Id= Guid.NewGuid(), RoleTypeId = Core.Enums.RoleTypes.Admin},
                new Role{Id= Guid.NewGuid(), RoleTypeId = Core.Enums.RoleTypes.Teacher},
                new Role{Id= Guid.NewGuid(), RoleTypeId = Core.Enums.RoleTypes.Student},
                new Role{Id= Guid.NewGuid(), RoleTypeId = Core.Enums.RoleTypes.Parent}
            });
        }
    }
}
