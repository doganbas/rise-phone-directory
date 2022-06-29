using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rise.PhoneDirectory.Core.Constants;
using Rise.PhoneDirectory.Core.Services;
using Rise.PhoneDirectory.Store.Dtos;
using Rise.PhoneDirectory.Store.Models;

namespace Rise.PhoneDirectory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<ReportController> _logger;

        public ReportController(IReportService service, IMapper mapper, ILogger<ReportController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ReportDto>> Get()
        {
            var reports = await _service.Where().ToListAsync();
            if (!reports.Any())
                return NoContent();
            return Ok(_mapper.Map<List<ReportDto>>(reports));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReportDto>> Get(int id)
        {
            var report = await _service.GetByIdAsync(id);
            if (report == null)
                return NotFound();
            return Ok(_mapper.Map<ReportDto>(report));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var report = await _service.GetByIdAsync(id);
            if (report == null)
                return NotFound();
            await _service.DeleteAsync(report);
            _logger.LogInformation(string.Format(ProjectConst.DeleteLogMessage, typeof(Report).Name), report);
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
            if (report != null && report.ReportId > 0)
                await _service.ReportExcelAsync(report.ReportId);
            _logger.LogInformation(ProjectConst.ExcelReportServiceRequestNew, report);
            return StatusCode(StatusCodes.Status201Created, _mapper.Map<ReportDto>(report));
        }
    }
}