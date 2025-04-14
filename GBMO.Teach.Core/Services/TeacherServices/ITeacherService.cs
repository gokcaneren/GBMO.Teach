using GBMO.Teach.Core.DTOs.Output.Auth.User;
using GBMO.Teach.Core.Entities.Teachers;
using GBMO.Teach.Core.Utilities;

namespace GBMO.Teach.Core.Services.TeacherServices
{
    public interface ITeacherService : IService<Teacher>
    {
        Task<ApiResponse<List<UserSimpleOutput>>> GetSubRequestListAsync(CancellationToken cancellationToken = default);
    }
}
