using Microsoft.AspNetCore.Mvc;
using Rise.PhoneDirectory.Core.Services;
using Rise.PhoneDirectory.Store.Dtos;

namespace Rise.PhoneDirectory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<ReportDto> Get()
        {
            var reports = _service.Where().ToList();

            if (!reports.Any())
                return NoContent();

            return Ok(reports);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReportDto>> Get(int id)
        {
            var report = await _service.GetByIdAsync(id);

            if (report == null)
                return NotFound();

            return Ok(report);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.RemoveAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpGet("GetReportData")]
        public ActionResult<ReportDataDto> GetReportData()
        {
            var reportData = _service.GetReportData();
            return StatusCode(StatusCodes.Status200OK, reportData);
        }

        [HttpPost("CreateReport")]
        public async Task<ActionResult<ReportDto>> CreateReport()
        {
            var report = await _service.AddAsync(new()
            {
                ReportStatus = Store.Enums.ReportStatus.ToBe,
                RequestTime = DateTime.Now
            });

            if (report != null && report.Id > 0)
                await _service.ReportExcelAsync(report.Id);

            return StatusCode(StatusCodes.Status201Created, report);
        }

        [HttpPost("CompleteReport/{reportId}")]
        public async Task<ActionResult> CompleteReport(IFormFile reportFile, int reportId)
        {
            var reportResult = await _service.CompleteReportAsync(reportFile, reportId);

            if (!reportResult)
                return StatusCode(StatusCodes.Status400BadRequest);

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}