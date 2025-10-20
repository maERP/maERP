using FluentValidation;

namespace maERP.Application.Features.Tenant.Queries.TenantList;

public class TenantListValidator : AbstractValidator<TenantListQuery>
{
    public TenantListValidator()
    {
        RuleFor(p => p.UserId)
            .NotNull().WithMessage("{PropertyName} must not be null.")
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(p => p.PageNumber)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(p => p.PageSize)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("{PropertyName} must not exceed 100.");
    }
}
