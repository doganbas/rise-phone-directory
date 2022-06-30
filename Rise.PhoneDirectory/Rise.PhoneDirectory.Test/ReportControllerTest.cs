using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Rise.PhoneDirectory.API.Controllers;
using Rise.PhoneDirectory.Core.Repositories;
using Rise.PhoneDirectory.Core.Services;
using Rise.PhoneDirectory.Core.UnitOfWorks;
using Rise.PhoneDirectory.Service.Mappings;
using Rise.PhoneDirectory.Service.Services;
using Rise.PhoneDirectory.Store.Dtos;
using Rise.PhoneDirectory.Store.Models;
using Rise.PhoneDirectory.Test.Helper;

namespace Rise.PhoneDirectory.Test
{
    public class ReportControllerTest
    {
        private readonly Mock<IGenericRepository<Report>> _mockRepository;
        private readonly Mock<IGenericRepository<ContactInformation>> _mockContactInformationRepository;
        private readonly Mock<IPersonRepository> _mockPersonRepository;
        private readonly Mock<IReporterClientService> _mockReporterClientService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ILogger<ReportService>> _mockLogger;
        private readonly IReportService _reportService;
        private readonly ReportController _controller;
        private readonly IMapper _mapper;
        private readonly List<Person> _persons;
        private readonly List<ContactInformation> _contacts;
        private readonly List<Report> _reports;

        public ReportControllerTest()
        {
            _mapper = new MapperConfiguration(mc => { mc.AddProfile(new MapProfile()); }).CreateMapper();
            _mockRepository = new Mock<IGenericRepository<Report>>();
            _mockContactInformationRepository = new Mock<IGenericRepository<ContactInformation>>();
            _mockPersonRepository = new Mock<IPersonRepository>();
            _mockReporterClientService = new Mock<IReporterClientService>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockLogger = new Mock<ILogger<ReportService>>();
            _reportService = new ReportService(_mockUnitOfWork.Object, _mockRepository.Object, _mockPersonRepository.Object, _mapper, _mockReporterClientService.Object, _mockContactInformationRepository.Object, _mockLogger.Object);
            _controller = new ReportController(_reportService);
            _persons = SampleData.personData;
            _contacts = SampleData.contactInformationData;
            _reports = SampleData.reportData;
        }

        [Fact]
        public void Get_ActionExecutes_ReturnOkResultWithReports()
        {
            _mockRepository.Setup(nq => nq.Where(null)).Returns(_reports.AsQueryable());
            var result = _controller.Get();
            var actionResult = Assert.IsAssignableFrom<ActionResult<List<ReportDto>>>(result);
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var returnReports = Assert.IsAssignableFrom<List<ReportDto>>(okResult.Value);
            Assert.True(returnReports.Count > 0);
        }

        [Fact]
        public void Get_ActionExecutesWithZeroData_ReturnNoContent()
        {
            _mockRepository.Setup(nq => nq.Where(null)).Returns(new List<Report>().AsQueryable());
            var result = _controller.Get();
            var actionResult = Assert.IsAssignableFrom<ActionResult<List<ReportDto>>>(result);
            Assert.IsAssignableFrom<NoContentResult>(actionResult.Result);
        }




        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void Get_IdValid_ReturnOkResultWithReport(int reportId)
        {
            var report = _reports.First(nq => nq.ReportId == reportId);
            _mockRepository.Setup(nq => nq.GetByIdAsync(reportId)).ReturnsAsync(report);
            var result = await _controller.Get(reportId);
            var actionResult = Assert.IsAssignableFrom<ActionResult<ReportDto>>(result);
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var returnReport = Assert.IsAssignableFrom<ReportDto>(okResult.Value);
            Assert.Equal(reportId, returnReport.Id);
        }

        [Fact]
        public async void Get_IdInValid_ReturnNotFound()
        {
            Report report = null;
            _mockRepository.Setup(nq => nq.GetByIdAsync(0)).ReturnsAsync(report);
            var result = await _controller.Get(0);
            var actionResult = Assert.IsAssignableFrom<ActionResult<ReportDto>>(result);
            Assert.IsAssignableFrom<NotFoundResult>(actionResult.Result);
        }




        [Theory]
        [InlineData(1)]
        public async void Delete_IdIsNotEqualReport_IntervalServerErrorResult(int reportId)
        {
            Report report = null;
            _mockRepository.Setup(nq => nq.GetByIdAsync(reportId)).ReturnsAsync(report);
            var result = await _controller.Delete(reportId);
            var objectResult = Assert.IsAssignableFrom<ObjectResult>(result);
            var exceptionResult = Assert.IsType<Exception>(objectResult.Value);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult?.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public async void Delete_ActionExecutes_ReturnNoContent(int reportId)
        {
            var reportDto = new ReportDto() { Id = reportId, CreatedTime = DateTime.Now, FilePath = "", ReportStatus = Store.Enums.ReportStatus.Completed, RequestTime = DateTime.Now };
            var report = _mapper.Map<Report>(reportDto);
            _mockRepository.Setup(nq => nq.GetByIdAsync(reportId)).ReturnsAsync(report);
            _mockRepository.Setup(nq => nq.Remove(report));
            var result = await _controller.Delete(reportId);
            var actionResult = Assert.IsAssignableFrom<StatusCodeResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, actionResult?.StatusCode);
        }



        [Fact]
        public void GetReportData_ActionExecutes_ReturnOkResultWithReportData()
        {
            var personWithInformation = _persons.ToList();
            foreach (var item in personWithInformation)
                item.ContactInformations = _contacts.Where(nq => nq.PersonId == item.PersonId).ToList();
            var contactInformations = _contacts.Where(nq => nq.InformationType == Store.Enums.ContactInformationType.Location);
            _mockContactInformationRepository.Setup(nq => nq.Where(sq => sq.InformationType == Store.Enums.ContactInformationType.Location)).Returns(contactInformations.AsQueryable());

            foreach (var location in contactInformations.Select(nq => nq.InformationContent).Distinct().ToList())
            {
                var persons = personWithInformation.Where(nq => nq.ContactInformations.Any(sq => sq.InformationType == Store.Enums.ContactInformationType.Location && sq.InformationContent == location));
                _mockPersonRepository.Setup(nq => nq.Where(sq => sq.ContactInformations.Any(x => x.InformationType == Store.Enums.ContactInformationType.Location && x.InformationContent == location))).Returns(persons.AsQueryable());
            }

            var result = _controller.GetReportData();
            var actionResult = Assert.IsAssignableFrom<ActionResult<List<ReportDataDto>>>(result);
            var objectResult = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var returnReports = Assert.IsAssignableFrom<List<ReportDataDto>>(objectResult.Value);
            Assert.Equal(StatusCodes.Status200OK, objectResult?.StatusCode);
            Assert.True(returnReports.Count > 0);
        }

        //public void CreateReport_ActionExecutes_ReturnCreatedResult()
        //{
        //    _mockRepository.Setup(nq => nq.AddAsync(new Report() { ReportStatus = Store.Enums.ReportStatus.ToBe, RequestTime = DateTime.Now })).Returns(Task.CompletedTask);

        //}

        //CreateReport
        //CompleteReport
    }
}
