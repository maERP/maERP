using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace maERP.Application.Features.Superadmin.UserTenants.Commands.AssignUserToTenant;

public class AssignUserToTenantHandler : IRequestHandler<AssignUserToTenantCommand, Result<int>>
{
    private readonly IUserTenantRepository _userTenantRepository;
    private readonly ITenantRepository _tenantRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IValidator<AssignUserToTenantCommand> _validator;

    public AssignUserToTenantHandler(
        IUserTenantRepository userTenantRepository,
        ITenantRepository tenantRepository,
        UserManager<ApplicationUser> userManager,
        IValidator<AssignUserToTenantCommand> validator)
    {
        _userTenantRepository = userTenantRepository;
        _tenantRepository = tenantRepository;
        _userManager = userManager;
        _validator = validator;
    }

    public async Task<Result<int>> Handle(AssignUserToTenantCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return Result<int>.Fail(ResultStatusCode.BadRequest, errors);
        }

        // Check if user exists
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return Result<int>.Fail(ResultStatusCode.BadRequest, "User not found");
        }

        // Check if tenant exists
        if (!await _tenantRepository.ExistsAsync(request.TenantId))
        {
            return Result<int>.Fail(ResultStatusCode.BadRequest, "Tenant not found");
        }

        // Check if assignment already exists
        var assignmentExists = await _userTenantRepository.Entities
            .AsNoTracking()
            .AnyAsync(ut => ut.UserId == request.UserId && ut.TenantId == request.TenantId, cancellationToken);

        if (assignmentExists)
        {
            return Result<int>.Fail(ResultStatusCode.BadRequest, "User is already assigned to this tenant");
        }

        // If this should be the default tenant, remove default flag from other assignments
        if (request.IsDefault)
        {
            var userAssignments = await _userTenantRepository.Entities
                .Where(ut => ut.UserId == request.UserId && ut.IsDefault)
                .ToListAsync(cancellationToken);

            foreach (var assignment in userAssignments)
            {
                assignment.IsDefault = false;
                await _userTenantRepository.UpdateAsync(assignment);
            }
        }

        var userTenant = new Domain.Entities.UserTenant
        {
            UserId = request.UserId,
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
            return Result<int>.Fail(ResultStatusCode.BadRequest, "User is already assigned to this tenant");
        }

        var success = Result<int>.Success(1, "User successfully assigned to tenant");
        success.StatusCode = ResultStatusCode.Created;
        return success;
    }
}
