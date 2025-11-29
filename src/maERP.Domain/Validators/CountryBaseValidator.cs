using FluentValidation;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Validators;

public class CountryBaseValidator<T> : AbstractValidator<T> where T : ICountryInputModel
{
    public CountryBaseValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(p => p.CountryCode)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(3).WithMessage("{PropertyName} must not exceed 3 characters.");
    }
}
