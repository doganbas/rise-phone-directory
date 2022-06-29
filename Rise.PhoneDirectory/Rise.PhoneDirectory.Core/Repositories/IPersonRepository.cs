using Rise.PhoneDirectory.Store.Models;

namespace Rise.PhoneDirectory.Core.Repositories
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        public Task<Person> GetPersonByIdWithContactInformationAsync(int personId);

        public Person GetPersonByIdWithContactInformation(int personId);
    }
}