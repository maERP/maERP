using FluentValidation;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Validators;

public class ProductBaseValidator<T> : AbstractValidator<T> where T : IProductInputModel
{
    public ProductBaseValidator()
    {
        RuleFor(p => p.Sku)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MinimumLength(3).WithMessage("{PropertyName} must be more than {MinLength} characters.")
            .MaximumLength(255).WithMessage("{PropertyName} must be less than {MaxLength} characters.");

        RuleFor(p => p.Name)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(255).WithMessage("{PropertyName} must be less than {MaxLength} characters.");

        RuleFor(p => p.Price)
            .NotNull().WithMessage("{PropertyName} is required.")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}.");

        RuleFor(p => p.TaxClassId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}.");
    }
}