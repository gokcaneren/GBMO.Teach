using GBMO.Teach.Core.Entities.Configurations;
using GBMO.Teach.Core.Repositories.ConfigurationsRepositories;
using GBMO.Teach.Infrastructure.Context;

namespace GBMO.Teach.Infrastructure.Repositories.ConfigurationsRepositories
{
    public class SettingRepository : GenericRepository<Setting>, ISettingRepository
    {
        public SettingRepository(GbmoDbContext gbmoDbContext) : base(gbmoDbContext)
        {
        }
    }
}
