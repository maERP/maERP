using maERP.SharedUI.Models.TaxClass;

namespace maERP.SharedUI.Pages.TaxClasses;

public partial class TaxClasses
{

    private ICollection<TaxClassVM>? taxClasses;

    protected override async Task OnInitializedAsync()
    {
        taxClasses = await _taxClassService.GetTaxClasses();
    }
}