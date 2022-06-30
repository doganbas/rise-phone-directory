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
    public class ContactInformationControllerTest
    {
        private readonly Mock<IGenericRepository<ContactInformation>> _mockRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ILogger<ContactInformationService>> _mockLogger;
        private readonly IContactInformationService _contactInformationService;
        private readonly ContactInformationController _controller;
        private readonly IMapper _mapper;
        private readonly List<Person> _persons;
        private readonly List<ContactInformation> _contacts;

        public ContactInformationControllerTest()
        {
            _mapper = new MapperConfiguration(mc => { mc.AddProfile(new MapProfile()); }).CreateMapper();
            _mockRepository = new Mock<IGenericRepository<ContactInformation>>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockLogger = new Mock<ILogger<ContactInformationService>>();
            _contactInformationService = new ContactInformationService(_mockRepository.Object, _mockUnitOfWork.Object, _mapper, _mockLogger.Object);
            _controller = new ContactInformationController(_contactInformationService);
            _persons = SampleData.personData;
            _contacts = SampleData.contactInformationData;
        }

        [Fact]
        public void Get_ActionExecutes_ReturnOkResultWithContactInformations()
        {
            _mockRepository.Setup(nq => nq.Where(null)).Returns(_contacts.AsQueryable());
            var result = _controller.Get();
            var actionResult = Assert.IsAssignableFrom<ActionResult<List<ContactInformationDto>>>(result);
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var returnContacts = Assert.IsAssignableFrom<List<ContactInformationDto>>(okResult.Value);
            Assert.True(returnContacts.Count > 0);
        }

        [Fact]
        public void Get_ActionExecutesWithZeroData_ReturnNoContent()
        {
            _mockRepository.Setup(nq => nq.Where(null)).Returns(new List<ContactInformation>().AsQueryable());
            var result = _controller.Get();
            var actionResult = Assert.IsAssignableFrom<ActionResult<List<ContactInformationDto>>>(result);
            Assert.IsAssignableFrom<NoContentResult>(actionResult.Result);
        }



        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void Get_IdValid_ReturnOkResultWithContactInformation(int contactInformationId)
        {
            var contact = _contacts.First(nq => nq.ContactInformationId == contactInformationId);
            _mockRepository.Setup(nq => nq.GetByIdAsync(contactInformationId)).ReturnsAsync(contact);
            var result = await _controller.Get(contactInformationId);
            var actionResult = Assert.IsAssignableFrom<ActionResult<ContactInformationDto>>(result);
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var returnContactInformation = Assert.IsAssignableFrom<ContactInformationDto>(okResult.Value);
            Assert.Equal(contactInformationId, returnContactInformation.Id);
        }

        [Fact]
        public async void Get_IdInValid_ReturnNotFound()
        {
            ContactInformation contact = null;
            _mockRepository.Setup(nq => nq.GetByIdAsync(0)).ReturnsAsync(contact);
            var result = await _controller.Get(0);
            var actionResult = Assert.IsAssignableFrom<ActionResult<ContactInformationDto>>(result);
            Assert.IsAssignableFrom<NotFoundResult>(actionResult.Result);
        }



        [Theory]
        [InlineData(1, 2)]
        public async void Put_IdIsNotEqualContactInformation_BadRequestResult(int contactInformationId, int testContactInformationId)
        {
            var contact = _mapper.Map<ContactInformationDto>(_contacts.First(nq => nq.ContactInformationId == contactInformationId));
            var result = await _controller.Put(testContactInformationId, contact);
            var actionResult = Assert.IsAssignableFrom<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, actionResult?.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public async void Put_ActionExecutes_ReturnNoContent(int contactId)
        {
            var contactInformationDto = new ContactInformationDto() { Id = contactId, InformationType = Store.Enums.ContactInformationType.Location, InformationContent = "Test Content" };
            var contact = _mapper.Map<ContactInformation>(contactInformationDto);
            _mockRepository.Setup(nq => nq.Update(contact));
            var result = await _controller.Put(contactId, contactInformationDto);
            var actionResult = Assert.IsAssignableFrom<StatusCodeResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, actionResult?.StatusCode);
        }



        [Fact]
        public async void Post_ActionExecutes_ReturnCreatedWithContactInformation()
        {
            var contactInformationDto = new ContactInformationDto() { Id = 0, InformationType = Store.Enums.ContactInformationType.Location, InformationContent = "Test Content" };
            var contact = _mapper.Map<ContactInformation>(contactInformationDto);
            _mockRepository.Setup(nq => nq.AddAsync(contact)).Returns(Task.CompletedTask);
            var result = await _controller.Post(contactInformationDto);
            var actionResult = Assert.IsAssignableFrom<ActionResult<ContactInformationDto>>(result);
            var createdResult = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var returnContactInformation = Assert.IsAssignableFrom<ContactInformationDto>(createdResult.Value);
            Assert.Equal(contactInformationDto.Id, returnContactInformation.Id);
        }


        [Theory]
        [InlineData(1)]
        public async void Delete_IdIsNotEqualPerson_IntervalServerErrorResult(int contactId)
        {
            ContactInformation contact = null;
            _mockRepository.Setup(nq => nq.GetByIdAsync(contactId)).ReturnsAsync(contact);
            var result = await _controller.Delete(contactId);
            var objectResult = Assert.IsAssignableFrom<ObjectResult>(result);
            var exceptionResult = Assert.IsType<Exception>(objectResult.Value);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult?.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public async void Delete_ActionExecutes_ReturnNoContent(int contactId)
        {
            var contactInformationDto = new ContactInformationDto() { Id = contactId, InformationType = Store.Enums.ContactInformationType.Location, InformationContent = "Test Content" };
            var contact = _mapper.Map<ContactInformation>(contactInformationDto);
            _mockRepository.Setup(nq => nq.GetByIdAsync(contactId)).ReturnsAsync(contact);
            _mockRepository.Setup(nq => nq.Remove(contact));
            var result = await _controller.Delete(contactId);
            var actionResult = Assert.IsAssignableFrom<StatusCodeResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, actionResult?.StatusCode);
        }
    }
}