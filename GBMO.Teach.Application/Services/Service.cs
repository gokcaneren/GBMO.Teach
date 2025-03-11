using GBMO.Teach.Core.Entities;
using GBMO.Teach.Core.Repositories;
using GBMO.Teach.Core.Services;
using GBMO.Teach.Core.UnitOfWorks;

namespace GBMO.Teach.Application.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : BaseEntity
    {
        private readonly IGenericRepository<TEntity> _repository;

        public Service(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<TEntity> CreateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await _repository.CreateAsync(entity, false, cancellationToken);
            return entity;
        }

        public async Task<List<TEntity>> CreateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _repository.CreateRangeAsync(entities, cancellationToken);
            return entities;
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _repository.DeleteAsync(entity, cancellationToken);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public IQueryable<TEntity> GetAllAsQueryable()
        {
            return _repository.GetAllAsQueryable();
        }

        public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }

        public TEntity? GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _repository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _repository.UpdateAsync(entity, cancellationToken);
            return entity;
        }
    }
}
