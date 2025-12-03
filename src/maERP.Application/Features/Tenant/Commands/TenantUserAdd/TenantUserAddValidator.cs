using FluentValidation;

namespace maERP.Application.Features.Tenant.Commands.TenantUserAdd;

public class TenantUserAddValidator : AbstractValidator<TenantUserAddCommand>
{
    public TenantUserAddValidator()
    {
        RuleFor(x => x.TenantId)
            .NotEmpty()
            .WithMessage("Tenant ID is required");

        RuleFor(x => x.CurrentUserId)
            .NotEmpty()
            .WithMessage("Current user ID is required");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email address is required")
            .EmailAddress()
            .WithMessage("A valid email address is required");
    }
}
