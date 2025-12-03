using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using maERP.Domain.Entities;
using maERP.Domain.Enums;

namespace maERP.Application.Features.Tenant.Commands.TenantCreate;

public class TenantCreateHandler : IRequestHandler<TenantCreateCommand, Result<Guid>>
{
    private readonly IAppLogger<TenantCreateHandler> _logger;
    private readonly ITenantRepository _tenantRepository;
    private readonly IUserTenantRepository _userTenantRepository;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly ISalesChannelRepository _salesChannelRepository;
    private readonly ITaxClassRepository _taxClassRepository;

    public TenantCreateHandler(
        IAppLogger<TenantCreateHandler> logger,
        ITenantRepository tenantRepository,
        IUserTenantRepository userTenantRepository,
        IWarehouseRepository warehouseRepository,
        ISalesChannelRepository salesChannelRepository,
        ITaxClassRepository taxClassRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _tenantRepository = tenantRepository ?? throw new ArgumentNullException(nameof(tenantRepository));
        _userTenantRepository = userTenantRepository ?? throw new ArgumentNullException(nameof(userTenantRepository));
        _warehouseRepository = warehouseRepository ?? throw new ArgumentNullException(nameof(warehouseRepository));
        _salesChannelRepository = salesChannelRepository ?? throw new ArgumentNullException(nameof(salesChannelRepository));
        _taxClassRepository = taxClassRepository ?? throw new ArgumentNullException(nameof(taxClassRepository));
    }

    public async Task<Result<Guid>> Handle(TenantCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User {UserId} is creating a new tenant with name: {Name}",
            request.UserId, request.Name);

        var result = new Result<Guid>();

        var validator = new TenantCreateValidator(_tenantRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in create request for {0}: {1}",
                nameof(TenantCreateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        // Use a database transaction to ensure atomicity
        await using var transaction = await _tenantRepository.BeginTransactionAsync(cancellationToken);

        try
        {
            // Create the tenant
            var tenantToCreate = new Domain.Entities.Tenant
            {
                Name = request.Name,
                Description = request.Description,
                IsActive = request.IsActive,
                CompanyName = request.CompanyName,
                ContactEmail = request.ContactEmail,
                Phone = request.Phone,
                Website = request.Website,
                Street = request.Street,
                Street2 = request.Street2,
                PostalCode = request.PostalCode,
                City = request.City,
                State = request.State,
                Country = request.Country,
                Iban = request.Iban
            };

            // Add tenant to context without saving
            _tenantRepository.Add(tenantToCreate);

            // Assign the user to the tenant with management permissions
            var userTenant = new UserTenant
            {
                UserId = request.UserId,
                TenantId = tenantToCreate.Id,
                IsDefault = true, // Make it the default tenant
                RoleManageUser = true, // Give user management permission for their own tenant
                RoleManageTenant = true // Give tenant management permission to the creator
            };

            // Add user-tenant association to context without saving
            _userTenantRepository.Add(userTenant);

            // Create default warehouse
            var defaultWarehouse = new Domain.Entities.Warehouse
            {
                Name = "Hauptlager",
                TenantId = tenantToCreate.Id
            };
            _warehouseRepository.Add(defaultWarehouse);

            // Create default tax classes
            var taxClass0 = new Domain.Entities.TaxClass { TaxRate = 0, TenantId = tenantToCreate.Id };
            var taxClass7 = new Domain.Entities.TaxClass { TaxRate = 7, TenantId = tenantToCreate.Id };
            var taxClass19 = new Domain.Entities.TaxClass { TaxRate = 19, TenantId = tenantToCreate.Id };
            _taxClassRepository.Add(taxClass0);
            _taxClassRepository.Add(taxClass7);
            _taxClassRepository.Add(taxClass19);

            // Create default sales channel (Point of Sale) with the warehouse
            var defaultSalesChannel = new Domain.Entities.SalesChannel
            {
                Name = "Kasse Hauptlager",
                Type = SalesChannelType.PointOfSale,
                TenantId = tenantToCreate.Id,
                Warehouses = new List<Domain.Entities.Warehouse> { defaultWarehouse }
            };
            _salesChannelRepository.Add(defaultSalesChannel);

            // Save all changes within the transaction
            await _tenantRepository.SaveChangesAsync(cancellationToken);

            // Commit the transaction
            await transaction.CommitAsync(cancellationToken);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = tenantToCreate.Id;

            _logger.LogInformation("Successfully created tenant with ID: {Id} and assigned user {UserId} to it with default warehouse, sales channel and tax classes",
                tenantToCreate.Id, request.UserId);
        }
        catch (Exception ex)
        {
            // Transaction will automatically rollback on dispose if not committed
            await transaction.RollbackAsync(cancellationToken);

            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating the tenant: {ex.Message}");

            _logger.LogError("Error creating tenant for user {UserId}: {Message}",
                request.UserId, ex.Message);
        }

        return result;
    }
}
