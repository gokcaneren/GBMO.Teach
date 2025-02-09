using GBMO.Teach.Core.Entities.Common;
using GBMO.Teach.Core.Repositories.CommonRepositories;
using GBMO.Teach.Infrastructure.Context;

namespace GBMO.Teach.Infrastructure.Repositories.CommonRepositories
{
    public class ClassBookingRepository : GenericRepository<ClassBooking>, IClassBookingRepository
    {
        public ClassBookingRepository(GbmoDbContext gbmoDbContext) : base(gbmoDbContext)
        {
        }
    }
}
