using FluentValidation;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Validators;

public class SalesChannelBaseValidator<T> : AbstractValidator<T> where T : ISalesChannelInputModel
{
    public SalesChannelBaseValidator()
    {
        RuleFor(p => p.Name)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(p => p.Url)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Must(BeAValidUrl).WithMessage("{PropertyName} must be a valid URL.");
    }

    private bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}