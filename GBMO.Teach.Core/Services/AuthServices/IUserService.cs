using GBMO.Teach.Core.DTOs.Input.Auth.User;
using GBMO.Teach.Core.DTOs.Output.Auth.User;
using GBMO.Teach.Core.Entities.Auth;
using GBMO.Teach.Core.Utilities;

namespace GBMO.Teach.Core.Services.AuthServices
{
    public interface IUserService : IService<User>
    {
        Task<ApiResponse<bool>> RegisterAsync(UserRegisterInput userRegisterInput, CancellationToken cancellationToken = default);
        Task<ApiResponse<UserLoginOutput>> LoginAsync(UserLoginInput userLoginInput, CancellationToken cancellationToken = default);
    }
}
