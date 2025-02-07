using GBMO.Teach.Core.Entities.Students;
using GBMO.Teach.Core.Entities.Teachers;

namespace GBMO.Teach.Core.Entities.Auth
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Student Student { get; set; }
    }
}
