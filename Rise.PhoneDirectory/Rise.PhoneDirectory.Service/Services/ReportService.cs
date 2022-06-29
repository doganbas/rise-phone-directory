using Rise.PhoneDirectory.Core.Repositories;
using Rise.PhoneDirectory.Core.Services;
using Rise.PhoneDirectory.Core.UnitOfWorks;
using Rise.PhoneDirectory.Store.Dtos;
using Rise.PhoneDirectory.Store.Models;

namespace Rise.PhoneDirectory.Service.Services
{
    public class ReportService : GenericService<Report>, IReportService
    {
        private readonly IReportRepository _repository;

        public ReportService(IUnitOfWork unitOfWork, IReportRepository repository) : base(unitOfWork, repository)
        {
            _repository = repository;
        }

        public List<ReportDataDto> GetReportData()
        {
            return _repository.GetReportData();
        }
    }
}