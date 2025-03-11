using GBMO.Teach.Application.Services;
using GBMO.Teach.Core.Entities.Teachers;
using GBMO.Teach.Core.Repositories;
using GBMO.Teach.Core.UnitOfWorks;

namespace GBMO.Teach.Core.Services.TeacherServices
{
    public class TeacherScheduleService : Service<TeacherSchedule>, ITeacherScheduleService
    {
        public TeacherScheduleService(IGenericRepository<TeacherSchedule> repository) : base(repository)
        {
        }
    }
}
