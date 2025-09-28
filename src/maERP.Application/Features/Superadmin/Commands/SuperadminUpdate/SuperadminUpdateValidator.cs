using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Superadmin.Commands.SuperadminUpdate;

public class SuperadminUpdateValidator : TenantBaseValidator<SuperadminUpdateCommand>
{
    private readonly ITenantRepository _tenantRepository;

    public SuperadminUpdateValidator(ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;

        RuleFor(q => q.Id)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");

        RuleFor(q => q)
            .MustAsync(TenantExistsAsync).WithMessage("Tenant not found.");

        RuleFor(q => q)
            .MustAsync(IsUniqueCodeAsync).WithMessage("Tenant with the same code already exists.");
    }

    private async Task<bool> TenantExistsAsync(SuperadminUpdateCommand command, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.GetByIdAsync(command.Id);
        return tenant != null;
    }

    private async Task<bool> IsUniqueCodeAsync(SuperadminUpdateCommand command, CancellationToken cancellationToken)
    {
        return !await _tenantRepository.TenantCodeExistsAsync(command.TenantCode, command.Id);
    }
}