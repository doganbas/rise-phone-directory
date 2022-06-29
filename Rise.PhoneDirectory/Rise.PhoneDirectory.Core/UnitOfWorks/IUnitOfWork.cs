namespace Rise.PhoneDirectory.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();

        void SaveChanges();
    }
}
