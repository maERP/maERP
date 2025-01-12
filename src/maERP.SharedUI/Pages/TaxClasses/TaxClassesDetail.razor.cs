using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.TaxClass;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.TaxClasses;

public partial class TaxClassesDetail
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required ITaxClassService _taxClassService { get; set; }

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