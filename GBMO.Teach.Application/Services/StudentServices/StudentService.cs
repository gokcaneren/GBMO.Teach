using GBMO.Teach.Application.Services;
using GBMO.Teach.Core.Entities.Students;
using GBMO.Teach.Core.Repositories;
using GBMO.Teach.Core.UnitOfWorks;

namespace GBMO.Teach.Core.Services.StudentServices
{
    public class StudentService : Service<Student>, IStudentService
    {
        public StudentService(IGenericRepository<Student> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
