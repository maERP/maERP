using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.TaxClass.Commands.UpdateTaxClassCommand;

public class UpdateTaxClassCommandValidator : AbstractValidator<UpdateTaxClassCommand>
{
    private readonly ITaxClassRepository _taxClassRepository;

    public UpdateTaxClassCommandValidator(ITaxClassRepository taxClassRepository)
    {
        _taxClassRepository = taxClassRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        
        RuleFor(p => p.TaxRate)
            //.NotNull().WithMessage("{PropertyName} is required.")
            //.NotEmpty().WithMessage("{PropertyName} is required.")
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must greater or equal 0.")
            .LessThanOrEqualTo(100).WithMessage("{PropertyName} must less or equal 100.");

        RuleFor(t => t)
            .MustAsync(TaxClassExists).WithMessage("TaxClass not found")
            .Must(IsUnique).WithMessage("TaxClass with the same tax rate already exists.");
    }
    
    private async Task<bool> TaxClassExists(UpdateTaxClassCommand command, CancellationToken cancellationToken)
    {
        return await _taxClassRepository.GetByIdAsync(command.Id) != null;
    }

    private bool IsUnique(UpdateTaxClassCommand command)
    {
        var taxClass = new maERP.Domain.Models.TaxClass
        {
            TaxRate = command.TaxRate
        };

        return _taxClassRepository.IsUnique(taxClass, command.Id);
    }
}