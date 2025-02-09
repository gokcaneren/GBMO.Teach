using GBMO.Teach.Application.Services;
using GBMO.Teach.Core.Entities.Teachers;
using GBMO.Teach.Core.Repositories;
using GBMO.Teach.Core.UnitOfWorks;

namespace GBMO.Teach.Core.Services.TeacherServices
{
    public class TeacherService : Service<Teacher>, ITeacherService
    {
        public TeacherService(IGenericRepository<Teacher> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
