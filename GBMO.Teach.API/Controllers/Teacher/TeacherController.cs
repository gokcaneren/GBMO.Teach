using GBMO.Teach.Core.DTOs.Output.Auth.User;
using GBMO.Teach.Core.DTOs.Output.Teacher.TeacherSchedule;
using GBMO.Teach.Core.Services.TeacherServices;
using GBMO.Teach.Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;

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
        public async Task<ApiResponse<List<StudentUserSimpleOutput>>> GetSubRequestersAsync(CancellationToken cancellationToken = default)
        {
            return await _teacherService.GetSubRequestListAsync(cancellationToken);
        }


        [HttpPost("sub-request/{studentId}")]
        public async Task<ApiResponse<bool>> AcceptSubRequestAsync(string studentId, bool isAccepted = false,
            CancellationToken cancellationToken = default)
        {
            return await _teacherService.ActSubRequestAsync(studentId, isAccepted, cancellationToken);
        }

        [HttpGet("all-classes")]
        public async Task<ApiResponse<List<TeacherClassOutput>>> GetAllClassesAsync(bool onlyActives = false,
            CancellationToken cancellationToken = default)
        {
            return await _teacherService.GetAllClassesAsync(onlyActives, cancellationToken);
        }

        [HttpGet("class-history")]
        public async Task<ApiResponse<List<TeacherClassOutput>>> GetClassHistoryAsync(CancellationToken cancellationToken = default)
        {
            return await _teacherService.GetClassHistoryAsync(cancellationToken);
        }

    }
}
