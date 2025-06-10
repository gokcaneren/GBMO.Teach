using GBMO.Teach.Core.Entities.Common;
using GBMO.Teach.Core.Repositories.CommonRepositories;
using GBMO.Teach.Infrastructure.Context;

namespace GBMO.Teach.Infrastructure.Repositories.CommonRepositories
{
    public class ScheduleBookRequestRepository : GenericRepository<ScheduleBookRequest>, IScheduleBookRequestRepository
    {
        public ScheduleBookRequestRepository(GbmoDbContext gbmoDbContext) : base(gbmoDbContext)
        {
        }
    }
}
