using GBMO.Teach.Core.Entities.Auth;
using GBMO.Teach.Core.Repositories.AuthRepositories;
using GBMO.Teach.Infrastructure.Context;

namespace GBMO.Teach.Infrastructure.Repositories.AuthRepositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(GbmoDbContext gbmoDbContext) : base(gbmoDbContext)
        {
        }
    }
}
