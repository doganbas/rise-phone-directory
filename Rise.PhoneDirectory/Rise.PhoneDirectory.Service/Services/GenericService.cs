using Rise.PhoneDirectory.Core.Repositories;
using Rise.PhoneDirectory.Core.Services;
using Rise.PhoneDirectory.Core.UnitOfWorks;
using Rise.PhoneDirectory.Store.Abstract;
using System.Linq.Expressions;

namespace Rise.PhoneDirectory.Service.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public GenericService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }


        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public TEntity GetById(int id)
        {
            return _repository.GetById(id);
        }


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



        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }

        public TEntity Add(TEntity entity)
        {
            _repository.Add(entity);
            _unitOfWork.SaveChanges();
            return entity;
        }



        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.SaveChangesAsync();
            return entities;
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            _repository.AddRange(entities);
            _unitOfWork.SaveChanges();
            return entities;
        }



        public async Task UpdateAsync(TEntity entity)
        {
            _repository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            _repository.Update(entity);
            _unitOfWork.SaveChanges();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public void Delete(TEntity entity)
        {
            _repository.Remove(entity);
            _unitOfWork.SaveChanges();
        }

        public async Task DeleteRageAsync(IEnumerable<TEntity> entities)
        {
            _repository.RemoveRage(entities);
            await _unitOfWork.SaveChangesAsync();
        }

        public void DeleteRage(IEnumerable<TEntity> entities)
        {
            _repository.RemoveRage(entities);
            _unitOfWork.SaveChanges();
        }
    }
}