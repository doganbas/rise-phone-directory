using Rise.PhoneDirectory.Store.Dtos;
using Rise.PhoneDirectory.Store.Models;

namespace Rise.PhoneDirectory.Core.Repositories
{
    public interface IReportRepository : IGenericRepository<Report>
    {
        public List<ReportDataDto> GetReportData();
    }
}
