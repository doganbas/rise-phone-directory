using Rise.PhoneDirectory.Store.Dtos;

namespace Rise.PhoneDirectory.Core.Services
{
    public interface IReportApiService
    {
        Task<List<ReportDataDto>> GetReportDataAsync();

        List<ReportDataDto> GetReportData();

        Task<bool> CompleteReportAsync(byte[] reportFile, int reportId);

        bool CompleteReport(byte[] reportFile, int reportId);
    }
}