using Rise.PhoneDirectory.Store.Dtos;
using Rise.PhoneDirectory.Store.Models;
using System.Linq.Expressions;

namespace Rise.PhoneDirectory.Core.Services
{
    public interface IPersonService
    {
        Task<PersonDto> GetByIdAsync(int id);

        PersonDto GetById(int id);


        IEnumerable<PersonDto> Where(Expression<Func<Person, bool>> expression = null);


        Task<bool> AnyAsync(Expression<Func<Person, bool>> expression = null);

        bool Any(Expression<Func<Person, bool>> expression = null);


        Task<PersonDto> AddAsync(PersonDto entity);

        PersonDto Add(PersonDto entity);


        Task<IEnumerable<PersonDto>> AddRangeAsync(IEnumerable<PersonDto> entities);

        IEnumerable<PersonDto> AddRange(IEnumerable<PersonDto> entities);


        Task UpdateAsync(PersonDto entity);

        void Update(PersonDto entity);


        Task RemoveAsync(PersonDto entity);

        void Remove(PersonDto entity);


        Task RemoveByIdAsync(int id);

        void RemoveById(int id);


        Task RemoveRageAsync(IEnumerable<PersonDto> entities);

        void RemoveRage(IEnumerable<PersonDto> entities);


        public Task<PersonWithContactInfoDto> GetPersonByIdWithContactInformationAsync(int personId);

        public PersonWithContactInfoDto GetPersonByIdWithContactInformation(int personId);
    }
}