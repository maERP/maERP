using FluentValidation;
using maERP.Domain.Enums;

namespace maERP.Application.Features.TenantEmailSettings.Commands.TenantEmailSettingsUpsert;

public class TenantEmailSettingsUpsertValidator : AbstractValidator<TenantEmailSettingsUpsertCommand>
{
    public TenantEmailSettingsUpsertValidator()
    {
        RuleFor(c => c.ProviderType)
            .IsInEnum().WithMessage("Provider type must be a valid value.");

        RuleFor(c => c.FromAddress)
            .EmailAddress().When(c => !string.IsNullOrWhiteSpace(c.FromAddress))
            .WithMessage("FromAddress must be a valid email address.");

        RuleFor(c => c.ReplyToAddress)
            .EmailAddress().When(c => !string.IsNullOrWhiteSpace(c.ReplyToAddress))
            .WithMessage("ReplyToAddress must be a valid email address.");

        RuleFor(c => c.M365SenderAddress)
            .EmailAddress().When(c => !string.IsNullOrWhiteSpace(c.M365SenderAddress))
            .WithMessage("M365SenderAddress must be a valid email address.");

        RuleFor(c => c.SmtpPort)
            .InclusiveBetween(1, 65535).When(c => c.SmtpPort.HasValue)
            .WithMessage("SmtpPort must be between 1 and 65535.");

        // Provider-specific minimum coverage when the tenant override is the sole source.
        // We only enforce required fields when the tenant explicitly enables the configuration.
        When(c => c.IsActive && c.ProviderType == EmailProviderType.Smtp, () =>
        {
            RuleFor(c => c.SmtpHost)
                .NotEmpty().WithMessage("SmtpHost is required for SMTP when no server-level value will be inherited.")
                .When(c => c.SmtpPort.HasValue || !string.IsNullOrWhiteSpace(c.SmtpUsername));
        });

        When(c => c.IsActive && c.ProviderType == EmailProviderType.Microsoft365, () =>
        {
            RuleFor(c => c.M365TenantId)
                .NotEmpty().WithMessage("M365TenantId is required when overriding Microsoft 365 credentials at the tenant level.")
                .When(c => !string.IsNullOrWhiteSpace(c.M365ClientId) || !string.IsNullOrWhiteSpace(c.M365ClientSecret));

            RuleFor(c => c.M365ClientId)
                .NotEmpty().WithMessage("M365ClientId is required when overriding Microsoft 365 credentials at the tenant level.")
                .When(c => !string.IsNullOrWhiteSpace(c.M365TenantId) || !string.IsNullOrWhiteSpace(c.M365ClientSecret));
        });
    }
}
