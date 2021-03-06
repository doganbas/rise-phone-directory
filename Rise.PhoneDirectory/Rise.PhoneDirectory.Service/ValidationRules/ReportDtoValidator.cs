using FluentValidation;
using Rise.PhoneDirectory.Store.Dtos;

namespace Rise.PhoneDirectory.Service.ValidationRules
{
    public class ReportDtoValidator : AbstractValidator<ReportDto>
    {
        public ReportDtoValidator()
        {
            RuleFor(nq => nq.RequestTime).NotEmpty();
            RuleFor(nq => nq.RequestTime).GreaterThan(DateTime.Now.AddDays(-1));
            RuleFor(nq => nq.ReportStatus).IsInEnum().NotNull();
            RuleFor(nq => nq.CreatedTime).GreaterThan(nq => nq.RequestTime);
            RuleFor(nq => nq.FilePath).NotEmpty().When(nq => nq.CreatedTime != null);
        }
    }
}
