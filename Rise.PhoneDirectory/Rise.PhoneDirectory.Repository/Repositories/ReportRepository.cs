using Rise.PhoneDirectory.Core.Repositories;
using Rise.PhoneDirectory.Store.Dtos;
using Rise.PhoneDirectory.Store.Models;

namespace Rise.PhoneDirectory.Repository.Repositories
{
    public class ReportRepository : GenericRepository<Report>, IReportRepository
    {
        public ReportRepository(PhoneDirectoryDbContext dbContext) : base(dbContext)
        {
        }

        public List<ReportDataDto> GetReportData()
        {
            var reportData = new List<ReportDataDto>();
            _dbContext.ContactInformations.Where(nq => nq.InformationType == Store.Enums.ContactInformationType.Location).Select(nq => nq.InformationContent).Distinct().ToList().ForEach(location =>
            {
                var persons = _dbContext.Persons.Where(nq => nq.ContactInformations.Any(sq => sq.InformationType == Store.Enums.ContactInformationType.Location && sq.InformationContent == location));
                reportData.Add(new()
                {
                    Location = location,
                    PersonCount = persons.Count(),
                    PhoneCount = persons.SelectMany(nq => nq.ContactInformations).Where(nq => nq.InformationType == Store.Enums.ContactInformationType.PhoneNumber).Count()
                });
            });
            return reportData;
        }
    }
}