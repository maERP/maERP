using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.TaxClasses;

public partial class TaxClassesDelete
{

    [Parameter]
    public int taxClassId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (taxClassId > 0)
        {
            await _taxClassService.DeleteTaxClass(taxClassId);
            _navigationManager.NavigateTo("/TaxClasses");
        }
    }
}