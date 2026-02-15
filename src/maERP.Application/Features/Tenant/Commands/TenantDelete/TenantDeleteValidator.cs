using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;

namespace maERP.Application.Features.Tenant.Commands.TenantDelete;

public class TenantDeleteValidator : AbstractValidator<TenantDeleteCommand>
{
    private readonly ITenantRepository _tenantRepository;
    private readonly ITenantPermissionService _tenantPermissionService;

    public TenantDeleteValidator(
        ITenantRepository tenantRepository,
        ITenantPermissionService tenantPermissionService)
    {
        _tenantRepository = tenantRepository;
        _tenantPermissionService = tenantPermissionService;

        RuleFor(p => p.TenantId)
            .NotNull().WithMessage("{PropertyName} must not be null.")
            .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");

        RuleFor(p => p.UserId)
            .NotNull().WithMessage("{PropertyName} must not be null.")
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(t => t)
            .MustAsync(TenantExists).WithMessage("Tenant not found")
            .MustAsync(UserHasPermission).WithMessage("You do not have permission to delete this tenant");
    }

    private async Task<bool> TenantExists(TenantDeleteCommand command, CancellationToken cancellationToken)
    {
        return await _tenantRepository.GetByIdAsync(command.TenantId, true) != null;
    }

    private async Task<bool> UserHasPermission(TenantDeleteCommand command, CancellationToken cancellationToken)
    {
        return await _tenantPermissionService.CanManageTenantAsync(command.UserId, command.TenantId, cancellationToken);
    }
}
