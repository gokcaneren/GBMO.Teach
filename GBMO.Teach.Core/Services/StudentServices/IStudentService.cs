using GBMO.Teach.Core.DTOs.Output.Teacher;
using GBMO.Teach.Core.Entities.Students;
using GBMO.Teach.Core.Utilities;

namespace GBMO.Teach.Core.Services.StudentServices
{
    public interface IStudentService : IService<Student>
    {
        Task<ApiResponse<List<SimpleTeacherOutput>>> GetSubbedTeachersAsync(CancellationToken cancellationToken = default);
    }
}
