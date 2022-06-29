using Rise.PhoneDirectory.Store.Dtos;
using Rise.PhoneDirectory.Store.Models;

namespace Rise.PhoneDirectory.Core.Services
{
    public interface IPersonService : IGenericService<Person>
    {
        public Task<PersonWithContactInfoDto> GetPersonByIdWithContactInformationAsync(int personId);

        public PersonWithContactInfoDto GetPersonByIdWithContactInformation(int personId);
    }
}
