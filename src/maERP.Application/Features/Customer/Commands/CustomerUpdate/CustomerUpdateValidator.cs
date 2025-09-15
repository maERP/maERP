using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Customer.Commands.CustomerUpdate;

public class CustomerUpdateValidator : CustomerBaseValidator<CustomerUpdateCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerUpdateValidator(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;

        // Add ID validation for Zero-GUID first
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty).WithMessage("Customer ID cannot be empty.");

        // Only check existence if ID is valid
        RuleFor(c => c)
            .MustAsync(CustomerExists).WithMessage("Customer not found")
            .When(c => c.Id != Guid.Empty);

        // Add uniqueness validation for updates
        RuleFor(q => q)
            .MustAsync(IsUniqueAsync).WithMessage("Customer with the same values already exists.")
            .When(c => c.Id != Guid.Empty);
    }

    private async Task<bool> CustomerExists(CustomerUpdateCommand command, CancellationToken cancellationToken)
    {
        return await _customerRepository.ExistsGloballyAsync(command.Id);
    }

    private async Task<bool> IsUniqueAsync(CustomerUpdateCommand command, CancellationToken cancellationToken)
    {
        var customer = new Domain.Entities.Customer
        {
            Firstname = command.Firstname,
            Lastname = command.Lastname
        };

        return await _customerRepository.IsUniqueAsync(customer, command.Id);
    }
}