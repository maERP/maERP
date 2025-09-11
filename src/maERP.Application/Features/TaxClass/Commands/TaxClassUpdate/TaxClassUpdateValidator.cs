using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassUpdate;

public class TaxClassUpdateValidator : TaxClassBaseValidator<TaxClassUpdateCommand>
{
    private readonly ITaxClassRepository _taxClassRepository;

    public TaxClassUpdateValidator(ITaxClassRepository taxClassRepository)
    {
        _taxClassRepository = taxClassRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");

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