using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Superadmin.Commands.SuperadminDelete;

public class SuperadminDeleteValidator : AbstractValidator<SuperadminDeleteCommand>
{
    private readonly ITenantRepository _tenantRepository;

    public SuperadminDeleteValidator(ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;

        RuleFor(q => q.Id)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");

        RuleFor(q => q)
            .MustAsync(TenantExistsAsync).WithMessage("Tenant not found.")
            .MustAsync(CanBeDeletedAsync).WithMessage("Tenant cannot be deleted because it has associated users.");
    }

    private async Task<bool> TenantExistsAsync(SuperadminDeleteCommand command, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.GetByIdAsync(command.Id);
        return tenant != null;
    }

    private async Task<bool> CanBeDeletedAsync(SuperadminDeleteCommand command, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.GetByIdAsync(command.Id);
        return tenant?.UserTenants?.Any() != true;
    }
}