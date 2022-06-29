using Rise.PhoneDirectory.Store.Dtos;
using Rise.PhoneDirectory.Store.Models;

namespace Rise.PhoneDirectory.Core.Services
{
    public interface IReportService : IGenericService<Report>
    {
        public List<ReportDataDto> GetReportData();
    }
}