using Rise.PhoneDirectory.Core.Constants;
using Rise.PhoneDirectory.Core.Services;
using Rise.PhoneDirectory.Store.Dtos;
using System.Net.Http.Json;

namespace Rise.PhoneDirectory.ReportWorker.Services
{
    public class ReportApiService : IReportApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ReportApiService> _logger;

        public ReportApiService(HttpClient httpClient, ILogger<ReportApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<ReportDataDto>> GetReportDataAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<ReportDataDto>>("Report/GetReportData");
            return response;
        }

        public List<ReportDataDto> GetReportData()
        {
            return GetReportDataAsync().Result;
        }

        public async Task<bool> CompleteReportAsync(byte[] reportFile, int reportId)
        {
            MultipartFormDataContent multipartFormDataContent = new()
            {
                { new ByteArrayContent(reportFile), "reportFile", Guid.NewGuid().ToString() + ".xlsx" }
            };
            var response = await _httpClient.PostAsync($"/Report/CompleteReport/{reportId}", multipartFormDataContent);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation(string.Format(ProjectConst.ExcelReportServiceCrated, reportId));
                return true;
            }

            return false;
        }

        public bool CompleteReport(byte[] reportFile, int reportId)
        {
            return CompleteReportAsync(reportFile, reportId).Result;
        }
    }
}