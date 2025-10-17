using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Superadmin.Commands.SuperadminUpdate;

public class SuperadminUpdateHandler : IRequestHandler<SuperadminUpdateCommand, Result<Guid>>
{
    private readonly IAppLogger<SuperadminUpdateHandler> _logger;
    private readonly ITenantRepository _tenantRepository;

    public SuperadminUpdateHandler(
        IAppLogger<SuperadminUpdateHandler> logger,
        ITenantRepository tenantRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _tenantRepository = tenantRepository ?? throw new ArgumentNullException(nameof(tenantRepository));
    }

    public async Task<Result<Guid>> Handle(SuperadminUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating tenant with ID: {Id}", request.Id);

        var result = new Result<Guid>();

        var validator = new SuperadminUpdateValidator(_tenantRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in update request for {0}: {1}",
                nameof(SuperadminUpdateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            var tenantToUpdate = await _tenantRepository.GetByIdAsync(request.Id);

            if (tenantToUpdate == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add("Tenant not found.");

                _logger.LogWarning("Tenant with ID {Id} not found for update", request.Id);
                return result;
            }

            tenantToUpdate.Name = request.Name;
            tenantToUpdate.Description = request.Description;
            tenantToUpdate.IsActive = request.IsActive;
            tenantToUpdate.ContactEmail = request.ContactEmail;

            await _tenantRepository.UpdateAsync(tenantToUpdate);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = tenantToUpdate.Id;

            _logger.LogInformation("Successfully updated tenant with ID: {Id}", tenantToUpdate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while updating the tenant: {ex.Message}");

            _logger.LogError("Error updating tenant: {Message}", ex.Message);
        }

        return result;
    }
}