using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassUpdate;

public class TaxClassUpdateValidator : AbstractValidator<TaxClassUpdateCommand>
{
    private readonly ITaxClassRepository _taxClassRepository;

    public TaxClassUpdateValidator(ITaxClassRepository taxClassRepository)
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
            .MustAsync(IsUniqueAsync).WithMessage("TaxClass with the same tax rate already exists.");
    }
    
    private async Task<bool> TaxClassExists(TaxClassUpdateCommand command, CancellationToken cancellationToken)
    {
        return await _taxClassRepository.GetByIdAsync(command.Id, true) != null;
    }

    private async Task<bool> IsUniqueAsync(TaxClassUpdateCommand command, CancellationToken cancellationToken)
    {
        var taxClass = new Domain.Entities.TaxClass
        {
            TaxRate = command.TaxRate
        };

        return await _taxClassRepository.IsUniqueAsync(taxClass, command.Id);
    }
}