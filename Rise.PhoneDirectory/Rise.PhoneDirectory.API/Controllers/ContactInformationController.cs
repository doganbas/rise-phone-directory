using Microsoft.AspNetCore.Mvc;
using Rise.PhoneDirectory.Core.Constants;
using Rise.PhoneDirectory.Core.Services;
using Rise.PhoneDirectory.Store.Dtos;

namespace Rise.PhoneDirectory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactInformationController : ControllerBase
    {
        private readonly IContactInformationService _service;

        public ContactInformationController(IContactInformationService service)
        {
            _service = service;
        }


        [HttpGet]
        public ActionResult<ContactInformationDto> Get()
        {
            var contactInforations = _service.Where().ToList();

            if (!contactInforations.Any())
                return NoContent();

            return Ok(contactInforations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactInformationDto>> Get(int id)
        {
            var contactInformation = await _service.GetByIdAsync(id);

            if (contactInformation == null)
                return NotFound();

            return Ok(contactInformation);
        }

        [HttpPost]
        public async Task<ActionResult<ContactInformationDto>> Post([FromBody] ContactInformationDto contactInformationDto)
        {
            var contactInformation = await _service.AddAsync(contactInformationDto);

            return StatusCode(StatusCodes.Status201Created, contactInformation);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ContactInformationDto contactInformationDto)
        {
            if (contactInformationDto.Id != id)
                return StatusCode(StatusCodes.Status400BadRequest, ProjectConst.PutIdError);

            await _service.UpdateAsync(contactInformationDto);

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.RemoveAsync(id);

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}