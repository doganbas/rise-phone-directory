using Microsoft.EntityFrameworkCore;
using Rise.PhoneDirectory.Core.Repositories;
using Rise.PhoneDirectory.Store.Models;

namespace Rise.PhoneDirectory.Repository.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(PhoneDirectoryDbContext dbContext) : base(dbContext)
        {
        }


        public async Task<Person> GetPersonByIdWithContactInformationAsync(int personId)
        {
            return await _dbContext.Persons.Include(nq => nq.ContactInformations).Where(nq => nq.PersonId == personId).SingleOrDefaultAsync();
        }

        public Person GetPersonByIdWithContactInformation(int personId)
        {
            return _dbContext.Persons.Include(nq => nq.ContactInformations).Where(nq => nq.PersonId == personId).SingleOrDefault();
        }
    }
}
