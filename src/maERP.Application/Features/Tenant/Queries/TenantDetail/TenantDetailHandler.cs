using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Tenant.Queries.TenantDetail;

/// <summary>
/// Handler for processing tenant detail queries.
/// Implements IRequestHandler from MediatR to handle TenantDetailQuery requests
/// and return detailed tenant information wrapped in a Result.
/// </summary>
public class TenantDetailHandler : IRequestHandler<TenantDetailQuery, Result<TenantDetailDto>>
{
    private readonly IAppLogger<TenantDetailHandler> _logger;
    private readonly ITenantRepository _tenantRepository;
    private readonly IUserTenantRepository _userTenantRepository;
    private readonly ITenantPermissionService _tenantPermissionService;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="tenantRepository">Repository for tenant data access</param>
    /// <param name="userTenantRepository">Repository for user-tenant relationship data access</param>
    /// <param name="tenantPermissionService">Service for checking tenant permissions</param>
    public TenantDetailHandler(
        IAppLogger<TenantDetailHandler> logger,
        ITenantRepository tenantRepository,
        IUserTenantRepository userTenantRepository,
        ITenantPermissionService tenantPermissionService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _tenantRepository = tenantRepository ?? throw new ArgumentNullException(nameof(tenantRepository));
        _userTenantRepository = userTenantRepository ?? throw new ArgumentNullException(nameof(userTenantRepository));
        _tenantPermissionService = tenantPermissionService ?? throw new ArgumentNullException(nameof(tenantPermissionService));
    }

    /// <summary>
    /// Handles the tenant detail query request
    /// </summary>
    /// <param name="request">The query containing the tenant ID and user ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing detailed tenant information if successful</returns>
    public async Task<Result<TenantDetailDto>> Handle(TenantDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("TenantDetailHandler.Handle: Retrieving tenant details for ID: {Id}, User: {UserId}",
            request.Id, request.UserId);

        try
        {
            // First, check if the user has access to this tenant
            var hasAccess = await _userTenantRepository.Entities
                .IgnoreQueryFilters()
                .AnyAsync(ut => ut.TenantId == request.Id && ut.UserId == request.UserId, cancellationToken);

            if (!hasAccess)
            {
                _logger.LogWarning("User {UserId} does not have access to tenant {TenantId}",
                    request.UserId, request.Id);
                return Result<TenantDetailDto>.Fail(ResultStatusCode.Forbidden,
                    "You do not have access to this tenant");
            }

            // Retrieve tenant from the repository
            var tenant = await _tenantRepository.GetByIdAsync(request.Id, asNoTracking: true);

            // If tenant not found, return a not found result
            if (tenant == null)
            {
                _logger.LogWarning("Tenant with ID {Id} not found", request.Id);
                return Result<TenantDetailDto>.Fail(ResultStatusCode.NotFound,
                    $"Tenant with ID {request.Id} not found");
            }

            // Get the user count for this tenant
            var userCount = await _userTenantRepository.Entities
                .IgnoreQueryFilters()
                .CountAsync(ut => ut.TenantId == request.Id, cancellationToken);

            // Check if the user can manage this tenant
            var canManageTenant = await _tenantPermissionService.CanManageTenantAsync(
                request.UserId, request.Id, cancellationToken);

            // Manual mapping to TenantDetailDto
            var data = new TenantDetailDto
            {
                Id = tenant.Id,
                Name = tenant.Name,
                Description = tenant.Description,
                IsActive = tenant.IsActive,
                CompanyName = tenant.CompanyName,
                ContactEmail = tenant.ContactEmail,
                Phone = tenant.Phone,
                Website = tenant.Website,
                Street = tenant.Street,
                Street2 = tenant.Street2,
                PostalCode = tenant.PostalCode,
                City = tenant.City,
                State = tenant.State,
                Country = tenant.Country,
                Iban = tenant.Iban,
                DateCreated = tenant.DateCreated,
                DateModified = tenant.DateModified,
                // Note: Domain, ConnectionString, AdminEmail, and ValidUntil are not part of the Tenant entity
                // These fields remain null in the DTO
                Domain = null,
                ConnectionString = null,
                AdminEmail = null,
                ValidUntil = null,
                UserCount = userCount,
                CanManageTenant = canManageTenant
            };

            _logger.LogInformation("Tenant with ID {Id} retrieved successfully for user {UserId}",
                request.Id, request.UserId);

            return Result<TenantDetailDto>.Success(data);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error retrieving tenant {Id} for user {UserId}: {Message}",
                request.Id, request.UserId, ex.Message);

            return Result<TenantDetailDto>.Fail(ResultStatusCode.InternalServerError,
                $"An error occurred while retrieving the tenant: {ex.Message}");
        }
    }
}
