using GBMO.Teach.Core.Entities;
using GBMO.Teach.Core.Repositories;
using GBMO.Teach.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GBMO.Teach.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly GbmoDbContext _gbmoDbContext;

        public GenericRepository(GbmoDbContext gbmoDbContext)
        {
            _gbmoDbContext = gbmoDbContext;
        }

        public void Create(TEntity entity)
        {
            _gbmoDbContext.Set<TEntity>().Add(entity);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _gbmoDbContext.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _gbmoDbContext.Remove(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => _gbmoDbContext.Remove(entity));
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _gbmoDbContext.Set<TEntity>().ToList();
        }

        public IQueryable<TEntity> GetAllAsQueryable()
        {
            return _gbmoDbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _gbmoDbContext.Set<TEntity>().ToListAsync();
        }

        public TEntity? GetBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _gbmoDbContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        public async Task<TEntity?> GetByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _gbmoDbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public TEntity? GetById(Guid id)
        {
            return _gbmoDbContext.Set<TEntity>().FirstOrDefault(c=>c.Id.Equals(id));
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _gbmoDbContext.Set<TEntity>().FirstOrDefaultAsync(c => c.Id.Equals(id));
        }

        public IEnumerable<TEntity> GetListBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _gbmoDbContext.Set<TEntity>().Where(predicate).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetListByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _gbmoDbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public int Save()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }   

        public void Update(TEntity entity)
        {
            _gbmoDbContext.Set<TEntity>().Update(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(()=> _gbmoDbContext.Set<TEntity>().Update(entity));
        }
    }
}
