using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Tenant.Commands.TenantUpdate;

public class TenantUpdateHandler : IRequestHandler<TenantUpdateCommand, Result<Guid>>
{
    private readonly IAppLogger<TenantUpdateHandler> _logger;
    private readonly ITenantRepository _tenantRepository;
    private readonly ITenantPermissionService _tenantPermissionService;

    public TenantUpdateHandler(
        IAppLogger<TenantUpdateHandler> logger,
        ITenantRepository tenantRepository,
        ITenantPermissionService tenantPermissionService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _tenantRepository = tenantRepository ?? throw new ArgumentNullException(nameof(tenantRepository));
        _tenantPermissionService = tenantPermissionService ?? throw new ArgumentNullException(nameof(tenantPermissionService));
    }

    public async Task<Result<Guid>> Handle(TenantUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User {UserId} is updating tenant {TenantId} with name: {Name}",
            request.UserId, request.TenantId, request.Name);

        var result = new Result<Guid>();

        var validator = new TenantUpdateValidator(_tenantRepository, _tenantPermissionService);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in update request for {0}: {1}",
                nameof(TenantUpdateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            var tenantToUpdate = await _tenantRepository.GetByIdAsync(request.TenantId);

            if (tenantToUpdate == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add("Tenant not found.");

                _logger.LogWarning("Tenant with ID {TenantId} not found for update", request.TenantId);
                return result;
            }

            // Update tenant properties
            tenantToUpdate.Name = request.Name;
            tenantToUpdate.Description = request.Description;
            tenantToUpdate.CompanyName = request.CompanyName;
            tenantToUpdate.ContactEmail = request.ContactEmail;
            tenantToUpdate.Phone = request.Phone;
            tenantToUpdate.Website = request.Website;
            tenantToUpdate.Street = request.Street;
            tenantToUpdate.Street2 = request.Street2;
            tenantToUpdate.PostalCode = request.PostalCode;
            tenantToUpdate.City = request.City;
            tenantToUpdate.State = request.State;
            tenantToUpdate.Country = request.Country;
            tenantToUpdate.Iban = request.Iban;

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

            _logger.LogError("Error updating tenant for user {UserId}: {Message}",
                request.UserId, ex.Message);
        }

        return result;
    }
}
