using FluentValidation;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Validators;

/// <summary>
/// Basis-Validator für Product - enthält feldbasierte Validierungsregeln.
/// WICHTIG: Dieser Validator wird von Client (maERP.UI) und Server (maERP.Application) verwendet.
///
/// Client: ProductClientValidator erbt von dieser Klasse und fügt UI-spezifische Regeln hinzu.
/// Server: ProductCreateValidator/ProductUpdateValidator erben von dieser Klasse und fügen DB-Validierungen hinzu.
///
/// Änderungen an diesem Validator wirken sich auf Client UND Server aus!
/// Enthält nur feldbasierte Validierungen ohne externe Dependencies (keine DB-Zugriffe, keine Repositories).
/// </summary>
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
            .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
    }
}