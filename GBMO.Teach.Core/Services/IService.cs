using GBMO.Teach.Core.Entities;

namespace GBMO.Teach.Core.Services
{
    public interface IService<TEntity> where TEntity : BaseEntity
    {
        TEntity? GetById(Guid id);
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        IQueryable<TEntity> GetAllAsQueryable();
        IEnumerable<TEntity> GetAll();
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<TEntity> CreateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
        Task<List<TEntity>> CreateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default);

        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
