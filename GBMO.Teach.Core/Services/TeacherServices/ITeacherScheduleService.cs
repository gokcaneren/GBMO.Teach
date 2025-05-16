using GBMO.Teach.Core.DTOs.Input.Teacher.TeacherSchedule;
using GBMO.Teach.Core.DTOs.Output.Teacher.TeacherSchedule;
using GBMO.Teach.Core.Entities.Teachers;
using GBMO.Teach.Core.Utilities;

namespace GBMO.Teach.Core.Services.TeacherServices
{
    public interface ITeacherScheduleService : IService<TeacherSchedule>
    {
        Task<ApiResponse<bool>> CreateClassScheduleAsync(TeacherScheduleCreateInput teacherScheduleCreateInput,
            CancellationToken cancellationToken = default);

        Task<ApiResponse<TeacherWithScheduleOutput>> GetTeacherClassScheduleAsync(string teacherId,
            CancellationToken cancellationToken = default);
    }
}
