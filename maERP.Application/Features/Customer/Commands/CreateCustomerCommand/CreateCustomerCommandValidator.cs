using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Customer.Commands.CreateCustomerCommand;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandValidator(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;

        RuleFor(p => p.CustomerId)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .InclusiveBetween(0, 100).WithMessage("{PropertyName} must between 0 and 100.");
            
        RuleFor(q => q)    
            .Must(CustomerUnique).WithMessage("Warehouse with the same name already exists.");

    }

    private bool CustomerUnique(CreateCustomerCommand command)
    {
        var customer = new Domain.Models.Customer()
        {
            FirstName = command.Forename,
        };
        
        return _customerRepository.IsUnique(customer);     
    }
}
