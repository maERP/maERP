using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Superadmin.Commands.SuperadminCreate;

public class SuperadminCreateHandler : IRequestHandler<SuperadminCreateCommand, Result<Guid>>
{
    private readonly IAppLogger<SuperadminCreateHandler> _logger;
    private readonly ITenantRepository _tenantRepository;

    public SuperadminCreateHandler(
        IAppLogger<SuperadminCreateHandler> logger,
        ITenantRepository tenantRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _tenantRepository = tenantRepository ?? throw new ArgumentNullException(nameof(tenantRepository));
    }

    public async Task<Result<Guid>> Handle(SuperadminCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new tenant with name: {Name}",
            request.Name);

        var result = new Result<Guid>();

        var validator = new SuperadminCreateValidator(_tenantRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in create request for {0}: {1}",
                nameof(SuperadminCreateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            var tenantToCreate = new Domain.Entities.Tenant
            {
                Name = request.Name,
                Description = request.Description,
                IsActive = request.IsActive,
                ContactEmail = request.ContactEmail
            };

            await _tenantRepository.CreateAsync(tenantToCreate);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = tenantToCreate.Id;

            _logger.LogInformation("Successfully created tenant with ID: {Id}", tenantToCreate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating the tenant: {ex.Message}");

            _logger.LogError("Error creating tenant: {Message}", ex.Message);
        }

        return result;
    }
}