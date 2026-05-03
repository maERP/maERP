using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Application.Mediator;
using maERP.Domain.Dtos.TenantOAuthAppSettings;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.TenantOAuthAppSettings.Queries.TenantOAuthAppSettingsDetail;

public class TenantOAuthAppSettingsDetailHandler
    : IRequestHandler<TenantOAuthAppSettingsDetailQuery, Result<TenantOAuthAppSettingsDetailDto>>
{
    private readonly IAppLogger<TenantOAuthAppSettingsDetailHandler> _logger;
    private readonly ITenantOAuthAppSettingsRepository _repository;
    private readonly ITenantContext _tenantContext;

    public TenantOAuthAppSettingsDetailHandler(
        IAppLogger<TenantOAuthAppSettingsDetailHandler> logger,
        ITenantOAuthAppSettingsRepository repository,
        ITenantContext tenantContext)
    {
        _logger = logger;
        _repository = repository;
        _tenantContext = tenantContext;
    }

    public async Task<Result<TenantOAuthAppSettingsDetailDto>> Handle(
        TenantOAuthAppSettingsDetailQuery request, CancellationToken cancellationToken)
    {
        var tenantId = _tenantContext.GetCurrentTenantId();
        if (!tenantId.HasValue)
        {
            return Result<TenantOAuthAppSettingsDetailDto>.Fail(
                ResultStatusCode.BadRequest, "No active tenant in context.");
        }

        var row = await _repository.GetByTenantAndProviderAsync(tenantId.Value, request.Provider);

        if (row is null)
        {
            // Not configured yet — return an empty stub for the editor to populate.
            return Result<TenantOAuthAppSettingsDetailDto>.Success(new TenantOAuthAppSettingsDetailDto
            {
                TenantId = tenantId.Value,
                Provider = request.Provider,
                IsActive = false,
                HasClientSecret = false,
                ClientSecret = string.Empty,
            });
        }

        return Result<TenantOAuthAppSettingsDetailDto>.Success(new TenantOAuthAppSettingsDetailDto
        {
            Id = row.Id,
            TenantId = row.TenantId,
            Provider = row.Provider,
            IsActive = row.IsActive,
            ClientId = row.ClientId,
            ClientSecret = string.Empty,                       // never returned in clear
            HasClientSecret = !string.IsNullOrEmpty(row.ClientSecret),
            RedirectUri = row.RedirectUri,
            RuName = row.RuName,
            Scopes = row.Scopes,
            UseSandbox = row.UseSandbox,
        });
    }
}
