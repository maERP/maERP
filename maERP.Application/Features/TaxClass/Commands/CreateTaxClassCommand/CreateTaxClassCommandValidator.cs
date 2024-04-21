using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.TaxClass.Commands.CreateTaxClassCommand;

public class CreateTaxClassCommandValidator : AbstractValidator<CreateTaxClassCommand>
{
    private readonly ITaxClassRepository _taxClassRepository;

    public CreateTaxClassCommandValidator(ITaxClassRepository taxClassRepository)
    {
        _taxClassRepository = taxClassRepository;

        RuleFor(p => p.TaxRate)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .InclusiveBetween(0, 100).WithMessage("{PropertyName} must between 0 and 100.");

        RuleFor(q => q)
            .MustAsync(TaxClassUnique).WithMessage("Tax class with the same tax rate already exists.");
    }

    private async Task<bool> TaxClassUnique(CreateTaxClassCommand command, CancellationToken cancellationToken)
    {
        return await _taxClassRepository.GetByTaxRateAsync(command.TaxRate) == null;
    }
}
