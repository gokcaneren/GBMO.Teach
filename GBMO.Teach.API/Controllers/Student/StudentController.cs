using GBMO.Teach.Core.DTOs.Output.Student;
using GBMO.Teach.Core.Services.CommonServices;
using GBMO.Teach.Core.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace GBMO.Teach.API.Controllers.Student
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ISubRequestService _subRequestService;

        public StudentController(ISubRequestService subRequestService)
        {
            _subRequestService = subRequestService;
        }

        [HttpPost("{teacherId}/subscribe")]
        public async Task<ApiResponse<bool>> SendSubRequestAsync(string teacherId, CancellationToken cancellationToken = default)
        {
            return await _subRequestService.SendSubRequestAsync(teacherId, cancellationToken);
        }

        [HttpGet("non-sub-teachers")]
        public async Task<ApiResponse<List<NonSubTeacherOutput>>> GetNonSubTeachersAsync(CancellationToken cancellationToken = default)
        {
            return await _subRequestService.GetNonSubTeachersAsync(cancellationToken);
        }
    }
}
