using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassDelete;

public class TaxClassDeleteValidator : AbstractValidator<TaxClassDeleteCommand>
{
    private readonly ITaxClassRepository _taxClassRepository;

    public TaxClassDeleteValidator(ITaxClassRepository taxClassRepository)
    {
        _taxClassRepository = taxClassRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        
        RuleFor(q => q)
            .MustAsync(TaxClassExists).WithMessage("TaxClass not found");
    }
    
    private async Task<bool> TaxClassExists(TaxClassDeleteCommand command, CancellationToken cancellationToken)
    {
        return await _taxClassRepository.ExistsAsync(command.Id);
    }
}