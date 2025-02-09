using GBMO.Teach.Core.UnitOfWorks;
using GBMO.Teach.Infrastructure.Context;

namespace GBMO.Teach.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GbmoDbContext _gbmoDbContext;

        public UnitOfWork(GbmoDbContext gbmoDbContext)
        {
            _gbmoDbContext = gbmoDbContext;
        }

        public int Commit()
        {
            using (var transaction = _gbmoDbContext.Database.BeginTransaction())
            {
                try
                {
                    var saveCount = _gbmoDbContext.SaveChanges();
                    transaction.Commit();
                    return saveCount;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<int> CommitAsync()
        {
            using (var transaction = _gbmoDbContext.Database.BeginTransaction())
            {
                try
                {
                    var saveCount = await _gbmoDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return saveCount;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
