using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Superadmin.Commands.SuperadminDelete;

public class SuperadminDeleteHandler : IRequestHandler<SuperadminDeleteCommand, Result<Guid>>
{
    private readonly IAppLogger<SuperadminDeleteHandler> _logger;
    private readonly ITenantRepository _tenantRepository;

    public SuperadminDeleteHandler(
        IAppLogger<SuperadminDeleteHandler> logger,
        ITenantRepository tenantRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _tenantRepository = tenantRepository ?? throw new ArgumentNullException(nameof(tenantRepository));
    }

    public async Task<Result<Guid>> Handle(SuperadminDeleteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting tenant with ID: {Id}", request.Id);

        var result = new Result<Guid>();

        var validator = new SuperadminDeleteValidator(_tenantRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in delete request for {0}: {1}",
                nameof(SuperadminDeleteCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            var tenantToDelete = await _tenantRepository.GetByIdAsync(request.Id);

            if (tenantToDelete == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add("Tenant not found.");

                _logger.LogWarning("Tenant with ID {Id} not found for deletion", request.Id);
                return result;
            }

            await _tenantRepository.DeleteAsync(tenantToDelete);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = tenantToDelete.Id;

            _logger.LogInformation("Successfully deleted tenant with ID: {Id}", tenantToDelete.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while deleting the tenant: {ex.Message}");

            _logger.LogError("Error deleting tenant: {Message}", ex.Message);
        }

        return result;
    }
}