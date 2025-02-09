using GBMO.Teach.Application.Services;
using GBMO.Teach.Core.Entities.Common;
using GBMO.Teach.Core.Repositories;
using GBMO.Teach.Core.UnitOfWorks;

namespace GBMO.Teach.Core.Services.CommonServices
{
    public class ClassBookingService : Service<ClassBooking>, IClassBookingService
    {
        public ClassBookingService(IGenericRepository<ClassBooking> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
