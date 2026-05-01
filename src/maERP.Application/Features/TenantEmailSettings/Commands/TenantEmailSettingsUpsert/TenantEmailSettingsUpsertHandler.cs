using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Application.Mediator;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.TenantEmailSettings.Commands.TenantEmailSettingsUpsert;

public class TenantEmailSettingsUpsertHandler : IRequestHandler<TenantEmailSettingsUpsertCommand, Result<Guid>>
{
    private readonly IAppLogger<TenantEmailSettingsUpsertHandler> _logger;
    private readonly ITenantEmailSettingsRepository _repository;
    private readonly ITenantContext _tenantContext;

    public TenantEmailSettingsUpsertHandler(
        IAppLogger<TenantEmailSettingsUpsertHandler> logger,
        ITenantEmailSettingsRepository repository,
        ITenantContext tenantContext)
    {
        _logger = logger;
        _repository = repository;
        _tenantContext = tenantContext;
    }

    public async Task<Result<Guid>> Handle(TenantEmailSettingsUpsertCommand request, CancellationToken cancellationToken)
    {
        var result = new Result<Guid>();

        var tenantId = _tenantContext.GetCurrentTenantId();
        if (!tenantId.HasValue)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.Add("No active tenant in context.");
            return result;
        }

        var validator = new TenantEmailSettingsUpsertValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
            return result;
        }

        try
        {
            var existing = await _repository.GetByTenantIdAsync(tenantId.Value);

            if (existing == null)
            {
                var entity = new Domain.Entities.TenantEmailSettings
                {
                    TenantId = tenantId.Value,
                    ProviderType = request.ProviderType,
                    IsActive = request.IsActive,
                    SmtpHost = request.SmtpHost,
                    SmtpPort = request.SmtpPort,
                    SmtpUsername = request.SmtpUsername,
                    SmtpPassword = request.SmtpPassword,
                    SmtpEnableSsl = request.SmtpEnableSsl,
                    M365TenantId = request.M365TenantId,
                    M365ClientId = request.M365ClientId,
                    M365ClientSecret = request.M365ClientSecret,
                    M365SenderAddress = request.M365SenderAddress,
                    FromAddress = request.FromAddress,
                    FromName = request.FromName,
                    ReplyToAddress = request.ReplyToAddress,
                    ReplyToName = request.ReplyToName
                };

                await _repository.CreateAsync(entity);

                result.Succeeded = true;
                result.StatusCode = ResultStatusCode.Created;
                result.Data = entity.Id;

                _logger.LogInformation("Created tenant email settings for tenant {TenantId}", tenantId.Value);
                return result;
            }

            existing.ProviderType = request.ProviderType;
            existing.IsActive = request.IsActive;
            existing.SmtpHost = request.SmtpHost;
            existing.SmtpPort = request.SmtpPort;
            existing.SmtpUsername = request.SmtpUsername;
            // Keep existing secret if caller did not supply a value (UI typically omits it on edit).
            if (request.SmtpPassword != null)
            {
                existing.SmtpPassword = request.SmtpPassword;
            }
            existing.SmtpEnableSsl = request.SmtpEnableSsl;
            existing.M365TenantId = request.M365TenantId;
            existing.M365ClientId = request.M365ClientId;
            if (request.M365ClientSecret != null)
            {
                existing.M365ClientSecret = request.M365ClientSecret;
            }
            existing.M365SenderAddress = request.M365SenderAddress;
            existing.FromAddress = request.FromAddress;
            existing.FromName = request.FromName;
            existing.ReplyToAddress = request.ReplyToAddress;
            existing.ReplyToName = request.ReplyToName;

            await _repository.UpdateAsync(existing);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = existing.Id;

            _logger.LogInformation("Updated tenant email settings for tenant {TenantId}", tenantId.Value);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error upserting tenant email settings: {Message}", ex.Message);
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"Error saving tenant email settings: {ex.Message}");
            return result;
        }
    }
}
