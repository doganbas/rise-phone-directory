using Rise.PhoneDirectory.Core.UnitOfWorks;

namespace Rise.PhoneDirectory.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PhoneDirectoryDbContext _dbContext;

        public UnitOfWork(PhoneDirectoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}