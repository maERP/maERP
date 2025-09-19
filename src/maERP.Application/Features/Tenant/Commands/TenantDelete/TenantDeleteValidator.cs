using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Tenant.Commands.TenantDelete;

public class TenantDeleteValidator : AbstractValidator<TenantDeleteCommand>
{
    private readonly ITenantRepository _tenantRepository;

    public TenantDeleteValidator(ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;

        RuleFor(q => q.Id)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");

        RuleFor(q => q)
            .MustAsync(TenantExistsAsync).WithMessage("Tenant not found.")
            .MustAsync(CanBeDeletedAsync).WithMessage("Tenant cannot be deleted because it has associated users.");
    }

    private async Task<bool> TenantExistsAsync(TenantDeleteCommand command, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.GetByIdAsync(command.Id);
        return tenant != null;
    }

    private async Task<bool> CanBeDeletedAsync(TenantDeleteCommand command, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.GetByIdAsync(command.Id);
        return tenant?.UserTenants?.Any() != true;
    }
}