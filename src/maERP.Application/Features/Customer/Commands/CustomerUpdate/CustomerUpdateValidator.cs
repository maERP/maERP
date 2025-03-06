using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Customer.Commands.CustomerUpdate;

public class CustomerUpdateValidator : CustomerBaseValidator<CustomerInputCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerUpdateValidator(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
        
        RuleFor(c => c)
            .MustAsync(CustomerExists).WithMessage("Customer not found");
    }
    
    private async Task<bool> CustomerExists(CustomerInputCommand command, CancellationToken cancellationToken)
    {
        return await _customerRepository.ExistsAsync(command.Id);
    }
}