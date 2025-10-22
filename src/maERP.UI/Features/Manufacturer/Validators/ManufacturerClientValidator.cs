using FluentValidation;
using maERP.Domain.Validators;
using maERP.UI.Features.Manufacturer.ViewModels;

namespace maERP.UI.Features.Manufacturer.Validators;

/// <summary>
/// Client-seitiger Validator für Manufacturer-Eingaben in der UI.
/// Erweitert ManufacturerBaseValidator um UI-spezifische Validierungen.
/// </summary>
public class ManufacturerClientValidator : ManufacturerBaseValidator<ManufacturerInputViewModel>
{
    public ManufacturerClientValidator()
    {
        // UI-spezifische Regeln können hier hinzugefügt werden
    }
}
