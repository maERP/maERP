using FluentValidation;
using maERP.Domain.Validators;
using maERP.UI.Shared.Features.Warehouses.ViewModels;

namespace maERP.UI.Shared.Features.Warehouses.Validators;

/// <summary>
/// Client-seitiger Validator für Warehouse-Eingaben in der UI.
/// Erweitert WarehouseBaseValidator um UI-spezifische Validierungen.
/// </summary>
public class WarehouseClientValidator : WarehouseBaseValidator<WarehouseInputViewModel>
{
    public WarehouseClientValidator()
    {
        // UI-spezifische Regeln können hier hinzugefügt werden
    }
}
