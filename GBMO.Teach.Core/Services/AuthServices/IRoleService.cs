using GBMO.Teach.Core.DTOs.Output.Auth.Role;
using GBMO.Teach.Core.Entities.Auth;
using GBMO.Teach.Core.Utilities;

namespace GBMO.Teach.Core.Services.AuthServices
{
    public interface IRoleService : IService<Role>
    {
        Task<ApiResponse<List<SimpleRoleOutput>>> SimpleRoleList();
    }
}
