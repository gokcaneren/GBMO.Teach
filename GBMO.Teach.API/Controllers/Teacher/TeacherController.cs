using GBMO.Teach.Core.DTOs.Output.Auth.User;
using GBMO.Teach.Core.Services.TeacherServices;
using GBMO.Teach.Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GBMO.Teach.API.Controllers.Teacher
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet("sub-requesters")]
        public async Task<ApiResponse<List<UserSimpleOutput>>> GetSubRequestersAsync(CancellationToken cancellationToken = default)
        {
            return await _teacherService.GetSubRequestListAsync(cancellationToken);
        }
    }
}
