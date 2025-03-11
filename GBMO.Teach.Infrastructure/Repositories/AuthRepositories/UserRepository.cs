using GBMO.Teach.Core.Entities.Auth;
using GBMO.Teach.Core.Repositories.AuthRepositories;
using GBMO.Teach.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GBMO.Teach.Infrastructure.Repositories.AuthRepositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(GbmoDbContext gbmoDbContext) : base(gbmoDbContext)
        {
        }

        public async Task<User?> GetUserWithUserRole(string email, CancellationToken cancellationToken = default)
        {
            return await _gbmoDbContext.Users.Include(c => c.Role).FirstOrDefaultAsync(c=>c.Email.Equals(email), cancellationToken);
        }
    }
}
