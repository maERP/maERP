using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.TaxClass.Commands.CreateTaxClass;

public class CreateTaxClassValidator : AbstractValidator<CreateTaxClassCommand>
{
    private readonly ITaxClassRepository _taxClassRepository;

    public CreateTaxClassValidator(ITaxClassRepository taxClassRepository)
    {
        _taxClassRepository = taxClassRepository;

        RuleFor(p => p.TaxRate)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .InclusiveBetween(0, 100).WithMessage("{PropertyName} must between 0 and 100.");

        RuleFor(q => q)
            .MustAsync(IsUniqueAsync).WithMessage("TaxClass with the same tax rate already exists.");
    }

    private async Task<bool> IsUniqueAsync(CreateTaxClassCommand command, CancellationToken cancellationToken)
    {
        var taxClassToCreate = new Domain.Models.TaxClass
        {
            TaxRate = command.TaxRate
        };

        return await _taxClassRepository.IsUniqueAsync(taxClassToCreate);
    }
}