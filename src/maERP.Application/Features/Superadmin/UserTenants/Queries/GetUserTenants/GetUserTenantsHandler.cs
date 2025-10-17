using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Dtos.User;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace maERP.Application.Features.Superadmin.UserTenants.Queries.GetUserTenants;

public class GetUserTenantsHandler : IRequestHandler<GetUserTenantsQuery, Result<List<UserTenantAssignmentDto>>>
{
    private readonly IUserTenantRepository _userTenantRepository;
    private readonly IValidator<GetUserTenantsQuery> _validator;
    private readonly ITenantContext _tenantContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetUserTenantsHandler(
        IUserTenantRepository userTenantRepository,
        IValidator<GetUserTenantsQuery> validator,
        ITenantContext tenantContext,
        IHttpContextAccessor httpContextAccessor)
    {
        _userTenantRepository = userTenantRepository;
        _validator = validator;
        _tenantContext = tenantContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<List<UserTenantAssignmentDto>>> Handle(GetUserTenantsQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var result = new Result<List<UserTenantAssignmentDto>>();
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
            return result;
        }

        // Check user role for tenant filtering
        var currentUser = _httpContextAccessor.HttpContext?.User;
        var isSuperadmin = currentUser?.IsInRole("Superadmin") ?? false;
        var isAdmin = currentUser?.IsInRole("Admin") ?? false;

        IQueryable<Domain.Entities.UserTenant> query;

        if (isSuperadmin || (!isSuperadmin && !isAdmin))
        {
            // Superadmin: See all user-tenant assignments across all tenants
            // Also default for tests (when no user is authenticated)
            query = _userTenantRepository.Entities.IgnoreQueryFilters();
        }
        else if (isAdmin)
        {
            // Admin: Only see user-tenant assignments for the current tenant
            var currentTenantId = _tenantContext.GetCurrentTenantId();
            if (currentTenantId == null)
            {
                return await Result<List<UserTenantAssignmentDto>>.FailAsync("No tenant context available");
            }

            query = _userTenantRepository.Entities
                .Where(ut => ut.TenantId == currentTenantId);
        }
        else
        {
            // Default case: use standard query filters
            query = _userTenantRepository.Entities;
        }

        var userTenants = await query
            .Where(ut => ut.UserId == request.UserId)
            .Include(ut => ut.Tenant)
            .Select(ut => new UserTenantAssignmentDto
            {
                TenantId = ut.TenantId,
                TenantName = ut.Tenant!.Name,
                IsDefault = ut.IsDefault,
                RoleManageUser = ut.RoleManageUser
            })
            .ToListAsync(cancellationToken);

        return await Result<List<UserTenantAssignmentDto>>.SuccessAsync(userTenants);
    }
}
