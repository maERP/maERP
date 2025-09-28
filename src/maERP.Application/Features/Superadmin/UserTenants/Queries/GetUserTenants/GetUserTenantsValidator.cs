using FluentValidation;

namespace maERP.Application.Features.Superadmin.UserTenants.Queries.GetUserTenants;

public class GetUserTenantsValidator : AbstractValidator<GetUserTenantsQuery>
{
    public GetUserTenantsValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required")
            .Must(BeValidGuid)
            .WithMessage("User ID must be a valid GUID format");
    }

    private static bool BeValidGuid(string userId)
    {
        return !string.IsNullOrEmpty(userId) && Guid.TryParse(userId, out _);
    }
}