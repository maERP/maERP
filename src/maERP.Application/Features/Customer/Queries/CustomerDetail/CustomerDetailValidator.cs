using FluentValidation;

namespace maERP.Application.Features.Customer.Queries.CustomerDetail;

public class CustomerDetailValidator : AbstractValidator<CustomerDetailQuery>
{
    public CustomerDetailValidator()
    {
        RuleFor(p => p.Id)
            .NotNull()
            .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
    }
}