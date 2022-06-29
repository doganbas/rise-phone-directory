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
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<ContactInformationController> _logger;

        public PersonController(IPersonService service, IMapper mapper, ILogger<ContactInformationController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<PersonDto>> Get()
        {
            var persons = await _service.Where().ToListAsync();
            if (!persons.Any())
                return NoContent();
            return Ok(_mapper.Map<List<PersonDto>>(persons));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> Get(int id)
        {
            var person = await _service.GetByIdAsync(id);
            if (person == null)
                return NotFound();
            return Ok(_mapper.Map<PersonDto>(person));
        }

        [HttpPost]
        public async Task<ActionResult<PersonDto>> Post([FromBody] PersonDto personDto)
        {
            var person = await _service.AddAsync(_mapper.Map<Person>(personDto));
            return StatusCode(StatusCodes.Status201Created, _mapper.Map<PersonDto>(person));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PersonDto personDto)
        {
            if (personDto.Id != id)
                return StatusCode(StatusCodes.Status400BadRequest, ProjectConst.PutIdError);
            await _service.UpdateAsync(_mapper.Map<Person>(personDto));
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var person = await _service.GetByIdAsync(id);
            if (person == null)
                return NotFound();
            await _service.DeleteAsync(person);

            _logger.LogInformation(string.Format(ProjectConst.DeleteLogMessage, typeof(Person).Name), person);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpGet("[action]/{personId}")]
        public async Task<ActionResult<PersonWithContactInfoDto>> GetPersonByIdWithContactInformation(int personId)
        {
            var personWithContactInformation = await _service.GetPersonByIdWithContactInformationAsync(personId);
            return Ok(personWithContactInformation);
        }
    }
}