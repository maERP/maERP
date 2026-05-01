using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Application.Mediator;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.TenantEmailSettings.Commands.TenantEmailSettingsDelete;

public class TenantEmailSettingsDeleteHandler : IRequestHandler<TenantEmailSettingsDeleteCommand, Result<Guid>>
{
    private readonly IAppLogger<TenantEmailSettingsDeleteHandler> _logger;
    private readonly ITenantEmailSettingsRepository _repository;
    private readonly ITenantContext _tenantContext;

    public TenantEmailSettingsDeleteHandler(
        IAppLogger<TenantEmailSettingsDeleteHandler> logger,
        ITenantEmailSettingsRepository repository,
        ITenantContext tenantContext)
    {
        _logger = logger;
        _repository = repository;
        _tenantContext = tenantContext;
    }

    public async Task<Result<Guid>> Handle(TenantEmailSettingsDeleteCommand request, CancellationToken cancellationToken)
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

        var existing = await _repository.GetByTenantIdAsync(tenantId.Value);
        if (existing == null)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.NotFound;
            result.Messages.Add("No tenant-level email configuration to delete.");
            return result;
        }

        try
        {
            await _repository.DeleteAsync(existing);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.NoContent;
            result.Data = existing.Id;

            _logger.LogInformation("Deleted tenant email settings for tenant {TenantId}", tenantId.Value);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error deleting tenant email settings: {Message}", ex.Message);
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"Error deleting tenant email settings: {ex.Message}");
            return result;
        }
    }
}
