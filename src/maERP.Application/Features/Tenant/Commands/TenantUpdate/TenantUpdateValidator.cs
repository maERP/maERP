using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Tenant.Commands.TenantUpdate;

public class TenantUpdateValidator : TenantBaseValidator<TenantUpdateCommand>
{
    private readonly ITenantRepository _tenantRepository;

    public TenantUpdateValidator(ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;

        RuleFor(q => q.Id)
            .NotNull().WithMessage("{PropertyName} is required.")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(q => q)
            .MustAsync(TenantExistsAsync).WithMessage("Tenant not found.");

        RuleFor(q => q)
            .MustAsync(IsUniqueCodeAsync).WithMessage("Tenant with the same code already exists.");
    }

    private async Task<bool> TenantExistsAsync(TenantUpdateCommand command, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.GetByIdAsync(command.Id);
        return tenant != null;
    }

    private async Task<bool> IsUniqueCodeAsync(TenantUpdateCommand command, CancellationToken cancellationToken)
    {
        return !await _tenantRepository.TenantCodeExistsAsync(command.TenantCode, command.Id);
    }
}