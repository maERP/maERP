using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Customer.Commands.DeleteCustomerCommand;

public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public DeleteCustomerCommandValidator(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        
        RuleFor(c => c)
            .MustAsync(CustomerExists).WithMessage("Customer not found");
    }
    
    private async Task<bool> CustomerExists(DeleteCustomerCommand command, CancellationToken cancellationToken)
    {
        return await _customerRepository.GetByIdAsync(command.Id) != null;
    }
}
