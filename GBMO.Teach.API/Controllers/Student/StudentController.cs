using GBMO.Teach.Core.DTOs.Output.Student;
using GBMO.Teach.Core.DTOs.Output.Teacher;
using GBMO.Teach.Core.DTOs.Output.Teacher.TeacherSchedule;
using GBMO.Teach.Core.Repositories.StudentRepositories;
using GBMO.Teach.Core.Services.CommonServices;
using GBMO.Teach.Core.Services.StudentServices;
using GBMO.Teach.Core.Services.TeacherServices;
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
        private readonly ITeacherScheduleService _teacherScheduleService;
        private readonly IScheduleBookRequestService _scheduleBookRequestService;

        public StudentController(ISubRequestService subRequestService,
            IStudentService studentService,
            ITeacherScheduleService teacherScheduleService,
            IScheduleBookRequestService scheduleBookRequestService)
        {
            _subRequestService = subRequestService;
            _studentService = studentService;
            _teacherScheduleService = teacherScheduleService;
            _scheduleBookRequestService = scheduleBookRequestService;
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

        [HttpGet("{teacherId}/schedules")]
        public async Task<ApiResponse<TeacherWithScheduleOutput>> GetTeacherClassScheduleAsync(string teacherId, 
            CancellationToken cancellationToken = default)
        {
            return await _teacherScheduleService.GetTeacherClassScheduleAsync(teacherId, cancellationToken);
        }

        [HttpPost("{teacherId}/{scheduleId}/book-class")]
        public async Task<ApiResponse<bool>> SendClassBookRequestAsnyc(string teacherId, string scheduleId,
            CancellationToken cancellationToken = default)
        {
            return await _scheduleBookRequestService.SendClassBookRequestAsync(teacherId, scheduleId, cancellationToken);
        }


    }
}
