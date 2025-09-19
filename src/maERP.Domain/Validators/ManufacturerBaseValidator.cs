using FluentValidation;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Validators;

public class ManufacturerBaseValidator<T> : AbstractValidator<T> where T : IManufacturerInputModel
{
    public ManufacturerBaseValidator()
    {
        RuleFor(p => p.Name)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MinimumLength(1).WithMessage("{PropertyName} must be at least 1 character.")
            .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters.");

        RuleFor(p => p.Street)
            .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters.");

        RuleFor(p => p.City)
            .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters.");

        RuleFor(p => p.State)
            .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters.");

        RuleFor(p => p.Country)
            .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters.");

        RuleFor(p => p.ZipCode)
            .MaximumLength(20).WithMessage("{PropertyName} must not exceed 20 characters.");

        RuleFor(p => p.Phone)
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(p => p.Email)
            .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters.");

        RuleFor(p => p.Email)
            .EmailAddress().When(p => !string.IsNullOrWhiteSpace(p.Email))
            .WithMessage("A valid email address is required.");

        RuleFor(p => p.Website)
            .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

        RuleFor(p => p.Website)
            .Must(BeValidUrl).When(p => !string.IsNullOrWhiteSpace(p.Website))
            .WithMessage("Website must be a valid URL.");

        RuleFor(p => p.Logo)
            .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");
    }

    private bool BeValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var result)
            && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
    }
}