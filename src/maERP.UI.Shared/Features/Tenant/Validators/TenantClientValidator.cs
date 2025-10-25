using FluentValidation;
using maERP.Domain.Validators;
using maERP.UI.Features.Tenant.ViewModels;

namespace maERP.UI.Features.Tenant.Validators;

/// <summary>
/// Client-seitiger Validator für Tenant-Eingaben in der UI.
/// Erweitert TenantBaseValidator um UI-spezifische Validierungen.
///
/// WICHTIG:
/// - Enthält nur synchrone Regeln für sofortiges Feedback
/// - KEINE Datenbankzugriffe oder Async-Operationen
/// - Wird in TenantInputViewModel verwendet
/// - Server führt zusätzlich eigene Validierungen mit DB-Zugriff durch
/// </summary>
public class TenantClientValidator : TenantBaseValidator<TenantInputViewModel>
{
    public TenantClientValidator()
    {
        // UI-spezifische Regeln können hier hinzugefügt werden
    }
}
