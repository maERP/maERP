using FluentValidation;
using maERP.Domain.Enums;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Validators;

public class SalesChannelBaseValidator<T> : AbstractValidator<T> where T : ISalesChannelInputModel
{
    public SalesChannelBaseValidator()
    {
        // SalesChannelType must be a valid enum value
        RuleFor(p => p.SalesChannelType)
            .IsInEnum().WithMessage("{PropertyName} is invalid.");

        // Name is always required
        RuleFor(p => p.Name)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        // URL is required only for Shopware5, Shopware6, WooCommerce
        RuleFor(p => p.Url)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Must(BeAValidUrl).WithMessage("{PropertyName} must be a valid URL.")
            .When(p => RequiresUrl(p.SalesChannelType));

        // Username is required for all types except PointOfSale
        RuleFor(p => p.Username)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .When(p => RequiresCredentials(p.SalesChannelType));

        // Password is required for all types except PointOfSale
        RuleFor(p => p.Password)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .When(p => RequiresCredentials(p.SalesChannelType));
    }

    private static bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }

    private static bool RequiresUrl(SalesChannelType type)
    {
        return type is SalesChannelType.Shopware5
            or SalesChannelType.Shopware6
            or SalesChannelType.WooCommerce;
    }

    private static bool RequiresCredentials(SalesChannelType type)
    {
        return type != SalesChannelType.PointOfSale;
    }
}