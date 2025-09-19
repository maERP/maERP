using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Services;
using maERP.Application.Extensions;
using maERP.Application.Mediator;
using maERP.Domain.Dtos.User;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace maERP.Application.Features.User.Queries.UserList;

/// <summary>
/// Handler for processing user list queries.
/// Implements IRequestHandler from MediatR to handle UserListQuery requests
/// and return a paginated list of users wrapped in a PaginatedResult.
/// </summary>
public class UserListHandler : IRequestHandler<UserListQuery, PaginatedResult<UserListDto>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<UserListHandler> _logger;

    /// <summary>
    /// ASP.NET Identity UserManager for user data operations
    /// </summary>
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITenantContext _tenantContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITenantPermissionService _tenantPermissionService;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="userManager">ASP.NET Identity UserManager for user data access</param>
    public UserListHandler(
        IAppLogger<UserListHandler> logger,
        UserManager<ApplicationUser> userManager,
        ITenantContext tenantContext,
        IHttpContextAccessor httpContextAccessor,
        ITenantPermissionService tenantPermissionService)
    {
        _logger = logger;
        _userManager = userManager;
        _tenantContext = tenantContext;
        _httpContextAccessor = httpContextAccessor;
        _tenantPermissionService = tenantPermissionService;
    }

    /// <summary>
    /// Handles the user list query request
    /// </summary>
    /// <param name="request">The query containing pagination, search, and ordering parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>PaginatedResult containing a list of users based on the query parameters</returns>
    public async Task<PaginatedResult<UserListDto>> Handle(UserListQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement filter specification if needed
        // var userFilterSpec = new UserFilterSpecification(request.SearchString);

        _logger.LogInformation("Handle UserListQuery: {0}", request);

        var httpContext = _httpContextAccessor.HttpContext;
        var currentTenantId = ResolveTenantId(_tenantContext.GetCurrentTenantId());

        var currentUser = httpContext?.User;
        var isSuperadmin = currentUser?.IsInRole("Superadmin") ?? false;

        if (!isSuperadmin)
        {
            var currentUserId = httpContext.GetUserId() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(currentUserId))
            {
                var missingUser = PaginatedResult<UserListDto>.Failure(new List<string>
                {
                    "User context is required to evaluate permissions."
                });
                missingUser.StatusCode = ResultStatusCode.Unauthorized;
                missingUser.Data = new List<UserListDto>();
                return missingUser;
            }

            if (!currentTenantId.HasValue || currentTenantId.Value == Guid.Empty)
            {
                var missingTenant = PaginatedResult<UserListDto>.Failure(new List<string>
                {
                    "Tenant context is required to list users."
                });
                missingTenant.StatusCode = ResultStatusCode.BadRequest;
                missingTenant.Data = new List<UserListDto>();
                return missingTenant;
            }

            var hasPermission = await _tenantPermissionService.CanManageUsersAsync(
                currentUserId,
                currentTenantId.Value,
                cancellationToken);

            if (!hasPermission)
            {
                var forbidden = PaginatedResult<UserListDto>.Failure(new List<string>
                {
                    "You do not have permission to manage users for this tenant."
                });
                forbidden.StatusCode = ResultStatusCode.Forbidden;
                forbidden.Data = new List<UserListDto>();
                return forbidden;
            }
        }
        else if (!currentTenantId.HasValue || currentTenantId.Value == Guid.Empty)
        {
            return PaginatedResult<UserListDto>.Success(new List<UserListDto>(), 0, request.PageNumber, request.PageSize);
        }

        // Manual mapping using LINQ projection instead of AutoMapper
        // This creates a query that selects only the needed properties for the DTO
        var query = _userManager.Users
            .Where(u => u.UserTenants!.Any(ut => ut.TenantId == currentTenantId.Value))
            .Select(u => new UserListDto
            {
                Id = u.Id,
                Email = u.Email ?? string.Empty,
                Firstname = u.Firstname,
                Lastname = u.Lastname,
                DateCreated = u.DateCreated
            });

        // If no ordering is specified, return paginated results without ordering
        if (request.OrderBy.Any() != true)
        {
            return await query
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        // Join the ordering properties into a string format that can be used by Dynamic LINQ
        var ordering = string.Join(",", request.OrderBy);

        // Apply dynamic ordering and pagination to the query
        return await query
            .OrderBy(ordering)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
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
