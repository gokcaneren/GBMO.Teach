using GBMO.Teach.Core.DTOs.Output.Student;
using GBMO.Teach.Core.Entities.Common;
using GBMO.Teach.Core.Utilities;

namespace GBMO.Teach.Core.Services.CommonServices
{
    public interface ISubRequestService : IService<SubsRequest>
    {
        Task<ApiResponse<bool>> SendSubRequestAsync(string teacherId, CancellationToken cancellationToken = default);
        Task<ApiResponse<List<NonSubTeacher>>> GetNonSubTeachersAsync(CancellationToken cancellationToken = default);
    }
}
