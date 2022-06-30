using AutoMapper;
using Rise.PhoneDirectory.Core.Aspects;
using Rise.PhoneDirectory.Core.Repositories;
using Rise.PhoneDirectory.Core.Services;
using Rise.PhoneDirectory.Core.UnitOfWorks;
using Rise.PhoneDirectory.Service.ValidationRules;
using Rise.PhoneDirectory.Store.Dtos;
using Rise.PhoneDirectory.Store.Models;
using System.Linq.Expressions;

namespace Rise.PhoneDirectory.Service.Services
{
    public class ContactInformationService : IContactInformationService
    {
        private readonly IGenericRepository<ContactInformation> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactInformationService(IGenericRepository<ContactInformation> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<ContactInformationDto> GetByIdAsync(int id)
        {
            var contactInformation = await _repository.GetByIdAsync(id);
            return _mapper.Map<ContactInformationDto>(contactInformation);
        }

        public ContactInformationDto GetById(int id)
        {
            var contactInformation = _repository.GetById(id);
            return _mapper.Map<ContactInformationDto>(contactInformation);
        }


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
        public async Task<ContactInformationDto> AddAsync(ContactInformationDto entity)
        {
            var contactInformation = _mapper.Map<ContactInformation>(entity);
            await _repository.AddAsync(contactInformation);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ContactInformationDto>(contactInformation);
        }

        [ValidationAspect(typeof(ContactInformationDtoValidator))]
        public ContactInformationDto Add(ContactInformationDto entity)
        {
            var contactInformation = _mapper.Map<ContactInformation>(entity);
            _repository.Add(contactInformation);
            _unitOfWork.SaveChanges();
            return _mapper.Map<ContactInformationDto>(contactInformation);
        }


        [ValidationAspect(typeof(ContactInformationDtoValidator))]
        public async Task<IEnumerable<ContactInformationDto>> AddRangeAsync(IEnumerable<ContactInformationDto> entities)
        {
            var contactInformations = _mapper.Map<List<ContactInformation>>(entities);
            await _repository.AddRangeAsync(contactInformations);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<IEnumerable<ContactInformationDto>>(contactInformations);
        }

        [ValidationAspect(typeof(ContactInformationDtoValidator))]
        public IEnumerable<ContactInformationDto> AddRange(IEnumerable<ContactInformationDto> entities)
        {
            var contactInformations = _mapper.Map<List<ContactInformation>>(entities);
            _repository.AddRange(contactInformations);
            _unitOfWork.SaveChanges();
            return _mapper.Map<IEnumerable<ContactInformationDto>>(contactInformations);
        }


        [ValidationAspect(typeof(ContactInformationDtoValidator))]
        public async Task UpdateAsync(ContactInformationDto entity)
        {
            _repository.Update(_mapper.Map<ContactInformation>(entity));
            await _unitOfWork.SaveChangesAsync();
        }

        [ValidationAspect(typeof(ContactInformationDtoValidator))]
        public void Update(ContactInformationDto entity)
        {
            _repository.Update(_mapper.Map<ContactInformation>(entity));
            _unitOfWork.SaveChanges();
        }


        public async Task RemoveAsync(ContactInformationDto entity)
        {
            _repository.Remove(_mapper.Map<ContactInformation>(entity));
            await _unitOfWork.SaveChangesAsync();
        }

        public void Remove(ContactInformationDto entity)
        {
            _repository.Remove(_mapper.Map<ContactInformation>(entity));
            _unitOfWork.SaveChanges();
        }


        public async Task RemoveAsync(int id)
        {
            var contactInformation = _repository.GetById(id);
            _repository.Remove(contactInformation);
            await _unitOfWork.SaveChangesAsync();
        }

        public void Remove(int id)
        {
            var contactInformation = _repository.GetById(id);
            _repository.Remove(contactInformation);
            _unitOfWork.SaveChanges();
        }


        public async Task RemoveRageAsync(IEnumerable<ContactInformationDto> entities)
        {
            _repository.RemoveRage(_mapper.Map<List<ContactInformation>>(entities));
            await _unitOfWork.SaveChangesAsync();
        }

        public void RemoveRage(IEnumerable<ContactInformationDto> entities)
        {
            _repository.RemoveRage(_mapper.Map<List<ContactInformation>>(entities));
            _unitOfWork.SaveChanges();
        }


    }
}