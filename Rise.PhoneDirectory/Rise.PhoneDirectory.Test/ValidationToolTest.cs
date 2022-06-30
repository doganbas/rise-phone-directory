using Rise.PhoneDirectory.Core.Tools;
using Rise.PhoneDirectory.Service.ValidationRules;
using Rise.PhoneDirectory.Store.Dtos;

namespace Rise.PhoneDirectory.Test
{
    public class ValidationToolTest
    {
        private readonly PersonDtoValidator _personValidator;
        private readonly ContactInformationDtoValidator _contactInformationValidator;
        private readonly ReportDtoValidator _reportValidator;

        public ValidationToolTest()
        {
            _personValidator = new PersonDtoValidator();
            _contactInformationValidator = new ContactInformationDtoValidator();
            _reportValidator = new ReportDtoValidator();
        }

        [Fact]
        public void Validate_PersonWithValid_Return()
        {
            var testData = new PersonDto()
            {
                CompanyName = "Test Company Name",
                Name = "Test",
                Surname = "User",
                Id = 0
            };
            ValidationTool.Validate(_personValidator, testData);
        }

        [Fact]
        public void Validate_PersonWithNullName_ReturnThrowValidationException()
        {
            var testData = new PersonDto()
            {
                CompanyName = "Test Company Name",
                Name = null,
                Surname = "User",
                Id = 0
            };
            Action act = () => ValidationTool.Validate(_personValidator, testData);
            Exception validationException = Record.Exception(act);
            Assert.True(!string.IsNullOrEmpty(validationException.Message));
        }



        [Fact]
        public void Validate_ContactInformationWithValid_Return()
        {
            var testData = new ContactInformationDto()
            {
                Id = 0,
                PersonId = 1,
                InformationType = Store.Enums.ContactInformationType.Location,
                InformationContent = "Test Content"
            };
            ValidationTool.Validate(_contactInformationValidator, testData);
        }

        [Fact]
        public void Validate_ContactInformationWithNullContent_ReturnThrowValidationException()
        {
            var testData = new ContactInformationDto()
            {
                Id = 0,
                PersonId = 1,
                InformationType = Store.Enums.ContactInformationType.Location
            };
            Action act = () => ValidationTool.Validate(_contactInformationValidator, testData);
            Exception validationException = Record.Exception(act);
            Assert.True(!string.IsNullOrEmpty(validationException.Message));
        }



        [Fact]
        public void Validate_ReportWithValid_Return()
        {
            var testData = new ReportDto()
            {
                Id = 0,
                RequestTime = DateTime.Now,
                ReportStatus = Store.Enums.ReportStatus.ToBe
            };
            ValidationTool.Validate(_reportValidator, testData);
        }

        [Fact]
        public void Validate_ReportWithNullFile_ReturnThrowValidationException()
        {
            var testData = new ReportDto()
            {
                Id = 0,
                RequestTime = DateTime.Now,
                ReportStatus = Store.Enums.ReportStatus.ToBe,
                CreatedTime = DateTime.Now.AddMinutes(1),
            };
            Action act = () => ValidationTool.Validate(_reportValidator, testData);
            Exception validationException = Record.Exception(act);
            Assert.True(!string.IsNullOrEmpty(validationException.Message));
        }


    }
}