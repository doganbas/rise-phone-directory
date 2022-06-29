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
    public class ContactInformationController : ControllerBase
    {
        private readonly IGenericService<ContactInformation> _service;
        private readonly IMapper _mapper;
        private readonly ILogger<ContactInformationController> _logger;

        public ContactInformationController(IGenericService<ContactInformation> service, IMapper mapper, ILogger<ContactInformationController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<ContactInformationDto>> Get()
        {
            var contactInforations = await _service.Where().ToListAsync();
            if (!contactInforations.Any())
                return NoContent();
            return Ok(_mapper.Map<List<ContactInformationDto>>(contactInforations));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactInformationDto>> Get(int id)
        {
            var contactInformation = await _service.GetByIdAsync(id);
            if (contactInformation == null)
                return NotFound();
            return Ok(_mapper.Map<ContactInformationDto>(contactInformation));
        }

        [HttpPost]
        public async Task<ActionResult<ContactInformationDto>> Post([FromBody] ContactInformationDto personDto)
        {
            var contactInformation = await _service.AddAsync(_mapper.Map<ContactInformation>(personDto));
            return StatusCode(StatusCodes.Status201Created, _mapper.Map<ContactInformationDto>(contactInformation));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ContactInformationDto contactInformationDto)
        {
            if (contactInformationDto.Id != id)
                return StatusCode(StatusCodes.Status400BadRequest, ProjectConst.PutIdError);
            await _service.UpdateAsync(_mapper.Map<ContactInformation>(contactInformationDto));
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var person = await _service.GetByIdAsync(id);
            if (person == null)
                return NotFound();
            await _service.DeleteAsync(person);

            _logger.LogInformation(string.Format(ProjectConst.DeleteLogMessage, typeof(ContactInformation).Name), person);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}