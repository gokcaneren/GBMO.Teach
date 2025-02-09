using GBMO.Teach.Application.Services;
using GBMO.Teach.Core.Entities.Auth;
using GBMO.Teach.Core.Repositories;
using GBMO.Teach.Core.UnitOfWorks;

namespace GBMO.Teach.Core.Services.AuthServices
{
    public class UserService : Service<User>, IUserService
    {
        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
