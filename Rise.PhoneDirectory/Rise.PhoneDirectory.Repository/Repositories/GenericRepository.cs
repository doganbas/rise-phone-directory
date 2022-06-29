using Microsoft.EntityFrameworkCore;
using Rise.PhoneDirectory.Core.Repositories;
using Rise.PhoneDirectory.Store.Abstract;
using System.Linq.Expressions;

namespace Rise.PhoneDirectory.Repository.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        protected readonly PhoneDirectoryDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(PhoneDirectoryDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }


        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }


        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
                return _dbSet.AsQueryable();
            return _dbSet.Where(expression).AsQueryable();
        }


        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public bool Any(Expression<Func<TEntity, bool>> expression = null)
        {
            return _dbSet.Any(expression);
        }


        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }


        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }


        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRage(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}