using Rise.PhoneDirectory.Store.Dtos;
using Rise.PhoneDirectory.Store.Models;

namespace Rise.PhoneDirectory.Core.Services
{
    public interface IReportService : IGenericService<Report>
    {
        public List<ReportDataDto> GetReportData();

        public Task<bool> ReportExcelAsync(int reportId);

        public bool ReportExcel(int reportId);
    }
}