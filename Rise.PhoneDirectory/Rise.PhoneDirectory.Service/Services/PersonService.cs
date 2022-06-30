using AutoMapper;
using Microsoft.Extensions.Logging;
using Rise.PhoneDirectory.Core.Aspects;
using Rise.PhoneDirectory.Core.Constants;
using Rise.PhoneDirectory.Core.Repositories;
using Rise.PhoneDirectory.Core.Services;
using Rise.PhoneDirectory.Core.UnitOfWorks;
using Rise.PhoneDirectory.Service.ValidationRules;
using Rise.PhoneDirectory.Store.Dtos;
using Rise.PhoneDirectory.Store.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Rise.PhoneDirectory.Service.Services
{
    [ExceptionLogAspect]
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonService> _logger;

        public PersonService(IPersonRepository repository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<PersonService> logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }


        [CacheAspect]
        public async Task<PersonDto> GetByIdAsync(int id)
        {
            var person = await _repository.GetByIdAsync(id);
            return _mapper.Map<PersonDto>(person);
        }

        [CacheAspect]
        public PersonDto GetById(int id)
        {
            var person = _repository.GetById(id);
            return _mapper.Map<PersonDto>(person);
        }


        [CacheAspect]
        public IEnumerable<PersonDto> Where(Expression<Func<Person, bool>> expression = null)
        {
            var persons = _repository.Where(expression).ToList();
            return _mapper.Map<IEnumerable<PersonDto>>(persons);
        }


        public async Task<bool> AnyAsync(Expression<Func<Person, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public bool Any(Expression<Func<Person, bool>> expression = null)
        {
            return _repository.Any(expression);
        }


        [ValidationAspect(typeof(PersonDtoValidator))]
        [CacheRemoveAspect]
        public async Task<PersonDto> AddAsync(PersonDto entity)
        {
            var person = _mapper.Map<Person>(entity);
            if (_repository.Any(nq => nq.Name == person.Name && nq.Surname == person.Surname))
                throw new ValidationException(ValidationMessages.PersonNameUniqueError);

            await _repository.AddAsync(person);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<PersonDto>(person);
        }

        [ValidationAspect(typeof(PersonDtoValidator))]
        [CacheRemoveAspect]
        public PersonDto Add(PersonDto entity)
        {
            var person = _mapper.Map<Person>(entity);
            if (_repository.Any(nq => nq.Name == person.Name && nq.Surname == person.Surname))
                throw new ValidationException(ValidationMessages.PersonNameUniqueError);

            _repository.Add(person);
            _unitOfWork.SaveChanges();
            return _mapper.Map<PersonDto>(person);
        }


        [ValidationAspect(typeof(PersonDtoValidator))]
        [CacheRemoveAspect]
        public async Task<IEnumerable<PersonDto>> AddRangeAsync(IEnumerable<PersonDto> entities)
        {
            var persons = _mapper.Map<List<Person>>(entities);
            foreach (var item in persons)
                if (_repository.Any(nq => nq.Name == item.Name && nq.Surname == item.Surname))
                    throw new ValidationException(ValidationMessages.PersonNameUniqueError);

            await _repository.AddRangeAsync(persons);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<IEnumerable<PersonDto>>(persons);
        }

        [ValidationAspect(typeof(PersonDtoValidator))]
        [CacheRemoveAspect]
        public IEnumerable<PersonDto> AddRange(IEnumerable<PersonDto> entities)
        {
            var persons = _mapper.Map<List<Person>>(entities);
            foreach (var item in persons)
                if (_repository.Any(nq => nq.Name == item.Name && nq.Surname == item.Surname))
                    throw new ValidationException(ValidationMessages.PersonNameUniqueError);

            _repository.AddRange(persons);
            _unitOfWork.SaveChanges();
            return _mapper.Map<IEnumerable<PersonDto>>(persons);
        }


        [ValidationAspect(typeof(PersonDtoValidator))]
        [CacheRemoveAspect]
        public async Task UpdateAsync(PersonDto entity)
        {
            if (_repository.Any(nq => nq.PersonId != entity.Id && nq.Name == entity.Name && nq.Surname == entity.Surname))
                throw new ValidationException(ValidationMessages.PersonNameUniqueError);

            _repository.Update(_mapper.Map<Person>(entity));
            await _unitOfWork.SaveChangesAsync();
        }

        [ValidationAspect(typeof(PersonDtoValidator))]
        [CacheRemoveAspect]
        public void Update(PersonDto entity)
        {
            if (_repository.Any(nq => nq.PersonId != entity.Id && nq.Name == entity.Name && nq.Surname == entity.Surname))
                throw new ValidationException(ValidationMessages.PersonNameUniqueError);

            _repository.Update(_mapper.Map<Person>(entity));
            _unitOfWork.SaveChanges();
        }


        [CacheRemoveAspect]
        public async Task RemoveAsync(PersonDto entity)
        {
            _repository.Remove(_mapper.Map<Person>(entity));
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, typeof(Person).Name);
        }

        [CacheRemoveAspect]
        public void Remove(PersonDto entity)
        {
            _repository.Remove(_mapper.Map<Person>(entity));
            _unitOfWork.SaveChanges();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, typeof(Person).Name);
        }


        [CacheRemoveAspect]
        public async Task RemoveAsync(int id)
        {
            var person = _repository.GetById(id);
            if (person == null)
                throw new Exception(ProjectConst.DeleteNotFoundError);
            _repository.Remove(person);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, typeof(Person).Name);
        }

        [CacheRemoveAspect]
        public void Remove(int id)
        {
            var person = _repository.GetById(id);
            if (person == null)
                throw new ArgumentNullException(nameof(person));
            _repository.Remove(person);
            _unitOfWork.SaveChanges();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, typeof(Person).Name);
        }


        [CacheRemoveAspect]
        public async Task RemoveRageAsync(IEnumerable<PersonDto> entities)
        {
            _repository.RemoveRage(_mapper.Map<List<Person>>(entities));
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, typeof(Person).Name);
        }

        [CacheRemoveAspect]
        public void RemoveRage(IEnumerable<PersonDto> entities)
        {
            _repository.RemoveRage(_mapper.Map<List<Person>>(entities));
            _unitOfWork.SaveChanges();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, typeof(Person).Name);
        }


        [CacheAspect]
        public async Task<PersonWithContactInfoDto> GetPersonByIdWithContactInformationAsync(int personId)
        {
            var person = await _repository.GetPersonByIdWithContactInformationAsync(personId);
            return _mapper.Map<PersonWithContactInfoDto>(person);
        }

        [CacheAspect]
        public PersonWithContactInfoDto GetPersonByIdWithContactInformation(int personId)
        {
            var person = _repository.GetPersonByIdWithContactInformation(personId);
            return _mapper.Map<PersonWithContactInfoDto>(person);
        }
    }
}