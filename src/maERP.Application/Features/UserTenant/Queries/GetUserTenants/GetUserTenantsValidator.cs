using FluentValidation;

namespace maERP.Application.Features.UserTenant.Queries.GetUserTenants;

public class GetUserTenantsValidator : AbstractValidator<GetUserTenantsQuery>
{
    public GetUserTenantsValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required");
    }
}