using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Tenant.Commands.TenantUpdate;

public class TenantUpdateValidator : TenantBaseValidator<TenantUpdateCommand>
{
    private readonly ITenantRepository _tenantRepository;
    private readonly ITenantPermissionService _tenantPermissionService;

    public TenantUpdateValidator(
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
            .MustAsync(UserHasPermission).WithMessage("You do not have permission to manage this tenant")
            .MustAsync(IsUniqueAsync).WithMessage("Tenant with the same name already exists.");
    }

    private async Task<bool> TenantExists(TenantUpdateCommand command, CancellationToken cancellationToken)
    {
        return await _tenantRepository.GetByIdAsync(command.TenantId, true) != null;
    }

    private async Task<bool> UserHasPermission(TenantUpdateCommand command, CancellationToken cancellationToken)
    {
        return await _tenantPermissionService.CanManageTenantAsync(command.UserId, command.TenantId, cancellationToken);
    }

    private async Task<bool> IsUniqueAsync(TenantUpdateCommand command, CancellationToken cancellationToken)
    {
        var tenant = new Domain.Entities.Tenant
        {
            Name = command.Name,
        };

        return await _tenantRepository.IsUniqueAsync(tenant, command.TenantId);
    }
}
