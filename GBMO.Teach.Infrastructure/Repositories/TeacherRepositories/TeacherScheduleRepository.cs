using GBMO.Teach.Core.Entities.Teachers;
using GBMO.Teach.Core.Repositories.TeacherRepositories;
using GBMO.Teach.Infrastructure.Context;

namespace GBMO.Teach.Infrastructure.Repositories.TeacherRepositories
{
    public class TeacherScheduleRepository : GenericRepository<TeacherSchedule>, ITeacherScheduleRepository
    {
        public TeacherScheduleRepository(GbmoDbContext gbmoDbContext) : base(gbmoDbContext)
        {
        }
    }
}
