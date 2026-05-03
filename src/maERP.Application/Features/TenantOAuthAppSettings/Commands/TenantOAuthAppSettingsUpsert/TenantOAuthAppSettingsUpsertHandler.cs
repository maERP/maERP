using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Application.Mediator;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.TenantOAuthAppSettings.Commands.TenantOAuthAppSettingsUpsert;

public class TenantOAuthAppSettingsUpsertHandler
    : IRequestHandler<TenantOAuthAppSettingsUpsertCommand, Result<Guid>>
{
    private readonly IAppLogger<TenantOAuthAppSettingsUpsertHandler> _logger;
    private readonly ITenantOAuthAppSettingsRepository _repository;
    private readonly ITenantContext _tenantContext;

    public TenantOAuthAppSettingsUpsertHandler(
        IAppLogger<TenantOAuthAppSettingsUpsertHandler> logger,
        ITenantOAuthAppSettingsRepository repository,
        ITenantContext tenantContext)
    {
        _logger = logger;
        _repository = repository;
        _tenantContext = tenantContext;
    }

    public async Task<Result<Guid>> Handle(
        TenantOAuthAppSettingsUpsertCommand request, CancellationToken cancellationToken)
    {
        var tenantId = _tenantContext.GetCurrentTenantId();
        if (!tenantId.HasValue)
        {
            return Result<Guid>.Fail(ResultStatusCode.BadRequest, "No active tenant in context.");
        }

        if (request.Provider is not (SalesChannelType.eBay or SalesChannelType.Amazon))
        {
            return Result<Guid>.Fail(ResultStatusCode.BadRequest,
                $"OAuth provider {request.Provider} is not supported.");
        }

        try
        {
            var existing = await _repository.GetByTenantAndProviderAsync(tenantId.Value, request.Provider);

            if (existing is null)
            {
                var entity = new Domain.Entities.TenantOAuthAppSettings
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenantId.Value,
                    Provider = request.Provider,
                    IsActive = request.IsActive,
                    ClientId = request.ClientId,
                    ClientSecret = request.ClientSecret,
                    RedirectUri = request.RedirectUri,
                    RuName = request.RuName,
                    Scopes = request.Scopes,
                    UseSandbox = request.UseSandbox,
                };
                await _repository.CreateAsync(entity);

                _logger.LogInformation(
                    "Created tenant OAuth app settings for tenant {TenantId} provider {Provider}",
                    tenantId.Value, request.Provider);

                return new Result<Guid> { Succeeded = true, Data = entity.Id, StatusCode = ResultStatusCode.Created };
            }

            existing.IsActive = request.IsActive;
            existing.ClientId = request.ClientId;
            // null preserves existing secret; non-null replaces (incl. empty for explicit clear).
            if (request.ClientSecret is not null)
            {
                existing.ClientSecret = request.ClientSecret;
            }
            existing.RedirectUri = request.RedirectUri;
            existing.RuName = request.RuName;
            existing.Scopes = request.Scopes;
            existing.UseSandbox = request.UseSandbox;

            await _repository.UpdateAsync(existing);

            _logger.LogInformation(
                "Updated tenant OAuth app settings for tenant {TenantId} provider {Provider}",
                tenantId.Value, request.Provider);

            return Result<Guid>.Success(existing.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error upserting tenant OAuth app settings: {Message}", ex.Message);
            return Result<Guid>.Fail(ResultStatusCode.InternalServerError,
                $"Error saving tenant OAuth app settings: {ex.Message}");
        }
    }
}
