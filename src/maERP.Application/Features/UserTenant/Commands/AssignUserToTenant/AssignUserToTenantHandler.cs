using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.AspNetCore.Identity;

namespace maERP.Application.Features.UserTenant.Commands.AssignUserToTenant;

public class AssignUserToTenantHandler : IRequestHandler<AssignUserToTenantCommand, Result<Guid>>
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

    public async Task<Result<Guid>> Handle(AssignUserToTenantCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return await Result<Guid>.FailAsync(validationResult.ToString());
        }

        // Check if user exists
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return await Result<Guid>.FailAsync("User not found");
        }

        // Check if tenant exists
        if (!await _tenantRepository.ExistsAsync(request.TenantId))
        {
            return await Result<Guid>.FailAsync("Tenant not found");
        }

        // Check if assignment already exists
        var existingAssignment = _userTenantRepository.Entities
            .FirstOrDefault(ut => ut.UserId == request.UserId && ut.TenantId == request.TenantId);

        if (existingAssignment != null)
        {
            return await Result<Guid>.FailAsync("User is already assigned to this tenant");
        }

        // If this should be the default tenant, remove default flag from other assignments
        if (request.IsDefault)
        {
            var userAssignments = _userTenantRepository.Entities
                .Where(ut => ut.UserId == request.UserId && ut.IsDefault)
                .ToList();

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
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var id = await _userTenantRepository.CreateAsync(userTenant);

        return await Result<Guid>.SuccessAsync(id, "User successfully assigned to tenant");
    }
}