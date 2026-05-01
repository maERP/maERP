using maERP.Application.Contracts.Infrastructure;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Services;
using maERP.Application.Mediator;
using maERP.Application.Models.Email;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.TenantEmailSettings.Commands.TenantEmailSettingsTestSend;

public class TenantEmailSettingsTestSendHandler : IRequestHandler<TenantEmailSettingsTestSendCommand, Result<bool>>
{
    private readonly IAppLogger<TenantEmailSettingsTestSendHandler> _logger;
    private readonly IEmailService _emailService;
    private readonly ITenantContext _tenantContext;

    public TenantEmailSettingsTestSendHandler(
        IAppLogger<TenantEmailSettingsTestSendHandler> logger,
        IEmailService emailService,
        ITenantContext tenantContext)
    {
        _logger = logger;
        _emailService = emailService;
        _tenantContext = tenantContext;
    }

    public async Task<Result<bool>> Handle(TenantEmailSettingsTestSendCommand request, CancellationToken cancellationToken)
    {
        var result = new Result<bool>();

        var tenantId = _tenantContext.GetCurrentTenantId();
        if (!tenantId.HasValue)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.Add("No active tenant in context.");
            return result;
        }

        var validator = new TenantEmailSettingsTestSendValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
            return result;
        }

        var message = new EmailMessage
        {
            To = request.Recipient,
            ToName = request.Recipient,
            Subject = string.IsNullOrWhiteSpace(request.Subject) ? "maERP — Test E-Mail" : request.Subject!,
            Body = string.IsNullOrWhiteSpace(request.Body)
                ? "Dies ist eine Test-Nachricht zur Überprüfung der Email-Konfiguration."
                : request.Body!,
            IsHtml = false
        };

        var sent = await _emailService.SendEmailAsync(message, tenantId);

        result.Succeeded = sent;
        result.StatusCode = sent ? ResultStatusCode.Ok : ResultStatusCode.InternalServerError;
        result.Data = sent;

        if (!sent)
        {
            result.Messages.Add("Failed to send the test email. Check the server logs for provider errors.");
            _logger.LogWarning("Test send failed for tenant {TenantId}", tenantId.Value);
        }
        else
        {
            _logger.LogInformation("Test send succeeded for tenant {TenantId} to {Recipient}", tenantId.Value, request.Recipient);
        }

        return result;
    }
}
