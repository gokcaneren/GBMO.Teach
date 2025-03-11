using GBMO.Teach.Core.DTOs.Input.Auth.User;
using GBMO.Teach.Core.DTOs.Output.Auth.User;
using GBMO.Teach.Core.Services.AuthServices;
using GBMO.Teach.Core.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace GBMO.Teach.API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<ApiResponse<bool>> Register(
            UserRegisterInput userRegisterInput, CancellationToken cancellationToken = default)
        {
            return await _userService.RegisterAsync(userRegisterInput, cancellationToken);
        }

        [HttpPost("Login")]
        public async Task<ApiResponse<UserLoginOutput>> Login(
            UserLoginInput userLoginInput, CancellationToken cancellationToken = default)
        {
            return await _userService.LoginAsync(userLoginInput, cancellationToken);
        }
    }
}
