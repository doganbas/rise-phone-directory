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
        private readonly IGenericService<Report> _service;
        private readonly IMapper _mapper;
        private readonly ILogger<ReportController> _logger;

        public ReportController(IGenericService<Report> service, IMapper mapper, ILogger<ReportController> logger)
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

        [HttpPost]
        public async Task<ActionResult<ContactInformationDto>> Post([FromBody] ReportDto reportDto)
        {
            var report = await _service.AddAsync(_mapper.Map<Report>(reportDto));
            return StatusCode(StatusCodes.Status201Created, _mapper.Map<ReportDto>(report));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ReportDto reportDto)
        {
            if (reportDto.Id != id)
                return StatusCode(StatusCodes.Status400BadRequest, ProjectConst.PutIdError);
            await _service.UpdateAsync(_mapper.Map<Report>(reportDto));
            return StatusCode(StatusCodes.Status204NoContent);
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

    }
}