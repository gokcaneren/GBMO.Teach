using GBMO.Teach.Core.DTOs.Input.Teacher.TeacherSchedule;
using GBMO.Teach.Core.DTOs.Input.Teacher.TeacherSchedule.BookRequest;
using GBMO.Teach.Core.DTOs.Output.Teacher.TeacherSchedule;
using GBMO.Teach.Core.Services.CommonServices;
using GBMO.Teach.Core.Services.TeacherServices;
using GBMO.Teach.Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GBMO.Teach.API.Controllers.Teacher
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherSchedulesController : ControllerBase
    {
        private readonly ITeacherScheduleService _teacherScheduleService;
        private readonly IScheduleBookRequestService _scheduleBookRequestService;

        public TeacherSchedulesController(
            ITeacherScheduleService teacherScheduleService,
            IScheduleBookRequestService scheduleBookRequestService)
        {
            _teacherScheduleService = teacherScheduleService;
            _scheduleBookRequestService = scheduleBookRequestService;
        }

        [HttpPost("create-class")]
        [Authorize]
        public async Task<ApiResponse<bool>> CreateClassScheduleAsync(TeacherScheduleCreateInput teacherScheduleCreateInput,
            CancellationToken cancellationToken = default)
        {
            return await _teacherScheduleService.CreateClassScheduleAsync(teacherScheduleCreateInput, cancellationToken);
        }

        [HttpPost("class-book-requests")]
        public async Task<ApiResponse<List<StudentWithScheduleOutput>>> GetClassBookRequestsAsync(CancellationToken cancellationToken = default)
        {
            return await _scheduleBookRequestService.GetClassBookRequestsAsync(cancellationToken);
        }

        [HttpPost("act-book-request/{isAccepted}")]
        public async Task<ApiResponse<bool>> ActBookRequestAsync(bool isAccepted, BookRequestInput bookRequestInput, CancellationToken cancellationToken = default)
        {
            return await _teacherScheduleService.ActBookRequestAsync(isAccepted, bookRequestInput, cancellationToken);
        }
    }
}
