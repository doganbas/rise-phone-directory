using Rise.PhoneDirectory.Store.Abstract;
using System.Linq.Expressions;

namespace Rise.PhoneDirectory.Core.Repositories
{
    public interface IGenericRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        Task<TEntity> GetByIdAsync(int id);

        TEntity GetById(int id);


        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression = null);


        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null);

        bool Any(Expression<Func<TEntity, bool>> expression = null);


        Task AddAsync(TEntity entity);

        void Add(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void AddRange(IEnumerable<TEntity> entities);


        void Update(TEntity entity);


        void Remove(TEntity entity);

        void RemoveRage(IEnumerable<TEntity> entities);
    }
}