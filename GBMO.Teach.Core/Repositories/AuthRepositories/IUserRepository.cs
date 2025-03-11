using GBMO.Teach.Core.Entities.Auth;

namespace GBMO.Teach.Core.Repositories.AuthRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUserWithUserRole(string email, CancellationToken cancellationToken = default);
    }
}
