using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Application.Mediator;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.TenantOAuthAppSettings.Commands.TenantOAuthAppSettingsDelete;

public class TenantOAuthAppSettingsDeleteHandler
    : IRequestHandler<TenantOAuthAppSettingsDeleteCommand, Result<int>>
{
    private readonly IAppLogger<TenantOAuthAppSettingsDeleteHandler> _logger;
    private readonly ITenantOAuthAppSettingsRepository _repository;
    private readonly ITenantContext _tenantContext;

    public TenantOAuthAppSettingsDeleteHandler(
        IAppLogger<TenantOAuthAppSettingsDeleteHandler> logger,
        ITenantOAuthAppSettingsRepository repository,
        ITenantContext tenantContext)
    {
        _logger = logger;
        _repository = repository;
        _tenantContext = tenantContext;
    }

    public async Task<Result<int>> Handle(
        TenantOAuthAppSettingsDeleteCommand request, CancellationToken cancellationToken)
    {
        var tenantId = _tenantContext.GetCurrentTenantId();
        if (!tenantId.HasValue)
        {
            return Result<int>.Fail(ResultStatusCode.BadRequest, "No active tenant in context.");
        }

        var existing = await _repository.GetByTenantAndProviderAsync(tenantId.Value, request.Provider);
        if (existing is null)
        {
            return Result<int>.Fail(ResultStatusCode.NotFound,
                $"No tenant OAuth app settings configured for {request.Provider}.");
        }

        await _repository.DeleteAsync(existing);

        _logger.LogInformation(
            "Deleted tenant OAuth app settings for tenant {TenantId} provider {Provider}",
            tenantId.Value, request.Provider);

        return new Result<int> { Succeeded = true, Data = 1, StatusCode = ResultStatusCode.NoContent };
    }
}
