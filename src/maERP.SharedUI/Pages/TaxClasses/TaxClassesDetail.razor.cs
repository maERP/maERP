using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.TaxClass;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.TaxClasses;

public partial class TaxClassesDetail
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required ITaxClassService TaxClassService { get; set; }

    [Parameter]
    public int taxClassId { get; set; }

    protected string Title = "Steuers�tze";

    protected TaxClassVm TaxClass = new();

    protected override async Task OnParametersSetAsync()
    {
        if (taxClassId != 0)
        {
            Title = "Bearbeiten";
            TaxClass = await TaxClassService.GetTaxClassDetails(taxClassId);
        }
        else Title = "nicht gefunden";

        await Task.CompletedTask;
    }
}