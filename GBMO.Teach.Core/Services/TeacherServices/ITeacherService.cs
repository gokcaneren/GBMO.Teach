using GBMO.Teach.Core.DTOs.Output.Auth.User;
using GBMO.Teach.Core.DTOs.Output.Teacher.TeacherSchedule;
using GBMO.Teach.Core.Entities.Teachers;
using GBMO.Teach.Core.Utilities;

namespace GBMO.Teach.Core.Services.TeacherServices
{
    public interface ITeacherService : IService<Teacher>
    {
        Task<ApiResponse<List<StudentUserSimpleOutput>>> GetSubRequestListAsync(CancellationToken cancellationToken = default);
        Task<ApiResponse<bool>> ActSubRequestAsync(string studentId, bool isAccepted = false,
            CancellationToken cancellationToken = default);
        Task<ApiResponse<List<TeacherClassOutput>>> GetAllClassesAsync(bool onlyActives = false,
            CancellationToken cancellationToken = default);
        Task<ApiResponse<List<TeacherClassOutput>>> GetClassHistoryAsync(CancellationToken cancellationToken = default);
    }
}
