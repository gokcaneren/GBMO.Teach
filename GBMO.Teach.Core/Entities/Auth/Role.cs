using GBMO.Teach.Core.Enums;

namespace GBMO.Teach.Core.Entities.Auth
{
    public class Role : BaseEntity
    {
        public RoleTypes RoleTypeId { get; set; }

        public virtual ICollection<User> Users{ get; set; }
    }
}
