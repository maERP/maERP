using FluentValidation;
using maERP.Domain.Validators;
using maERP.UI.Shared.Features.SalesChannels.ViewModels;

namespace maERP.UI.Shared.Features.SalesChannels.Validators;

/// <summary>
/// Client-seitiger Validator für SalesChannel-Eingaben in der UI.
/// Erweitert SalesChannelBaseValidator um UI-spezifische Validierungen.
/// </summary>
public class SalesChannelClientValidator : SalesChannelBaseValidator<SalesChannelInputViewModel>
{
    public SalesChannelClientValidator()
    {
        // UI-spezifische Regeln können hier hinzugefügt werden
    }
}
