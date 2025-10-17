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
    }
}