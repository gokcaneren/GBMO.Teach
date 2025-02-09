using GBMO.Teach.Application.Services;
using GBMO.Teach.Core.Entities.Configurations;
using GBMO.Teach.Core.Repositories;
using GBMO.Teach.Core.UnitOfWorks;

namespace GBMO.Teach.Core.Services.ConfigurationsServices
{
    public class SettingService : Service<Setting>, ISettingService
    {
        public SettingService(IGenericRepository<Setting> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
