using FluentValidation;
using Rise.PhoneDirectory.Store.Dtos;

namespace Rise.PhoneDirectory.Service.ValidationRules
{
    public class ContactInformationDtoValidator : AbstractValidator<ContactInformationDto>
    {
        public ContactInformationDtoValidator()
        {
            RuleFor(nq => nq.PersonId).NotEmpty();
            RuleFor(nq => nq.InformationType).NotEmpty();
            RuleFor(nq => nq.InformationContent).NotEmpty();
            RuleFor(nq => nq.InformationContent).Length(2, 400);
        }
    }
}