using GBMO.Teach.Application.Services;
using GBMO.Teach.Core.Entities.Auth;
using GBMO.Teach.Core.Repositories;
using GBMO.Teach.Core.UnitOfWorks;

namespace GBMO.Teach.Core.Services.AuthServices
{
    public class RoleService : Service<Role>, IRoleService
    {
        public RoleService(IGenericRepository<Role> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
