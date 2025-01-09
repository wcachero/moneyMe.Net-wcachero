using FluentValidation;
using LoanApplication.Dto;
using LoanApplication.Interface;
using System;
using WebApplication2.Services;

namespace WebApplication2.Model
{
    public class CustomerDetailDtoValidator : AbstractValidator<CustomerDetailDto>
    {
        private readonly IBlacklistedPhoneNumbersService _blacklistedPhoneNumbersService;
        private readonly IBlackListedEmaiDomainService _blackListedEmaiDomainService;
        public CustomerDetailDtoValidator(
            IBlacklistedPhoneNumbersService blacklistedPhoneNumbersService,
            IBlackListedEmaiDomainService blackListedEmailDomain
            )

        {
            _blacklistedPhoneNumbersService = blacklistedPhoneNumbersService;
            _blackListedEmaiDomainService = blackListedEmailDomain;

            RuleFor(x => x.RequiredAmount).NotEmpty().WithMessage("RequiredAmount is required.");
            RuleFor(x => x.Term).NotEmpty().WithMessage("Term is required.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
                .Must(EmailNotBlacklisted).WithMessage("The provided Email Domain is blacklisted.");
            RuleFor(x => x.Mobile)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Mobile is required.")
                .MinimumLength(7).WithMessage("Phone number must have at least 7 digits.")
                .Matches(@"^\d+$").WithMessage("Phone number must contain only numbers.")
                .Must(PhoneNotBlacklisted).WithMessage("The provided Mobile is blacklisted.");
            RuleFor(x => x.Product).NotEmpty().WithMessage("Product is required.");
            RuleFor(x => x.DateofBirth)
                .NotEmpty().WithMessage("DateofBirth is required.")
                .Must(BeAtLeast18YearsOld).WithMessage("Customer must be at least 18 years old.");
        }

        private bool BeAtLeast18YearsOld(DateTimeOffset dateOfBirth)
        {
            return dateOfBirth <= DateTimeOffset.Now.AddYears(-18);
        }
        private bool PhoneNotBlacklisted(string mobile)
        {
           return  !_blacklistedPhoneNumbersService.Exists(mobile).GetAwaiter().GetResult();
        }
        private bool EmailNotBlacklisted(string email)
        {
            string domainName = email.Contains('@')
             ? email.Split('@')[1]
             : email;
            return !_blackListedEmaiDomainService.Exists(domainName).GetAwaiter().GetResult();
        }

    }
}
