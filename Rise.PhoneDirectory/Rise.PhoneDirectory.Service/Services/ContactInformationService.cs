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
using System.Linq.Expressions;

namespace Rise.PhoneDirectory.Service.Services
{
    [ExceptionLogAspect]
    public class ContactInformationService : IContactInformationService
    {
        private readonly IGenericRepository<ContactInformation> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ContactInformationService> _logger;

        public ContactInformationService(IGenericRepository<ContactInformation> repository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<ContactInformationService> logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }


        [CacheAspect]
        public async Task<ContactInformationDto> GetByIdAsync(int id)
        {
            var contactInformation = await _repository.GetByIdAsync(id);
            return _mapper.Map<ContactInformationDto>(contactInformation);
        }

        [CacheAspect]
        public ContactInformationDto GetById(int id)
        {
            var contactInformation = _repository.GetById(id);
            return _mapper.Map<ContactInformationDto>(contactInformation);
        }


        [CacheAspect]
        public IEnumerable<ContactInformationDto> Where(Expression<Func<ContactInformation, bool>> expression = null)
        {
            var contactInformations = _repository.Where(expression).ToList();
            return _mapper.Map<IEnumerable<ContactInformationDto>>(contactInformations);
        }


        public async Task<bool> AnyAsync(Expression<Func<ContactInformation, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public bool Any(Expression<Func<ContactInformation, bool>> expression = null)
        {
            return _repository.Any(expression);
        }


        [ValidationAspect(typeof(ContactInformationDtoValidator))]
        [CacheRemoveAspect]
        public async Task<ContactInformationDto> AddAsync(ContactInformationDto entity)
        {
            var contactInformation = _mapper.Map<ContactInformation>(entity);
            await _repository.AddAsync(contactInformation);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ContactInformationDto>(contactInformation);
        }

        [ValidationAspect(typeof(ContactInformationDtoValidator))]
        [CacheRemoveAspect]
        public ContactInformationDto Add(ContactInformationDto entity)
        {
            var contactInformation = _mapper.Map<ContactInformation>(entity);
            _repository.Add(contactInformation);
            _unitOfWork.SaveChanges();
            return _mapper.Map<ContactInformationDto>(contactInformation);
        }


        [ValidationAspect(typeof(ContactInformationDtoValidator))]
        [CacheRemoveAspect]
        public async Task<IEnumerable<ContactInformationDto>> AddRangeAsync(IEnumerable<ContactInformationDto> entities)
        {
            var contactInformations = _mapper.Map<List<ContactInformation>>(entities);
            await _repository.AddRangeAsync(contactInformations);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<IEnumerable<ContactInformationDto>>(contactInformations);
        }

        [ValidationAspect(typeof(ContactInformationDtoValidator))]
        [CacheRemoveAspect]
        public IEnumerable<ContactInformationDto> AddRange(IEnumerable<ContactInformationDto> entities)
        {
            var contactInformations = _mapper.Map<List<ContactInformation>>(entities);
            _repository.AddRange(contactInformations);
            _unitOfWork.SaveChanges();
            return _mapper.Map<IEnumerable<ContactInformationDto>>(contactInformations);
        }


        [ValidationAspect(typeof(ContactInformationDtoValidator))]
        [CacheRemoveAspect]
        public async Task UpdateAsync(ContactInformationDto entity)
        {
            _repository.Update(_mapper.Map<ContactInformation>(entity));
            await _unitOfWork.SaveChangesAsync();
        }

        [ValidationAspect(typeof(ContactInformationDtoValidator))]
        [CacheRemoveAspect]
        public void Update(ContactInformationDto entity)
        {
            _repository.Update(_mapper.Map<ContactInformation>(entity));
            _unitOfWork.SaveChanges();
        }


        [CacheRemoveAspect]
        public async Task RemoveAsync(ContactInformationDto entity)
        {
            _repository.Remove(_mapper.Map<ContactInformation>(entity));
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, typeof(ContactInformation).Name);
        }

        [CacheRemoveAspect]
        public void Remove(ContactInformationDto entity)
        {
            _repository.Remove(_mapper.Map<ContactInformation>(entity));
            _unitOfWork.SaveChanges();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, typeof(ContactInformation).Name);
        }


        [CacheRemoveAspect]
        public async Task RemoveByIdAsync(int id)
        {
            var contactInformation = await _repository.GetByIdAsync(id);
            if (contactInformation == null)
                throw new Exception(ProjectConst.DeleteNotFoundError);

            _repository.Remove(contactInformation);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, typeof(ContactInformation).Name);
        }

        [CacheRemoveAspect]
        public void RemoveById(int id)
        {
            var contactInformation = _repository.GetById(id);
            if (contactInformation == null)
                throw new Exception(ProjectConst.DeleteNotFoundError);

            _repository.Remove(contactInformation);
            _unitOfWork.SaveChanges();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, typeof(ContactInformation).Name);
        }


        [CacheRemoveAspect]
        public async Task RemoveRageAsync(IEnumerable<ContactInformationDto> entities)
        {
            _repository.RemoveRage(_mapper.Map<List<ContactInformation>>(entities));
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, typeof(ContactInformation).Name);
        }

        [CacheRemoveAspect]
        public void RemoveRage(IEnumerable<ContactInformationDto> entities)
        {
            _repository.RemoveRage(_mapper.Map<List<ContactInformation>>(entities));
            _unitOfWork.SaveChanges();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, typeof(ContactInformation).Name);
        }
    }
}