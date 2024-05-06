using maERP.SharedUI.Models.TaxClass;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.TaxClasses;

public partial class TaxClassesDetail
{

    [Parameter]
    public int taxClassId { get; set; }

    protected string Title = "Steuersätze";

    protected TaxClassVM taxClass = new();

    protected override async Task OnParametersSetAsync()
    {
        if (taxClassId != 0)
        {
            Title = "Bearbeiten";
            taxClass = await _taxClassService.GetTaxClassDetails(taxClassId);
        }
        else Title = "nicht gefunden";

        await Task.CompletedTask;
    }
}