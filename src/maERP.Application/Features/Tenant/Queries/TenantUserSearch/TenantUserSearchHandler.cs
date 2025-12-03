using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Mediator;
using maERP.Domain.Dtos.User;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Tenant.Queries.TenantUserSearch;

/// <summary>
/// Handler for searching users by email.
/// Requires the current user to have RoleManageUser permission on the tenant.
/// </summary>
public class TenantUserSearchHandler : IRequestHandler<TenantUserSearchQuery, Result<UserListDto?>>
{
    private readonly IUserTenantRepository _userTenantRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IValidator<TenantUserSearchQuery> _validator;

    public TenantUserSearchHandler(
        IUserTenantRepository userTenantRepository,
        UserManager<ApplicationUser> userManager,
        IValidator<TenantUserSearchQuery> validator)
    {
        _userTenantRepository = userTenantRepository;
        _userManager = userManager;
        _validator = validator;
    }

    public async Task<Result<UserListDto?>> Handle(TenantUserSearchQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return Result<UserListDto?>.Fail(ResultStatusCode.BadRequest, errors);
        }

        // Check if the current user has RoleManageUser permission on this tenant
        var currentUserTenant = await _userTenantRepository.Entities
            .AsNoTracking()
            .FirstOrDefaultAsync(ut => ut.UserId == request.CurrentUserId && ut.TenantId == request.TenantId, cancellationToken);

        if (currentUserTenant == null || !currentUserTenant.RoleManageUser)
        {
            return Result<UserListDto?>.Fail(ResultStatusCode.Forbidden, "You do not have permission to manage users for this tenant");
        }

        // Search for user by email (exact match, case-insensitive)
        var user = await _userManager.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.NormalizedEmail == request.Email.ToUpperInvariant(), cancellationToken);

        if (user == null)
        {
            return Result<UserListDto?>.Fail(ResultStatusCode.NotFound, "User not found with this email address");
        }

        // Check if user is already assigned to this tenant
        var alreadyAssigned = await _userTenantRepository.Entities
            .AsNoTracking()
            .AnyAsync(ut => ut.UserId == user.Id && ut.TenantId == request.TenantId, cancellationToken);

        if (alreadyAssigned)
        {
            return Result<UserListDto?>.Fail(ResultStatusCode.BadRequest, "User is already a member of this tenant");
        }

        var dto = new UserListDto
        {
            Id = user.Id,
            Email = user.Email ?? string.Empty,
            Firstname = user.Firstname,
            Lastname = user.Lastname,
            DateCreated = user.DateCreated
        };

        return Result<UserListDto?>.Success(dto, "User found");
    }
}
