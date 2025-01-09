using FluentValidation;
using LoanApplication.Dto;

namespace LoanApplication.Validators
{
    public class BlackListedEmailDomainValidator : AbstractValidator<BlackListedEmailDomainDto>
    {
        public BlackListedEmailDomainValidator()
        {
            RuleFor(x => x.DomainName)
                .NotEmpty().WithMessage("Email is required.");
        }
    }
}
