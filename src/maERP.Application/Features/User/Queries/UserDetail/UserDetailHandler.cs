using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Application.Extensions;
using maERP.Application.Mediator;
using maERP.Domain.Dtos.User;
using maERP.Domain.Wrapper;
using Microsoft.AspNetCore.Http;

namespace maERP.Application.Features.User.Queries.UserDetail;

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
            var currentTenantId = ResolveTenantId(_tenantContext.GetCurrentTenantId());
            var currentUser = httpContext?.User;
            var isSuperadmin = currentUser?.IsInRole("Superadmin") ?? false;
            var currentUserId = httpContext.GetUserId() ?? string.Empty;
            var isSelfRequest = !string.IsNullOrWhiteSpace(currentUserId) && currentUserId == request.Id;

            if (!isSuperadmin)
            {
                if (string.IsNullOrWhiteSpace(currentUserId))
                {
                    result.Succeeded = false;
                    result.StatusCode = ResultStatusCode.Unauthorized;
                    result.Messages.Add("User context is required to evaluate permissions.");
                    return result;
                }

                if (!currentTenantId.HasValue || currentTenantId.Value == Guid.Empty)
                {
                    result.Succeeded = false;
                    result.StatusCode = ResultStatusCode.BadRequest;
                    result.Messages.Add("Tenant context is required to retrieve user details.");
                    return result;
                }

                if (!isSelfRequest)
                {
                    var hasPermission = await _tenantPermissionService.CanManageUsersAsync(
                        currentUserId,
                        currentTenantId.Value,
                        cancellationToken);

                    if (!hasPermission)
                    {
                        result.Succeeded = false;
                        result.StatusCode = ResultStatusCode.Forbidden;
                        result.Messages.Add("You do not have permission to view other users in this tenant.");
                        return result;
                    }
                }
            }

            // Retrieve user from the repository by ID
            var user = await _userRepository.GetByIdWithTenantsAsync(request.Id);

            // If user not found, return a not found result
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
                if (!currentTenantId.HasValue || currentTenantId.Value == Guid.Empty ||
                    user.UserTenants == null || !user.UserTenants.Any(ut => ut.TenantId == currentTenantId.Value))
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

            // Get user tenant assignments
            var userTenantAssignments = await _userRepository.GetUserTenantAssignmentsAsync(request.Id);
            var tenantAssignments = new List<UserTenantAssignmentDto>();

            // Map tenant assignments to DTOs
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
                            TenantCode = assignment.Tenant.TenantCode,
                            IsDefault = assignment.IsDefault,
                            RoleManageUser = assignment.RoleManageUser
                        });
                    }
                }
            }

            // Manual mapping from entity to DTO (instead of using AutoMapper)
            var data = new UserDetailDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                TenantAssignments = tenantAssignments
            };

            // Set successful result with the user details
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = data;

            _logger.LogInformation("User with ID {Id} retrieved successfully", request.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during user retrieval
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
}
