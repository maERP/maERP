using FluentValidation;
using maERP.Domain.Validators;
using maERP.UI.Features.Administration.ViewModels;

namespace maERP.UI.Features.Administration.Validators;

/// <summary>
/// Client-seitiger Validator für TaxClass-Eingaben in der UI.
/// Erweitert TaxClassBaseValidator um UI-spezifische Validierungen.
/// </summary>
public class TaxClassClientValidator : TaxClassBaseValidator<TaxClassInputViewModel>
{
    public TaxClassClientValidator()
    {
        // UI-spezifische Regeln können hier hinzugefügt werden
    }
}
