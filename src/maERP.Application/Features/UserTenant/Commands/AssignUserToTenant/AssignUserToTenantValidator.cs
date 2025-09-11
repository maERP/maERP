using FluentValidation;

namespace maERP.Application.Features.UserTenant.Commands.AssignUserToTenant;

public class AssignUserToTenantValidator : AbstractValidator<AssignUserToTenantCommand>
{
    public AssignUserToTenantValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required");

        RuleFor(x => x.TenantId)
            .NotEmpty()
            .WithMessage("Tenant ID is required");
    }
}