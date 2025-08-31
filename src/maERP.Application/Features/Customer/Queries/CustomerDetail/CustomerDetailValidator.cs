using FluentValidation;

namespace maERP.Application.Features.Customer.Queries.CustomerDetail;

public class CustomerDetailValidator : AbstractValidator<CustomerDetailQuery>
{
    public CustomerDetailValidator()
    {
        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
    }
}