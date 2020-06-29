using FluentValidation;

namespace Hahn.ApplicationProcess.May2020.Domain.Common.Entities
{
    public class ApplicantValidator:AbstractValidator<Applicant>
    {
        public ApplicantValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(5).WithMessage("Name must be at least 5 Characters.");
            
            RuleFor(v=>v.FamilyName)
                .NotEmpty().WithMessage("Family Name is required.")
                .MinimumLength(5).WithMessage("Family Name must be at least 5 Characters.");

            RuleFor(v=>v.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MinimumLength(5).WithMessage("Address must be at least 10 Characters.");

            RuleFor(v => v.CountryOfOrigin)
                .NotEmpty().WithMessage("CountryOfOrigin is required.");

            RuleFor(v => v.EMailAddress)
                .NotEmpty().WithMessage("EMailAddress is required.")
                .EmailAddress().WithMessage("EmailAddress must be an valid email");

            RuleFor(v => v.Age)
                .NotNull().WithMessage("Age is required.")
                .InclusiveBetween(20, 60).WithMessage("Age must be between 20 and 60.");

            RuleFor(v => v.Hired)
                .NotNull().WithMessage("Hired is required");

            RuleFor(v => v.CountryIsExisted)
                .Equal(true).WithMessage("Country of Origin must be a valid Country");

        }
    }
}