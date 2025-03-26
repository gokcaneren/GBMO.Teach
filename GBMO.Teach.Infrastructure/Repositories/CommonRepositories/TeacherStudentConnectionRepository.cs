using GBMO.Teach.Core.Entities.Common;
using GBMO.Teach.Core.Repositories.CommonRepositories;
using GBMO.Teach.Infrastructure.Context;

namespace GBMO.Teach.Infrastructure.Repositories.CommonRepositories
{
    public class TeacherStudentConnectionRepository : GenericRepository<TeacherStudentConnection>, ITeacherStudentConnectionRepository
    {
        public TeacherStudentConnectionRepository(GbmoDbContext gbmoDbContext) : base(gbmoDbContext)
        {
        }
    }
}
