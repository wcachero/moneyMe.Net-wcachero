using FluentValidation;
using WebApplication2.Dtos;

namespace WebApplication2.Validators
{
    public class BlacklistedPhoneNumbersDtoValidator : AbstractValidator<BlacklistedPhoneNumbersDto>
    {
        public BlacklistedPhoneNumbersDtoValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .MinimumLength(7).WithMessage("Phone number must have at least 7 digits.")
                .Matches(@"^\d+$").WithMessage("Phone number must contain only numbers.");
        }
    }
}