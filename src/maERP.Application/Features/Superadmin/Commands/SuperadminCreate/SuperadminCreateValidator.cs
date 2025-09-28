using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Superadmin.Commands.SuperadminCreate;

public class SuperadminCreateValidator : TenantBaseValidator<SuperadminCreateCommand>
{
    private readonly ITenantRepository _tenantRepository;

    public SuperadminCreateValidator(ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;

        RuleFor(q => q.TenantCode)
            .MustAsync(IsUniqueCodeAsync).WithMessage("Tenant with the same code already exists.");
    }

    private async Task<bool> IsUniqueCodeAsync(string tenantCode, CancellationToken cancellationToken)
    {
        return !await _tenantRepository.TenantCodeExistsAsync(tenantCode);
    }
}