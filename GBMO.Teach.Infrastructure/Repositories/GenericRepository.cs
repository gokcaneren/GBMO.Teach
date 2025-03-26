using GBMO.Teach.Core.Entities;
using GBMO.Teach.Core.Repositories;
using GBMO.Teach.Infrastructure.Context;
using GBMO.Teach.Infrastructure.Extensions;
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

        public async Task CreateAsync(TEntity entity, bool autoSave= false, CancellationToken cancellationToken = default)
        {
            await _gbmoDbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

            if (autoSave)
            {
                await _gbmoDbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public void CreateRange(List<TEntity> entities)
        {
            _gbmoDbContext.Set<TEntity>().AddRange(entities);
        }

        public async Task CreateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _gbmoDbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
        }

        public void Delete(TEntity entity)
        {
            _gbmoDbContext.Remove(entity);
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => _gbmoDbContext.Remove(entity), cancellationToken);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _gbmoDbContext.Set<TEntity>().ToList();
        }

        public IQueryable<TEntity> GetAllAsQueryable()
        {
            return _gbmoDbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _gbmoDbContext.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public TEntity? GetBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _gbmoDbContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        public async Task<TEntity?> GetByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _gbmoDbContext.Set<TEntity>().FirstOrDefaultAsync(predicate ,cancellationToken);
        }

        public TEntity? GetById(Guid id)
        {
            return _gbmoDbContext.Set<TEntity>().FirstOrDefault(c=>c.Id.Equals(id));
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _gbmoDbContext.Set<TEntity>().FirstOrDefaultAsync(c => c.Id.Equals(id), cancellationToken);
        }

        public IEnumerable<TEntity> GetListBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _gbmoDbContext.Set<TEntity>().Where(predicate).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetListByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _gbmoDbContext.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken);
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

        public async Task UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await Task.Run(()=> _gbmoDbContext.Set<TEntity>().Update(entity), cancellationToken);

            if (autoSave)
            {
                await _gbmoDbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task LoadNavigationPropertyAsync<TProperty>(TEntity entity,
            Expression<Func<TEntity, TProperty?>> navigationProperty, CancellationToken cancellationToken = default)
            where TProperty : class
        {
            var navigationPropertyName = navigationProperty.GetPropertyName();


            var navigation = _gbmoDbContext.Entry(entity).Metadata
            .FindNavigation(navigationPropertyName);

            if (navigation?.IsCollection == true)
            {
                await _gbmoDbContext.Entry(entity)
                .Collection(navigation)
                .LoadAsync(cancellationToken);
            }
            else
            {
                await _gbmoDbContext.Entry(entity)
                .Reference(navigationProperty)
                .LoadAsync(cancellationToken);
            }
                
        }
        //public async Task LoadNavigationCollectionPropertyAsync<TProperty>(TEntity entity,
        //    Expression<Func<TEntity, TProperty?>> navigationProperty, CancellationToken cancellationToken = default)
        //    where TProperty : class
        //{
        //    await _gbmoDbContext.Entry(entity)
        //        .Collection(navigationProperty)
        //        .LoadAsync(cancellationToken);
        //}
    }
}
