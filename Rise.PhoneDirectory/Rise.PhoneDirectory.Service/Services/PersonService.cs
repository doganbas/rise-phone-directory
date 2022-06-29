using AutoMapper;
using Rise.PhoneDirectory.Core.Repositories;
using Rise.PhoneDirectory.Core.Services;
using Rise.PhoneDirectory.Core.UnitOfWorks;
using Rise.PhoneDirectory.Store.Dtos;
using Rise.PhoneDirectory.Store.Models;

namespace Rise.PhoneDirectory.Service.Services
{
    public class PersonService : GenericService<Person>, IPersonService
    {
        private readonly IPersonRepository _repository;
        private readonly IMapper _mapper;

        public PersonService(IUnitOfWork unitOfWork, IPersonRepository repository, IMapper mapper) : base(unitOfWork, repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PersonWithContactInfoDto> GetPersonByIdWithContactInformationAsync(int personId)
        {
            var person = await _repository.GetPersonByIdWithContactInformationAsync(personId);
            return _mapper.Map<PersonWithContactInfoDto>(person);
        }

        public PersonWithContactInfoDto GetPersonByIdWithContactInformation(int personId)
        {
            var person = _repository.GetPersonByIdWithContactInformation(personId);
            return _mapper.Map<PersonWithContactInfoDto>(person);
        }
    }
}