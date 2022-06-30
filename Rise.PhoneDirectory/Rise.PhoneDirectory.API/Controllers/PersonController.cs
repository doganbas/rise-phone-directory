using Microsoft.AspNetCore.Mvc;
using Rise.PhoneDirectory.Core.Constants;
using Rise.PhoneDirectory.Core.Services;
using Rise.PhoneDirectory.Store.Dtos;

namespace Rise.PhoneDirectory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _service;

        public PersonController(IPersonService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<PersonDto>> Get()
        {
            var persons = _service.Where().ToList();

            if (!persons.Any())
                return NoContent();

            return Ok(persons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> Get(int id)
        {
            var person = await _service.GetByIdAsync(id);

            if (person == null)
                return NotFound();

            return Ok(person);
        }

        [HttpPost]
        public async Task<ActionResult<PersonDto>> Post([FromBody] PersonDto personDto)
        {
            try
            {
                var person = await _service.AddAsync(personDto);
                return StatusCode(StatusCodes.Status201Created, person);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PersonDto personDto)
        {
            if (personDto.Id != id)
                return StatusCode(StatusCodes.Status400BadRequest, ProjectConst.PutIdError);

            try
            {
                await _service.UpdateAsync(personDto);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _service.RemoveAsync(id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("[action]/{personId}")]
        public async Task<ActionResult<PersonWithContactInfoDto>> GetPersonByIdWithContactInformation(int personId)
        {
            var personWithContactInformation = await _service.GetPersonByIdWithContactInformationAsync(personId);
            return Ok(personWithContactInformation);
        }
    }
}