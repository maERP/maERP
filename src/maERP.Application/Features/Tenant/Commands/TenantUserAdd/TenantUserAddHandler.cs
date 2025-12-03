using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Mediator;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Tenant.Commands.TenantUserAdd;

/// <summary>
/// Handler for adding a user to a tenant.
/// Requires the current user to have RoleManageUser permission on the tenant.
/// </summary>
public class TenantUserAddHandler : IRequestHandler<TenantUserAddCommand, Result<bool>>
{
    private readonly IUserTenantRepository _userTenantRepository;
    private readonly ITenantRepository _tenantRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IValidator<TenantUserAddCommand> _validator;

    public TenantUserAddHandler(
        IUserTenantRepository userTenantRepository,
        ITenantRepository tenantRepository,
        UserManager<ApplicationUser> userManager,
        IValidator<TenantUserAddCommand> validator)
    {
        _userTenantRepository = userTenantRepository;
        _tenantRepository = tenantRepository;
        _userManager = userManager;
        _validator = validator;
    }

    public async Task<Result<bool>> Handle(TenantUserAddCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return Result<bool>.Fail(ResultStatusCode.BadRequest, errors);
        }

        // Check if the current user has RoleManageUser permission on this tenant
        var currentUserTenant = await _userTenantRepository.Entities
            .AsNoTracking()
            .FirstOrDefaultAsync(ut => ut.UserId == request.CurrentUserId && ut.TenantId == request.TenantId, cancellationToken);

        if (currentUserTenant == null || !currentUserTenant.RoleManageUser)
        {
            return Result<bool>.Fail(ResultStatusCode.Forbidden, "You do not have permission to manage users for this tenant");
        }

        // Check if tenant exists
        if (!await _tenantRepository.ExistsAsync(request.TenantId))
        {
            return Result<bool>.Fail(ResultStatusCode.NotFound, "Tenant not found");
        }

        // Find user by email
        var user = await _userManager.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.NormalizedEmail == request.Email.ToUpperInvariant(), cancellationToken);

        if (user == null)
        {
            return Result<bool>.Fail(ResultStatusCode.NotFound, "User not found with this email address");
        }

        // Check if user is already assigned to this tenant
        var alreadyAssigned = await _userTenantRepository.Entities
            .AsNoTracking()
            .AnyAsync(ut => ut.UserId == user.Id && ut.TenantId == request.TenantId, cancellationToken);

        if (alreadyAssigned)
        {
            return Result<bool>.Fail(ResultStatusCode.BadRequest, "User is already a member of this tenant");
        }

        // If this should be the default tenant, remove default flag from other assignments for the target user
        if (request.IsDefault)
        {
            var userAssignments = await _userTenantRepository.Entities
                .Where(ut => ut.UserId == user.Id && ut.IsDefault)
                .ToListAsync(cancellationToken);

            foreach (var assignment in userAssignments)
            {
                assignment.IsDefault = false;
                await _userTenantRepository.UpdateAsync(assignment);
            }
        }

        // Create the user-tenant assignment
        var userTenant = new UserTenant
        {
            UserId = user.Id,
            TenantId = request.TenantId,
            IsDefault = request.IsDefault,
            RoleManageUser = request.RoleManageUser,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        try
        {
            await _userTenantRepository.CreateAsync(userTenant);
        }
        catch (Exception ex) when (ex is DbUpdateException or ArgumentException)
        {
            return Result<bool>.Fail(ResultStatusCode.BadRequest, "Failed to add user to tenant");
        }

        var success = Result<bool>.Success(true, "User successfully added to tenant");
        success.StatusCode = ResultStatusCode.Created;
        return success;
    }
}
