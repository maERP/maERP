using FluentValidation;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Validators;

public class CustomerBaseValidator<T> : AbstractValidator<T> where T : ICustomerInputModel
{
    public CustomerBaseValidator()
    {
        RuleFor(p => p.Firstname)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MinimumLength(1).WithMessage("{PropertyName} must be at least 1 character.")
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(p => p.Lastname)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MinimumLength(1).WithMessage("{PropertyName} must be at least 1 character.")
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(p => p.Email)
            .EmailAddress().WithMessage("{PropertyName} is not a valid email address.")
            .When(p => !string.IsNullOrEmpty(p.Email));

        RuleFor(p => p.Website)
            .Must(BeAValidUrl).WithMessage("{PropertyName} must be a valid URL.")
            .When(p => !string.IsNullOrEmpty(p.Website));
    }

    private static bool BeAValidUrl(string url)
    {
        if (string.IsNullOrEmpty(url))
            return true;
        
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}