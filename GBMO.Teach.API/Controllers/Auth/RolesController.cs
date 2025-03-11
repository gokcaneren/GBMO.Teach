using GBMO.Teach.Core.DTOs.Output.Auth.Role;
using GBMO.Teach.Core.Services.AuthServices;
using GBMO.Teach.Core.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace GBMO.Teach.API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ApiResponse<List<SimpleRoleOutput>>> GetRolesAsync(CancellationToken cancellationToken)
        {
            return await _roleService.SimpleRoleList();
        }
    }
}
