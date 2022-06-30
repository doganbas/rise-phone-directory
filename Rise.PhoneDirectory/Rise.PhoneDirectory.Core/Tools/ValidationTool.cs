using FluentValidation;

namespace Rise.PhoneDirectory.Core.Tools
{
    public static class ValidationTool
    {
        public static void Validate(IValidator validator, object data)
        {
            var result = validator.Validate(new ValidationContext<object>(data));
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        }
    }
}
