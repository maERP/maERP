using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Tenant.Commands.TenantDelete;

public class TenantDeleteHandler : IRequestHandler<TenantDeleteCommand, Result<Guid>>
{
    private readonly IAppLogger<TenantDeleteHandler> _logger;
    private readonly ITenantRepository _tenantRepository;
    private readonly ITenantPermissionService _tenantPermissionService;

    public TenantDeleteHandler(
        IAppLogger<TenantDeleteHandler> logger,
        ITenantRepository tenantRepository,
        ITenantPermissionService tenantPermissionService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _tenantRepository = tenantRepository ?? throw new ArgumentNullException(nameof(tenantRepository));
        _tenantPermissionService = tenantPermissionService ?? throw new ArgumentNullException(nameof(tenantPermissionService));
    }

    public async Task<Result<Guid>> Handle(TenantDeleteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User {UserId} is deleting tenant {TenantId}",
            request.UserId, request.TenantId);

        var result = new Result<Guid>();

        var validator = new TenantDeleteValidator(_tenantRepository, _tenantPermissionService);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in delete request for {0}: {1}",
                nameof(TenantDeleteCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            await _tenantRepository.DeleteTenantWithCascadeAsync(request.TenantId, cancellationToken);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.NoContent;
            result.Data = request.TenantId;

            _logger.LogInformation("Successfully deleted tenant with ID: {TenantId}", request.TenantId);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.NotFound;
            result.Messages.Add("Tenant was already deleted by another request");

            _logger.LogWarning("Tenant with ID: {TenantId} was deleted by another request: {Message}",
                request.TenantId, ex.Message);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while deleting the tenant: {ex.Message}");

            _logger.LogError("Error deleting tenant {TenantId}: {Message}",
                request.TenantId, ex.Message);
        }

        return result;
    }
}
