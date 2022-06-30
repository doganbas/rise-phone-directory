using Microsoft.Extensions.Logging;
using Rise.PhoneDirectory.Core.Aspects;
using Rise.PhoneDirectory.Core.Constants;
using Rise.PhoneDirectory.Core.Repositories;
using Rise.PhoneDirectory.Core.Services;
using Rise.PhoneDirectory.Core.UnitOfWorks;
using Rise.PhoneDirectory.Store.Abstract;
using System.Linq.Expressions;

namespace Rise.PhoneDirectory.Service.Services
{
    [ExceptionLogAspect]
    public class GenericService<TEntity> : IGenericService<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TEntity> _logger;

        public GenericService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> repository, ILogger<TEntity> logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        [CacheAspect]
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        [CacheAspect]
        public TEntity GetById(int id)
        {
            return _repository.GetById(id);
        }


        [CacheAspect]
        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression = null)
        {
            return _repository.Where(expression);
        }


        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public bool Any(Expression<Func<TEntity, bool>> expression = null)
        {
            return _repository.Any(expression);
        }


        [CacheRemoveAspect]
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }

        [CacheRemoveAspect]
        public TEntity Add(TEntity entity)
        {
            _repository.Add(entity);
            _unitOfWork.SaveChanges();
            return entity;
        }


        [CacheRemoveAspect]
        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.SaveChangesAsync();
            return entities;
        }

        [CacheRemoveAspect]
        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            _repository.AddRange(entities);
            _unitOfWork.SaveChanges();
            return entities;
        }


        [CacheRemoveAspect]
        public async Task UpdateAsync(TEntity entity)
        {
            _repository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        [CacheRemoveAspect]
        public void Update(TEntity entity)
        {
            _repository.Update(entity);
            _unitOfWork.SaveChanges();
        }


        [CacheRemoveAspect]
        public async Task RemoveAsync(TEntity entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, typeof(TEntity).Name);
        }

        [CacheRemoveAspect]
        public void Remove(TEntity entity)
        {
            _repository.Remove(entity);
            _unitOfWork.SaveChanges();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, typeof(TEntity).Name);
        }

        [CacheRemoveAspect]
        public async Task RemoveAsync(int id)
        {
            var removeEntity = await _repository.GetByIdAsync(id);

            if (removeEntity == null)
                throw new Exception(ProjectConst.DeleteNotFoundError);

            _repository.Remove(removeEntity);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, typeof(TEntity).Name);
        }

        [CacheRemoveAspect]
        public void Remove(int id)
        {
            var removeEntity = _repository.GetById(id);

            if (removeEntity == null)
                throw new Exception(ProjectConst.DeleteNotFoundError);

            _repository.Remove(removeEntity);
            _unitOfWork.SaveChanges();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, typeof(TEntity).Name);
        }


        [CacheRemoveAspect]
        public async Task RemoveRageAsync(IEnumerable<TEntity> entities)
        {
            _repository.RemoveRage(entities);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, typeof(TEntity).Name);
        }

        [CacheRemoveAspect]
        public void RemoveRage(IEnumerable<TEntity> entities)
        {
            _repository.RemoveRage(entities);
            _unitOfWork.SaveChanges();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, typeof(TEntity).Name);
        }
    }
}