using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Tenant.Commands.TenantCreate;

public class TenantCreateValidator : AbstractValidator<TenantCreateCommand>
{
    private readonly ITenantRepository _tenantRepository;

    public TenantCreateValidator(ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;

        Include(new TenantBaseValidator<TenantCreateCommand>());

        RuleFor(q => q.Name)
            .MustAsync(IsUniqueNameAsync).WithMessage("Tenant with the same name already exists.");

        RuleFor(q => q.UserId)
            .NotEmpty().WithMessage("User ID is required.");
    }

    private async Task<bool> IsUniqueNameAsync(string name, CancellationToken cancellationToken)
    {
        var existingTenant = await _tenantRepository.Entities
            .FirstOrDefaultAsync(t => t.Name == name, cancellationToken);
        return existingTenant == null;
    }
}
