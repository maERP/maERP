using FluentValidation;
using maERP.Domain.Validators;
using maERP.UI.Features.Customers.ViewModels;

namespace maERP.UI.Features.Customers.Validators;

/// <summary>
/// Client-seitiger Validator für Customer-Eingaben in der UI.
/// Erweitert CustomerBaseValidator um UI-spezifische Validierungen.
///
/// WICHTIG:
/// - Enthält nur synchrone Regeln für sofortiges Feedback
/// - KEINE Datenbankzugriffe oder Async-Operationen
/// - Wird in CustomerInputViewModel verwendet
/// - Server führt zusätzlich eigene Validierungen mit DB-Zugriff durch
/// </summary>
public class CustomerClientValidator : CustomerBaseValidator<CustomerInputViewModel>
{
    public CustomerClientValidator()
    {
        // UI-spezifische Formatierungs-Regeln

        // Telefonnummer-Format: Nur Ziffern, Leerzeichen, +, -, ( und )
        RuleFor(x => x.Phone)
            .Matches(@"^[\d\s\+\-\(\)]*$")
            .WithMessage("Telefonnummer enthält ungültige Zeichen. Erlaubt sind: Ziffern, Leerzeichen, +, -, ( und )")
            .When(x => !string.IsNullOrEmpty(x.Phone));

        // Zusätzliche UI-spezifische Regeln können hier hinzugefügt werden
        // z.B. strengere Pflichtfeld-Prüfungen, Format-Validierungen, etc.
    }
}
