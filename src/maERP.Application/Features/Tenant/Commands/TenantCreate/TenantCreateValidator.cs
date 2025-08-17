using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Tenant.Commands.TenantCreate;

public class TenantCreateValidator : TenantBaseValidator<TenantCreateCommand>
{
    private readonly ITenantRepository _tenantRepository;

    public TenantCreateValidator(ITenantRepository tenantRepository)
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