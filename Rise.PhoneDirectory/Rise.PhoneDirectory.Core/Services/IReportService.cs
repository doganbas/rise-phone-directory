using Microsoft.AspNetCore.Http;
using Rise.PhoneDirectory.Store.Dtos;
using Rise.PhoneDirectory.Store.Models;
using System.Linq.Expressions;

namespace Rise.PhoneDirectory.Core.Services
{
    public interface IReportService
    {
        Task<ReportDto> GetByIdAsync(int id);

        ReportDto GetById(int id);


        IEnumerable<ReportDto> Where(Expression<Func<Report, bool>> expression = null);


        Task<bool> AnyAsync(Expression<Func<Report, bool>> expression = null);

        bool Any(Expression<Func<Report, bool>> expression = null);


        Task<ReportDto> AddAsync(ReportDto entity);

        ReportDto Add(ReportDto entity);


        Task<IEnumerable<ReportDto>> AddRangeAsync(IEnumerable<ReportDto> entities);

        IEnumerable<ReportDto> AddRange(IEnumerable<ReportDto> entities);


        Task UpdateAsync(ReportDto entity);

        void Update(ReportDto entity);


        Task RemoveAsync(ReportDto entity);

        void Remove(ReportDto entity);


        Task RemoveByIdAsync(int id);

        void RemoveById(int id);


        Task RemoveRageAsync(IEnumerable<ReportDto> entities);

        void RemoveRage(IEnumerable<ReportDto> entities);


        public List<ReportDataDto> GetReportData();


        public Task<bool> ReportExcelAsync(int reportId);

        public bool ReportExcel(int reportId);


        public Task<bool> CompleteReportAsync(IFormFile reportFile, int reportId);

        public bool CompleteReport(IFormFile reportFile, int reportId);
    }
}