using RabbitMQ.Client;
using Rise.PhoneDirectory.Core.Constants;
using Rise.PhoneDirectory.Core.Repositories;
using Rise.PhoneDirectory.Core.Services;
using Rise.PhoneDirectory.Core.UnitOfWorks;
using Rise.PhoneDirectory.Store.Dtos;
using Rise.PhoneDirectory.Store.Models;
using System.Text;
using System.Text.Json;

namespace Rise.PhoneDirectory.Service.Services
{
    public class ReportService : GenericService<Report>, IReportService
    {
        private readonly IReportRepository _repository;
        private readonly IReporterClientService _reporterClientService;

        public ReportService(IUnitOfWork unitOfWork, IReportRepository repository, IReporterClientService reporterClientService) : base(unitOfWork, repository)
        {
            _repository = repository;
            _reporterClientService = reporterClientService;
        }

        public List<ReportDataDto> GetReportData()
        {
            return _repository.GetReportData();
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
            return true;
        }

        public bool ReportExcel(int reportId)
        {
            return ReportExcelAsync(reportId).Result;
        }
    }
}