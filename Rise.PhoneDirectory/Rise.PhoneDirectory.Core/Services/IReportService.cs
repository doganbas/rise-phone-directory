using Microsoft.AspNetCore.Http;
using Rise.PhoneDirectory.Store.Dtos;
using Rise.PhoneDirectory.Store.Models;

namespace Rise.PhoneDirectory.Core.Services
{
    public interface IReportService : IGenericService<Report>
    {
        public Task<bool> ReportExcelAsync(int reportId);

        public bool ReportExcel(int reportId);

        public List<ReportDataDto> GetReportData();

        public Task<bool> CompleteReportAsync(IFormFile reportFile, int reportId);

        public bool CompleteReport(IFormFile reportFile, int reportId);
    }
}