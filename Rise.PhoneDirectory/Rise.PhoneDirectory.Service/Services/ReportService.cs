using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Rise.PhoneDirectory.Core.Aspects;
using Rise.PhoneDirectory.Core.Constants;
using Rise.PhoneDirectory.Core.Repositories;
using Rise.PhoneDirectory.Core.Services;
using Rise.PhoneDirectory.Core.UnitOfWorks;
using Rise.PhoneDirectory.Service.ValidationRules;
using Rise.PhoneDirectory.Store.Dtos;
using Rise.PhoneDirectory.Store.Models;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;

namespace Rise.PhoneDirectory.Service.Services
{
    [ExceptionLogAspect]
    public class ReportService : IReportService
    {
        private readonly IReporterClientService _reporterClientService;
        private readonly IGenericRepository<ContactInformation> _contactInformationRepository;
        private readonly IGenericRepository<Report> _repository;
        private readonly ILogger<ReportService> _logger;
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReportService(IUnitOfWork unitOfWork, IGenericRepository<Report> repository, IPersonRepository personRepository, IMapper mapper, IReporterClientService reporterClientService, IGenericRepository<ContactInformation> contactInformationRepository, ILogger<ReportService> logger)
        {
            _repository = repository;
            _personRepository = personRepository; ;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _reporterClientService = reporterClientService;
            _contactInformationRepository = contactInformationRepository;
            _logger = logger;
        }


        [CacheAspect]
        public async Task<ReportDto> GetByIdAsync(int id)
        {
            var report = await _repository.GetByIdAsync(id);
            return _mapper.Map<ReportDto>(report);
        }

        [CacheAspect]
        public ReportDto GetById(int id)
        {
            var report = _repository.GetById(id);
            return _mapper.Map<ReportDto>(report);
        }


        [CacheAspect]
        public IEnumerable<ReportDto> Where(Expression<Func<Report, bool>> expression = null)
        {
            var reports = _repository.Where(expression).ToList();
            return _mapper.Map<IEnumerable<ReportDto>>(reports);
        }


        public async Task<bool> AnyAsync(Expression<Func<Report, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public bool Any(Expression<Func<Report, bool>> expression = null)
        {
            return _repository.Any(expression);
        }


        [ValidationAspect(typeof(ReportDtoValidator))]
        [CacheRemoveAspect]
        public async Task<ReportDto> AddAsync(ReportDto entity)
        {
            var report = _mapper.Map<Report>(entity);
            await _repository.AddAsync(report);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ReportDto>(report);
        }

        [ValidationAspect(typeof(ReportDtoValidator))]
        [CacheRemoveAspect]
        public ReportDto Add(ReportDto entity)
        {
            var report = _mapper.Map<Report>(entity);
            _repository.Add(report);
            _unitOfWork.SaveChanges();
            return _mapper.Map<ReportDto>(report);
        }


        [ValidationAspect(typeof(ReportDtoValidator))]
        [CacheRemoveAspect]
        public async Task<IEnumerable<ReportDto>> AddRangeAsync(IEnumerable<ReportDto> entities)
        {
            var reports = _mapper.Map<List<Report>>(entities);
            await _repository.AddRangeAsync(reports);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<IEnumerable<ReportDto>>(reports);
        }

        [ValidationAspect(typeof(ReportDtoValidator))]
        [CacheRemoveAspect]
        public IEnumerable<ReportDto> AddRange(IEnumerable<ReportDto> entities)
        {
            var reports = _mapper.Map<List<Report>>(entities);
            _repository.AddRange(reports);
            _unitOfWork.SaveChanges();
            return _mapper.Map<IEnumerable<ReportDto>>(reports);
        }


        [ValidationAspect(typeof(ReportDtoValidator))]
        [CacheRemoveAspect]
        public async Task UpdateAsync(ReportDto entity)
        {
            _repository.Update(_mapper.Map<Report>(entity));
            await _unitOfWork.SaveChangesAsync();
        }

        [ValidationAspect(typeof(ReportDtoValidator))]
        [CacheRemoveAspect]
        public void Update(ReportDto entity)
        {
            _repository.Update(_mapper.Map<Report>(entity));
            _unitOfWork.SaveChanges();
        }


        [CacheRemoveAspect]
        public async Task RemoveAsync(ReportDto entity)
        {
            _repository.Remove(_mapper.Map<Report>(entity));
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, nameof(Report));
        }

        [CacheRemoveAspect]
        public void Remove(ReportDto entity)
        {
            _repository.Remove(_mapper.Map<Report>(entity));
            _unitOfWork.SaveChanges();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, nameof(Report));
        }


        [CacheRemoveAspect]
        public async Task RemoveByIdAsync(int id)
        {
            var report = await _repository.GetByIdAsync(id);
            if (report == null)
                throw new Exception(ProjectConst.DeleteNotFoundError);
            _repository.Remove(report);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, nameof(Report));
        }

        [CacheRemoveAspect]
        public void RemoveById(int id)
        {
            var report = _repository.GetById(id);
            if (report == null)
                throw new Exception(ProjectConst.DeleteNotFoundError);
            _repository.Remove(report);
            _unitOfWork.SaveChanges();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, nameof(Report));
        }


        [CacheRemoveAspect]
        public async Task RemoveRageAsync(IEnumerable<ReportDto> entities)
        {
            _repository.RemoveRage(_mapper.Map<List<Report>>(entities));
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, nameof(Report));
        }

        [CacheRemoveAspect]
        public void RemoveRage(IEnumerable<ReportDto> entities)
        {
            _repository.RemoveRage(_mapper.Map<List<Report>>(entities));
            _unitOfWork.SaveChanges();
            _logger.LogInformation(ProjectConst.DeleteLogMessage, nameof(Report));
        }



        [CacheAspect]
        public List<ReportDataDto> GetReportData()
        {
            var reportData = new List<ReportDataDto>();
            _contactInformationRepository.Where(nq => nq.InformationType == Store.Enums.ContactInformationType.Location).Select(nq => nq.InformationContent).Distinct().ToList().ForEach(location =>
            {
                var persons = _personRepository.Where(nq => nq.ContactInformations.Any(sq => sq.InformationType == Store.Enums.ContactInformationType.Location && sq.InformationContent == location));
                reportData.Add(new()
                {
                    Location = location,
                    PersonCount = persons.Count(),
                    PhoneCount = persons.SelectMany(nq => nq.ContactInformations).Where(nq => nq.InformationType == Store.Enums.ContactInformationType.PhoneNumber).Count()
                });
            });
            _logger.LogInformation(ProjectConst.GetReportDataLogMessage);

            return reportData;
        }


        public async Task<bool> ReportExcelAsync(int reportId)
        {
            var report = await _repository.GetByIdAsync(reportId);
            if (report == null)
                return false;
            ReportExcelMessageDto reportExcelMessageDto = new()
            {
                ReportId = report.ReportId,
                StartDate = report.RequestTime
            };

            var channel = _reporterClientService.Connect();
            var bodyString = JsonSerializer.Serialize(reportExcelMessageDto);
            var bodyByte = Encoding.UTF8.GetBytes(bodyString);
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            channel.BasicPublish(exchange: ProjectConst.ExcelReportExchangeName, routingKey: ProjectConst.ExcelReportRouting, basicProperties: properties, body: bodyByte);
            _logger.LogInformation(ProjectConst.GetReportExcelLogMessage);

            return true;
        }

        public bool ReportExcel(int reportId)
        {
            return ReportExcelAsync(reportId).Result;
        }


        public async Task<bool> CompleteReportAsync(IFormFile reportFile, int reportId)
        {
            if (reportFile is not { Length: > 0 })
                return false;

            var report = await _repository.GetByIdAsync(reportId);
            if (report is null)
                return false;

            var fileName = Guid.NewGuid().ToString()[..10] + Path.GetExtension(reportFile.FileName);
            var saveDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/reports");
            var savePath = Path.Combine(saveDirectory, fileName);
            if (!Directory.Exists(saveDirectory))
                Directory.CreateDirectory(saveDirectory);

            using FileStream stream = new(savePath, FileMode.Create);
            await reportFile.CopyToAsync(stream);
            report.CreatedTime = DateTime.Now;
            report.FilePath = $"/reports/{fileName}";
            report.ReportStatus = Store.Enums.ReportStatus.Completed;
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation(ProjectConst.GetReportUploadLogMessage);

            return true;
        }

        public bool CompleteReport(IFormFile reportFile, int reportId)
        {
            return CompleteReportAsync(reportFile, reportId).Result;
        }

    }
}