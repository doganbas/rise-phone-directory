using Rise.PhoneDirectory.Store.Dtos;
using Rise.PhoneDirectory.Store.Models;
using System.Linq.Expressions;

namespace Rise.PhoneDirectory.Core.Services
{
    public interface IContactInformationService
    {
        Task<ContactInformationDto> GetByIdAsync(int id);

        ContactInformationDto GetById(int id);


        IEnumerable<ContactInformationDto> Where(Expression<Func<ContactInformation, bool>> expression = null);


        Task<bool> AnyAsync(Expression<Func<ContactInformation, bool>> expression = null);

        bool Any(Expression<Func<ContactInformation, bool>> expression = null);


        Task<ContactInformationDto> AddAsync(ContactInformationDto entity);

        ContactInformationDto Add(ContactInformationDto entity);


        Task<IEnumerable<ContactInformationDto>> AddRangeAsync(IEnumerable<ContactInformationDto> entities);

        IEnumerable<ContactInformationDto> AddRange(IEnumerable<ContactInformationDto> entities);


        Task UpdateAsync(ContactInformationDto entity);

        void Update(ContactInformationDto entity);


        Task RemoveAsync(ContactInformationDto entity);

        void Remove(ContactInformationDto entity);


        Task RemoveAsync(int id);

        void Remove(int id);


        Task RemoveRageAsync(IEnumerable<ContactInformationDto> entities);

        void RemoveRage(IEnumerable<ContactInformationDto> entities);
    }
}