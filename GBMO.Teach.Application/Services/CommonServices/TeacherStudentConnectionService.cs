using GBMO.Teach.Application.Services;
using GBMO.Teach.Core.Entities.Common;
using GBMO.Teach.Core.Repositories;
using GBMO.Teach.Core.UnitOfWorks;

namespace GBMO.Teach.Core.Services.CommonServices
{
    public class TeacherStudentConnectionService : Service<TeacherStudentConnection>, ITeacherStudentConnectionService
    {
        public TeacherStudentConnectionService(IGenericRepository<TeacherStudentConnection> repository) : base(repository)
        {
        }
    }
}
