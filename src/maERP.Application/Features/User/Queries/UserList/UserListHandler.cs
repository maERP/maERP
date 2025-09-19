using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;
using System.Security.Claims;
using System.Text.Json;
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

        _logger.LogInformation("Handle UserListQuery: {Request}", request);

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

        var sanitizedPageSize = request.PageSize <= 0 ? 10 : request.PageSize;
        var sanitizedPage = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageIndex = sanitizedPage - 1;

        if (!isSuperadmin)
        {
            if (string.IsNullOrWhiteSpace(currentUserId))
            {
                _logger.LogWarning("Missing user context while evaluating user list permissions.");
                var emptyResult = PaginatedResult<UserListDto>.Success(new List<UserListDto>(), 0, sanitizedPage, sanitizedPageSize);
                emptyResult.CurrentPage = sanitizedPage;
                emptyResult.PageSize = sanitizedPageSize;
                return emptyResult;
            }

            if (requestedTenantId.HasValue && !IsTenantKnown(httpContext, requestedTenantId.Value) && !_tenantContext.IsAssignedToTenant(requestedTenantId.Value))
            {
                var missingRequestedTenant = PaginatedResult<UserListDto>.Failure(new List<string>
                {
                    "You do not have permission to manage users for this tenant."
                });
                missingRequestedTenant.StatusCode = ResultStatusCode.Forbidden;
                missingRequestedTenant.Data = new List<UserListDto>();
                return missingRequestedTenant;
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
            var empty = PaginatedResult<UserListDto>.Success(new List<UserListDto>(), 0, sanitizedPage, sanitizedPageSize);
            empty.CurrentPage = sanitizedPage;
            empty.PageSize = sanitizedPageSize;
            return empty;
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

        if (!string.IsNullOrWhiteSpace(currentUserId))
        {
            query = query.Where(u => u.Id != currentUserId);
        }

        if (request.OrderBy.Any())
        {
            var ordering = string.Join(",", request.OrderBy);
            try
            {
                query = query.OrderBy(ordering);
            }
            catch (ParseException ex)
            {
                _logger.LogWarning("Invalid orderBy value '{0}' supplied. {1}", ordering, ex.Message);
            }
        }

        var result = await query.ToPaginatedListAsync(pageIndex, sanitizedPageSize);
        result.CurrentPage = sanitizedPage;
        result.PageSize = sanitizedPageSize;
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
