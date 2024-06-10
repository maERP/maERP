using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Customer.Commands.CustomerDelete;

public class CustomerDeleteValidator : AbstractValidator<CustomerDeleteCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerDeleteValidator(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        
        RuleFor(c => c)
            .MustAsync(CustomerExists).WithMessage("Customer not found");
    }
    
    private async Task<bool> CustomerExists(CustomerDeleteCommand command, CancellationToken cancellationToken)
    {
        return await _customerRepository.ExistsAsync(command.Id);
    }
}