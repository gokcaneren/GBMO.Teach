using GBMO.Teach.Core.DTOs.Output.Teacher.TeacherSchedule;
using GBMO.Teach.Core.Entities.Common;
using GBMO.Teach.Core.Utilities;

namespace GBMO.Teach.Core.Services.CommonServices
{
    public interface IScheduleBookRequestService : IService<ScheduleBookRequest>
    {
        Task<ApiResponse<bool>> SendClassBookRequestAsync(string teacherId, string scheduleId,
            CancellationToken cancellationToken = default);
        Task<ApiResponse<List<StudentWithScheduleOutput>>> GetClassBookRequestsAsync(CancellationToken cancellationToken = default);
    }
}
