using maERP.SharedUI.Services;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.TaxClasses;

public partial class TaxClassesDelete
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required HttpService HttpService { get; set; }

    [Parameter]
    public int taxClassId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (taxClassId > 0)
        
            await HttpService.DeleteAsync("/api/v1/TaxClasses/" + taxClassId);
            NavigationManager.NavigateTo("/TaxClasses");
    }
}