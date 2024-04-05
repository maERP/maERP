using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.TaxClass.Commands.CreateTaxClassCommand;

public class CreateTaxClassCommandValidator : AbstractValidator<CreateTaxClassCommand>
{
    private readonly ITaxClassRepository _taxClassRepository;

    public CreateTaxClassCommandValidator(ITaxClassRepository taxClassRepository)
    {
        _taxClassRepository = taxClassRepository;

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(q => q)
            .MustAsync(TaxClassUnique).WithMessage("TaxClass with the same name already exists.");
    }

    private async Task<bool> TaxClassUnique(CreateTaxClassCommand command, CancellationToken cancellationToken)
    {
        // TODO: Implement unique TaxClass name validation
        await Task.CompletedTask;
        return true;
    }
}
