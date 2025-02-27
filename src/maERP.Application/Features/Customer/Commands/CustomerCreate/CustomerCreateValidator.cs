using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Customer.Commands.CustomerCreate;

public class CustomerCreateValidator : CustomerBaseValidator<CustomerCreateCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerCreateValidator(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
            
        RuleFor(q => q)    
            .MustAsync(IsUniqueAsync).WithMessage("Customer with the same values already exists.");
    }

    private async Task<bool> IsUniqueAsync(CustomerCreateCommand command, CancellationToken cancellationToken)
    {
        var customer = new Domain.Entities.Customer
        {
            Firstname = command.Firstname,
        };
        
        return await _customerRepository.IsUniqueAsync(customer);     
    }
}
