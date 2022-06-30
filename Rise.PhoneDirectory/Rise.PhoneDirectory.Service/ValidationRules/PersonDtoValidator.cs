using FluentValidation;
using Rise.PhoneDirectory.Store.Dtos;

namespace Rise.PhoneDirectory.Service.ValidationRules
{
    public class PersonDtoValidator : AbstractValidator<PersonDto>
    {
        public PersonDtoValidator()
        {
            RuleFor(nq => nq.Name).NotEmpty();
            RuleFor(nq => nq.Name).Length(2, 150);

            RuleFor(nq => nq.Surname).MaximumLength(150);
            RuleFor(nq => nq.CompanyName).MaximumLength(250);
        }
    }
}