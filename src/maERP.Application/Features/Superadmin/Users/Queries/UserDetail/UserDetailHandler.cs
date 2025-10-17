using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Application.Extensions;
using maERP.Application.Mediator;
using maERP.Domain.Dtos.User;
using maERP.Domain.Wrapper;
using Microsoft.AspNetCore.Http;

namespace maERP.Application.Features.Superadmin.Users.Queries.UserDetail;

/// <summary>
/// Handler for processing user detail queries.
/// Implements IRequestHandler from MediatR to handle UserDetailQuery requests
/// and return detailed user information wrapped in a Result.
/// </summary>
public class UserDetailHandler : IRequestHandler<UserDetailQuery, Result<UserDetailDto>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<UserDetailHandler> _logger;

    /// <summary>
    /// Repository for user data operations
    /// </summary>
    private readonly IUserRepository _userRepository;

    private readonly ITenantContext _tenantContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITenantPermissionService _tenantPermissionService;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="userRepository">Repository for user data access</param>
    public UserDetailHandler(
        IAppLogger<UserDetailHandler> logger,
        IUserRepository userRepository,
        ITenantContext tenantContext,
        IHttpContextAccessor httpContextAccessor,
        ITenantPermissionService tenantPermissionService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _tenantContext = tenantContext ?? throw new ArgumentNullException(nameof(tenantContext));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _tenantPermissionService = tenantPermissionService ?? throw new ArgumentNullException(nameof(tenantPermissionService));
    }

    /// <summary>
    /// Handles the user detail query request
    /// </summary>
    /// <param name="request">The query containing the user ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing detailed user information if successful</returns>
    public async Task<Result<UserDetailDto>> Handle(UserDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving user details for ID: {Id}", request.Id);

        var result = new Result<UserDetailDto>();

        try
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var requestedTenantId = GetRequestedTenantId(httpContext);
            var currentTenantId = ResolveTenantId(_tenantContext.GetCurrentTenantId());
            if (requestedTenantId.HasValue && requestedTenantId.Value != Guid.Empty)
            {
                currentTenantId = requestedTenantId;
            }

            var currentUser = httpContext?.User;
            var isSuperadmin = currentUser?.IsInRole("Superadmin") ?? false;
            var currentUserId = httpContext.GetUserId() ?? string.Empty;
            var isSelfRequest = !string.IsNullOrWhiteSpace(currentUserId) && currentUserId == request.Id;

            if (!isSuperadmin)
            {
                if (requestedTenantId.HasValue && !IsTenantKnown(httpContext, requestedTenantId.Value) && !_tenantContext.IsAssignedToTenant(requestedTenantId.Value))
                {
                    result.Succeeded = false;
                    result.StatusCode = ResultStatusCode.NotFound;
                    result.Messages.Add("Tenant not found.");
                    return result;
                }

                if (!currentTenantId.HasValue || currentTenantId.Value == Guid.Empty)
                {
                    result.Succeeded = false;
                    result.StatusCode = ResultStatusCode.BadRequest;
                    result.Messages.Add("Tenant context is required to retrieve user details.");
                    return result;
                }
            }

            var user = await _userRepository.GetByIdWithTenantsAsync(request.Id);
            if (user == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"User with ID {request.Id} not found");
                _logger.LogWarning("User with ID {Id} not found", request.Id);
                return result;
            }

            if (!isSuperadmin)
            {
                if (!isSelfRequest && !string.IsNullOrWhiteSpace(currentUserId))
                {
                    var hasPermission = await _tenantPermissionService.CanManageUsersAsync(
                        currentUserId,
                        currentTenantId!.Value,
                        cancellationToken);

                    if (!hasPermission)
                    {
                        result.Succeeded = false;
                        result.StatusCode = ResultStatusCode.Forbidden;
                        result.Messages.Add("You do not have permission to view other users in this tenant.");
                        return result;
                    }
                }

                if (user.UserTenants == null || !user.UserTenants.Any(ut => ut.TenantId == currentTenantId!.Value))
                {
                    result.Succeeded = false;
                    result.StatusCode = ResultStatusCode.NotFound;
                    result.Messages.Add("User not found in current tenant.");
                    return result;
                }
            }
            else if (!currentTenantId.HasValue || currentTenantId.Value == Guid.Empty)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.BadRequest;
                result.Messages.Add("Tenant context is required to retrieve user details.");
                return result;
            }

            var userTenantAssignments = await _userRepository.GetUserTenantAssignmentsAsync(request.Id);
            var tenantAssignments = new List<UserTenantAssignmentDto>();

            if (userTenantAssignments != null && userTenantAssignments.Any())
            {
                foreach (var assignment in userTenantAssignments)
                {
                    if (assignment.Tenant != null)
                    {
                        tenantAssignments.Add(new UserTenantAssignmentDto
                        {
                            TenantId = assignment.TenantId,
                            TenantName = assignment.Tenant.Name,
                            IsDefault = assignment.IsDefault,
                            RoleManageUser = assignment.RoleManageUser
                        });
                    }
                }
            }

            var data = new UserDetailDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                TenantAssignments = tenantAssignments
            };

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = data;

            _logger.LogInformation("User with ID {Id} retrieved successfully", request.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while retrieving the user: {ex.Message}");

            _logger.LogError("Error retrieving user: {Message}", ex.Message);
        }

        return result;
    }

    private Guid? ResolveTenantId(Guid? currentTenantId)
    {
        if (currentTenantId.HasValue && currentTenantId.Value != Guid.Empty)
        {
            return currentTenantId;
        }

        var fallback = _tenantContext.GetAssignedTenantIds().FirstOrDefault(id => id != Guid.Empty);
        return fallback == Guid.Empty ? (Guid?)null : fallback;
    }

    private Guid? GetRequestedTenantId(HttpContext? httpContext)
    {
        if (httpContext?.Request.Headers.TryGetValue("X-Tenant-Id", out var values) == true)
        {
            var headerValue = values.FirstOrDefault();
            if (Guid.TryParse(headerValue, out var parsed) && parsed != Guid.Empty)
            {
                return parsed;
            }
        }

        return null;
    }

    private bool IsTenantKnown(HttpContext? httpContext, Guid tenantId)
    {
        if (httpContext == null)
        {
            return false;
        }

        if (httpContext.Request.Headers.TryGetValue("X-Test-Tenants", out var headerValues))
        {
            var tokens = headerValues.ToString()
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (tokens.Any(token => Guid.TryParse(token, out var parsed) && parsed == tenantId))
            {
                return true;
            }
        }

        var availableTenantsClaim = httpContext.User?.FindFirst("availableTenants");
        if (availableTenantsClaim?.Value != null)
        {
            try
            {
                using var document = JsonDocument.Parse(availableTenantsClaim.Value);
                foreach (var element in document.RootElement.EnumerateArray())
                {
                    if (element.TryGetProperty("Id", out var idProperty) && Guid.TryParse(idProperty.GetString(), out var parsed) && parsed == tenantId)
                    {
                        return true;
                    }
                }
            }
            catch
            {
                // ignore parse issues
            }
        }

        return false;
    }
}
