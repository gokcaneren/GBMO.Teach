using GBMO.Teach.Core.Entities;
using GBMO.Teach.Core.Repositories;
using GBMO.Teach.Core.Services;
using GBMO.Teach.Core.UnitOfWorks;

namespace GBMO.Teach.Application.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : BaseEntity
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public Service(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _repository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<List<TEntity>> CreateRangeAsync(List<TEntity> entities)
        {
            await _repository.CreateRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return entities;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await _repository.DeleteAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public IQueryable<TEntity> GetAllAsQueryable()
        {
            return _repository.GetAllAsQueryable();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public TEntity? GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await _repository.UpdateAsync(entity);
            return entity;
        }
    }
}
