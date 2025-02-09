using GBMO.Teach.Core.Entities;
using System.Linq.Expressions;

namespace GBMO.Teach.Core.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAllAsQueryable();
        IEnumerable<TEntity> GetAll();
        Task<List<TEntity>> GetAllAsync();
        TEntity? GetById(Guid id);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task DeleteAsync(TEntity entity);
        Task CreateAsync(TEntity entity);
        Task CreateRangeAsync(List<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task<int> SaveAsync();
        int Save();
        void Delete(TEntity entity);
        void Create(TEntity entity);
        void CreateRange(List<TEntity> entities);
        void Update(TEntity entity);
        TEntity? GetBy(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> GetByAsync(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetListBy(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetListByAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
