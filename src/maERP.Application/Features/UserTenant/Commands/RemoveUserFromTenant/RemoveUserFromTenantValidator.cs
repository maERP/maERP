using FluentValidation;

namespace maERP.Application.Features.UserTenant.Commands.RemoveUserFromTenant;

public class RemoveUserFromTenantValidator : AbstractValidator<RemoveUserFromTenantCommand>
{
    public RemoveUserFromTenantValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required");

        RuleFor(x => x.TenantId)
            .NotEmpty()
            .WithMessage("Tenant ID is required");
    }
}