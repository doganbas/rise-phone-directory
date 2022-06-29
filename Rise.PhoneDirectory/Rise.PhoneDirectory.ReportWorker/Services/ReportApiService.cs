using Rise.PhoneDirectory.Store.Dtos;
using System.Net.Http.Json;

namespace Rise.PhoneDirectory.ReportWorker.Services
{
    public class ReportApiService
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
    }
}
