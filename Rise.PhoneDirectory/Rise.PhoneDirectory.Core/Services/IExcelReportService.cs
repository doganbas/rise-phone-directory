using Rise.PhoneDirectory.Store.Dtos;

namespace Rise.PhoneDirectory.Core.Services
{
    public interface IExcelReportService
    {
        Task<byte[]> CreateExcel(ReportExcelMessageDto reportExcelMessageDto);
    }
}
