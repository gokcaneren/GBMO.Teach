using GBMO.Teach.Core.DTOs.Input.Teacher.TeacherSchedule;
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
        private ITeacherScheduleService _teacherScheduleService;

        public TeacherSchedulesController(ITeacherScheduleService teacherScheduleService)
        {
            _teacherScheduleService = teacherScheduleService;
        }

        [HttpPost("CreateClass")]
        [Authorize]
        public async Task<ApiResponse<bool>> CreateClassScheduleAsync(TeacherScheduleCreateInput teacherScheduleCreateInput,
            CancellationToken cancellationToken = default)
        {
            return await _teacherScheduleService.CreateClassScheduleAsync(teacherScheduleCreateInput, cancellationToken);
        }
    }
}
