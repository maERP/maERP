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
    }
}