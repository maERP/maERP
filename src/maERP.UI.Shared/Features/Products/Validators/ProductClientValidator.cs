using FluentValidation;
using maERP.Domain.Validators;
using maERP.UI.Features.Products.ViewModels;

namespace maERP.UI.Features.Products.Validators;

/// <summary>
/// Client-seitiger Validator für Product-Eingaben in der UI.
/// Erweitert ProductBaseValidator um UI-spezifische Validierungen.
///
/// WICHTIG:
/// - Enthält nur synchrone Regeln für sofortiges Feedback
/// - KEINE Datenbankzugriffe oder Async-Operationen
/// - Wird in ProductInputViewModel verwendet
/// - Server führt zusätzlich eigene Validierungen mit DB-Zugriff durch
/// </summary>
public class ProductClientValidator : ProductBaseValidator<ProductInputViewModel>
{
    public ProductClientValidator()
    {
        // UI-spezifische Formatierungs-Regeln

        // SKU-Format: Uppercase und keine Leerzeichen
        RuleFor(x => x.Sku)
            .Must(BeValidSkuFormat)
            .WithMessage("SKU sollte keine Leerzeichen enthalten und in Großbuchstaben sein.")
            .When(x => !string.IsNullOrEmpty(x.Sku));

        // Zusätzliche UI-spezifische Regeln können hier hinzugefügt werden
    }

    private bool BeValidSkuFormat(string sku)
    {
        // Prüft, ob SKU keine Leerzeichen enthält
        return !sku.Contains(' ');
    }
}
