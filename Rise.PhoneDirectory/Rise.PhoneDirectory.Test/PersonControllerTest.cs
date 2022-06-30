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
    public class PersonControllerTest
    {
        private readonly Mock<IPersonRepository> _mockRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ILogger<PersonService>> _mockLogger;
        private readonly IPersonService _personService;
        private readonly PersonController _controller;
        private readonly IMapper _mapper;
        private readonly List<Person> _persons;
        private readonly List<ContactInformation> _contacts;

        public PersonControllerTest()
        {
            _mapper = new MapperConfiguration(mc => { mc.AddProfile(new MapProfile()); }).CreateMapper();
            _mockRepository = new Mock<IPersonRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockLogger = new Mock<ILogger<PersonService>>();
            _personService = new PersonService(_mockRepository.Object, _mockUnitOfWork.Object, _mapper, _mockLogger.Object);
            _controller = new PersonController(_personService);
            _persons = SampleData.personData;
            _contacts = SampleData.contactInformationData;
        }

        [Fact]
        public void Get_ActionExecutes_ReturnOkResultWithPersons()
        {
            _mockRepository.Setup(nq => nq.Where(null)).Returns(_persons.AsQueryable());
            var result = _controller.Get();
            var actionResult = Assert.IsAssignableFrom<ActionResult<List<PersonDto>>>(result);
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var returnPersons = Assert.IsAssignableFrom<List<PersonDto>>(okResult.Value);
            Assert.True(returnPersons.Count > 0);
        }

        [Fact]
        public void Get_ActionExecutesWithZeroData_ReturnNoContent()
        {
            _mockRepository.Setup(nq => nq.Where(null)).Returns(new List<Person>().AsQueryable());
            var result = _controller.Get();
            var actionResult = Assert.IsAssignableFrom<ActionResult<List<PersonDto>>>(result);
            Assert.IsAssignableFrom<NoContentResult>(actionResult.Result);
        }



        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void Get_IdValid_ReturnOkResultWithPerson(int personId)
        {
            var person = _persons.First(nq => nq.PersonId == personId);
            _mockRepository.Setup(nq => nq.GetByIdAsync(personId)).ReturnsAsync(person);
            var result = await _controller.Get(personId);
            var actionResult = Assert.IsAssignableFrom<ActionResult<PersonDto>>(result);
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var returnPerson = Assert.IsAssignableFrom<PersonDto>(okResult.Value);
            Assert.Equal(personId, returnPerson.Id);
        }

        [Fact]
        public async void Get_IdInValid_ReturnNotFound()
        {
            Person person = null;
            _mockRepository.Setup(nq => nq.GetByIdAsync(0)).ReturnsAsync(person);
            var result = await _controller.Get(0);
            var actionResult = Assert.IsAssignableFrom<ActionResult<PersonDto>>(result);
            Assert.IsAssignableFrom<NotFoundResult>(actionResult.Result);
        }



        [Theory]
        [InlineData(1, 2)]
        public async void Put_IdIsNotEqualPerson_BadRequestResult(int personId, int testPersonId)
        {
            var person = _mapper.Map<PersonDto>(_persons.First(nq => nq.PersonId == personId));
            var result = await _controller.Put(testPersonId, person);
            var actionResult = Assert.IsAssignableFrom<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, actionResult?.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public async void Put_NameSurnameNotUniqe_UnprocessableEntityResult(int personId)
        {
            var personDto = new PersonDto() { Id = personId, Name = "Diana", Surname = "Edmunds", CompanyName = "What You Will Yoga Inc." };
            _mockRepository.Setup(nq => nq.AnyAsync(sq => sq.PersonId != personDto.Id && sq.Name == personDto.Name && sq.Surname == personDto.Surname)).ReturnsAsync(true);
            var result = await _controller.Put(personId, personDto);
            var actionResult = Assert.IsAssignableFrom<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status422UnprocessableEntity, actionResult?.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public async void Put_ActionExecutes_ReturnNoContent(int personId)
        {
            var personDto = new PersonDto() { Id = personId, Name = "Carol", Surname = "Edmunds", CompanyName = "What You Will Yoga Inc." };
            var person = _mapper.Map<Person>(personDto);
            _mockRepository.Setup(nq => nq.AnyAsync(sq => sq.PersonId != personDto.Id && sq.Name == personDto.Name && sq.Surname == personDto.Surname)).ReturnsAsync(false);
            _mockRepository.Setup(nq => nq.Update(person));
            var result = await _controller.Put(personId, personDto);
            var actionResult = Assert.IsAssignableFrom<StatusCodeResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, actionResult?.StatusCode);
        }



        [Fact]
        public async void Post_ActionExecutes_ReturnCreatedWithPerson()
        {
            var personDto = new PersonDto() { Id = 0, Name = "Test", Surname = "User", CompanyName = "What You Will Yoga Inc." };
            var person = _mapper.Map<Person>(personDto);
            _mockRepository.Setup(nq => nq.AddAsync(person)).Returns(Task.CompletedTask);
            var result = await _controller.Post(personDto);
            var actionResult = Assert.IsAssignableFrom<ActionResult<PersonDto>>(result);
            var createdResult = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var returnPerson = Assert.IsAssignableFrom<PersonDto>(createdResult.Value);
            Assert.Equal(personDto.Id, returnPerson.Id);
        }


        [Theory]
        [InlineData(1)]
        public async void Delete_IdIsNotEqualPerson_IntervalServerErrorResult(int personId)
        {
            Person person = null;
            _mockRepository.Setup(nq => nq.GetByIdAsync(personId)).ReturnsAsync(person);
            var result = await _controller.Delete(personId);
            var objectResult = Assert.IsAssignableFrom<ObjectResult>(result);
            var exceptionResult = Assert.IsType<Exception>(objectResult.Value);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult?.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public async void Delete_ActionExecutes_ReturnNoContent(int personId)
        {
            var personDto = new PersonDto() { Id = personId, Name = "Diana", Surname = "Edmunds", CompanyName = "What You Will Yoga Inc." };
            var person = _mapper.Map<Person>(personDto);
            _mockRepository.Setup(nq => nq.GetByIdAsync(personId)).ReturnsAsync(person);
            _mockRepository.Setup(nq => nq.Remove(person));
            var result = await _controller.Delete(personId);
            var actionResult = Assert.IsAssignableFrom<StatusCodeResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, actionResult?.StatusCode);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetPersonByIdWithContactInformation_ActionExecutes_ReturnOkResultWithPersonWithContactInformation(int personId)
        {
            var personWithContactInformation = _persons.First(nq => nq.PersonId == personId);
            personWithContactInformation.ContactInformations = _contacts.Where(nq => nq.PersonId == personId).ToList();
            _mockRepository.Setup(nq => nq.GetPersonByIdWithContactInformationAsync(personId)).ReturnsAsync(personWithContactInformation);
            var result = await _controller.GetPersonByIdWithContactInformation(personId);
            var actionResult = Assert.IsAssignableFrom<ActionResult<PersonWithContactInfoDto>>(result);
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var returnPerson = Assert.IsAssignableFrom<PersonWithContactInfoDto>(okResult.Value);
            Assert.Equal(personId, returnPerson.Id);
            Assert.True(returnPerson.ContactInformation.Any());
        }
    }
}