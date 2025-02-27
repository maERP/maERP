using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassCreate;

public class TaxClassCreateValidator : TaxClassBaseValidator<TaxClassCreateCommand>
{
    private readonly ITaxClassRepository _taxClassRepository;

    public TaxClassCreateValidator(ITaxClassRepository taxClassRepository)
    {
        _taxClassRepository = taxClassRepository;

        RuleFor(q => q)
            .MustAsync(IsUniqueAsync).WithMessage("TaxClass with the same tax rate already exists.");
    }

    private async Task<bool> IsUniqueAsync(TaxClassCreateCommand command, CancellationToken cancellationToken)
    {
        var taxClassToCreate = new Domain.Entities.TaxClass
        {
            TaxRate = command.TaxRate
        };

        return await _taxClassRepository.IsUniqueAsync(taxClassToCreate);
    }
}