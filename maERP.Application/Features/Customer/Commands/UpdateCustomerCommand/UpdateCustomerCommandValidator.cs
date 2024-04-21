using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Customer.Commands.UpdateCustomerCommand;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerCommandValidator(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        
        RuleFor(p => p.TaxRate)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .InclusiveBetween(0, 100).WithMessage("{PropertyName} must between 0 and 100.");

        RuleFor(c => c)
            .MustAsync(CustomerExists).WithMessage("Customer not found");
    }
    
    private async Task<bool> CustomerExists(UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        return await _customerRepository.GetByIdAsync(command.Id) != null;
    }
}