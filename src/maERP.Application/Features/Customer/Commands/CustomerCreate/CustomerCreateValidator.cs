using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Customer.Commands.CustomerCreate;

public class CustomerCreateValidator : AbstractValidator<CustomerCreateCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerCreateValidator(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;

        RuleFor(p => p.Firstname)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MinimumLength(1).WithMessage("{PropertyName} must be at least 1 character.")
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        
        RuleFor(p => p.Lastname)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MinimumLength(1).WithMessage("{PropertyName} must be at least 1 character.")
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
            
        RuleFor(q => q)    
            .MustAsync(IsUniqueAsync).WithMessage("Customer with the same values already exists.");
    }

    private async Task<bool> IsUniqueAsync(CustomerCreateCommand command, CancellationToken cancellationToken)
    {
        var customer = new Domain.Entities.Customer()
        {
            Firstname = command.Firstname,
        };
        
        return await _customerRepository.IsUniqueAsync(customer);     
    }
}
