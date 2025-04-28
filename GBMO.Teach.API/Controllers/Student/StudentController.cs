using GBMO.Teach.Core.DTOs.Output.Student;
using GBMO.Teach.Core.DTOs.Output.Teacher;
using GBMO.Teach.Core.Repositories.StudentRepositories;
using GBMO.Teach.Core.Services.CommonServices;
using GBMO.Teach.Core.Services.StudentServices;
using GBMO.Teach.Core.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace GBMO.Teach.API.Controllers.Student
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ISubRequestService _subRequestService;
        private readonly IStudentService _studentService;

        public StudentController(ISubRequestService subRequestService, IStudentService studentService)
        {
            _subRequestService = subRequestService;
            _studentService = studentService;
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

        [HttpGet("teachers")]
        public async Task<ApiResponse<List<SimpleTeacherOutput>>> GetSubbedTeachersAsync(CancellationToken cancellationToken = default)
        {
            return await _studentService.GetSubbedTeachersAsync(cancellationToken);
        }

        [HttpPost("{teacherId}/unsub-teacher")]
        public async Task<ApiResponse<bool>> UnSubTeacherAsync(string teacherId, CancellationToken cancellationToken = default)
        {
            return await _studentService.UnSubTeacherAsync(teacherId, cancellationToken);
        }
    }
}
