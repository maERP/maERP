using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Application.Mediator;
using maERP.Domain.Dtos.TenantOAuthAppSettings;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.TenantOAuthAppSettings.Queries.TenantOAuthAppSettingsList;

public class TenantOAuthAppSettingsListHandler
    : IRequestHandler<TenantOAuthAppSettingsListQuery, Result<List<TenantOAuthAppSettingsListDto>>>
{
    private readonly IAppLogger<TenantOAuthAppSettingsListHandler> _logger;
    private readonly ITenantOAuthAppSettingsRepository _repository;
    private readonly ITenantContext _tenantContext;

    public TenantOAuthAppSettingsListHandler(
        IAppLogger<TenantOAuthAppSettingsListHandler> logger,
        ITenantOAuthAppSettingsRepository repository,
        ITenantContext tenantContext)
    {
        _logger = logger;
        _repository = repository;
        _tenantContext = tenantContext;
    }

    public async Task<Result<List<TenantOAuthAppSettingsListDto>>> Handle(
        TenantOAuthAppSettingsListQuery request, CancellationToken cancellationToken)
    {
        var tenantId = _tenantContext.GetCurrentTenantId();
        if (!tenantId.HasValue)
        {
            return Result<List<TenantOAuthAppSettingsListDto>>.Fail(
                ResultStatusCode.BadRequest, "No active tenant in context.");
        }

        var rows = await _repository.GetByTenantIdAsync(tenantId.Value);

        var dtos = rows.Select(r => new TenantOAuthAppSettingsListDto
        {
            Id = r.Id,
            TenantId = r.TenantId,
            Provider = r.Provider,
            IsActive = r.IsActive,
            ClientId = r.ClientId,
            HasClientSecret = !string.IsNullOrEmpty(r.ClientSecret),
            RedirectUri = r.RedirectUri,
            RuName = r.RuName,
            UseSandbox = r.UseSandbox,
        }).ToList();

        return Result<List<TenantOAuthAppSettingsListDto>>.Success(dtos);
    }
}
