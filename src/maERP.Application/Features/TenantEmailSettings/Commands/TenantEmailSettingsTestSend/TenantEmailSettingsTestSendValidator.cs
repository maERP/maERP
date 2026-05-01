using FluentValidation;

namespace maERP.Application.Features.TenantEmailSettings.Commands.TenantEmailSettingsTestSend;

public class TenantEmailSettingsTestSendValidator : AbstractValidator<TenantEmailSettingsTestSendCommand>
{
    public TenantEmailSettingsTestSendValidator()
    {
        RuleFor(c => c.Recipient)
            .NotEmpty().WithMessage("Recipient is required.")
            .EmailAddress().WithMessage("Recipient must be a valid email address.");
    }
}
