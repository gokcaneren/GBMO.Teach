using GBMO.Teach.Core.Entities.Auth;

namespace GBMO.Teach.Core.Repositories.AuthRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<List<User>> GetNotConnectedTeachersAsync(string studentId, CancellationToken cancellationToken = default);
        Task<List<User>> GetConnectedTeachersAsync(string studentId, CancellationToken cancellationToken = default);
        Task<List<User>> GetRequesterStudentsAsync(List<Guid> studentIds, CancellationToken cancellationToken = default);
    }
}
