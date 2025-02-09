using GBMO.Teach.Core.Entities;

namespace GBMO.Teach.Core.Services
{
    public interface IService<TEntity> where TEntity : BaseEntity
    {
        TEntity? GetById(Guid id);
        Task<TEntity?> GetByIdAsync(Guid id);
        IQueryable<TEntity> GetAllAsQueryable();
        IEnumerable<TEntity> GetAll();
        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> CreateAsync(TEntity entity);
        Task<List<TEntity>> CreateRangeAsync(List<TEntity> entities);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}
