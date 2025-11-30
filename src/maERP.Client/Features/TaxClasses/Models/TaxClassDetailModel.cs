using maERP.Client.Features.TaxClasses.Services;
using maERP.Domain.Dtos.TaxClass;

namespace maERP.Client.Features.TaxClasses.Models;

/// <summary>
/// Model for tax class detail page using MVUX pattern.
/// Receives tax class ID from navigation data.
/// </summary>
public partial record TaxClassDetailModel
{
    private readonly ITaxClassService _taxClassService;
    private readonly INavigator _navigator;
    private readonly Guid _taxClassId;

    public TaxClassDetailModel(
        ITaxClassService taxClassService,
        INavigator navigator,
        TaxClassDetailData data)
    {
        _taxClassService = taxClassService;
        _navigator = navigator;
        _taxClassId = data.TaxClassId;
    }

    /// <summary>
    /// Feed that loads the tax class details.
    /// </summary>
    public IFeed<TaxClassDetailDto> TaxClass => Feed.Async(async ct =>
    {
        var taxClass = await _taxClassService.GetTaxClassAsync(_taxClassId, ct);
        return taxClass ?? throw new InvalidOperationException($"Tax class {_taxClassId} not found");
    });

    /// <summary>
    /// Navigate to edit tax class page (for future implementation).
    /// </summary>
    public async Task EditTaxClass()
    {
        // TODO: Implement when TaxClassEditPage is available
        // await _navigator.NavigateDataAsync(this, new TaxClassEditData(_taxClassId));
        await Task.CompletedTask;
    }

    /// <summary>
    /// Navigate back to tax class list.
    /// </summary>
    public async Task GoBack()
    {
        await _navigator.NavigateBackAsync(this);
    }
}
