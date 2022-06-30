using Rise.PhoneDirectory.Store.Abstract;
using System.Linq.Expressions;

namespace Rise.PhoneDirectory.Core.Services
{
    public interface IGenericService<TEntity>
        where TEntity : class, IEntity, new()
    {
        Task<TEntity> GetByIdAsync(int id);

        TEntity GetById(int id);


        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression = null);


        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null);

        bool Any(Expression<Func<TEntity, bool>> expression = null);


        Task<TEntity> AddAsync(TEntity entity);

        TEntity Add(TEntity entity);


        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);

        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);


        Task UpdateAsync(TEntity entity);

        void Update(TEntity entity);


        Task RemoveAsync(TEntity entity);

        void Remove(TEntity entity);


        Task RemoveByIdAsync(int id);

        void RemoveById(int id);


        Task RemoveRageAsync(IEnumerable<TEntity> entities);

        void RemoveRage(IEnumerable<TEntity> entities);
    }
}