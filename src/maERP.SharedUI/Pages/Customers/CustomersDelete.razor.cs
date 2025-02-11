using maERP.SharedUI.Services;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Customers;

public partial class CustomersDelete
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required HttpService HttpService { get; set; }
    
    [Parameter]
    public int customerId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (customerId > 0)
        {
            await HttpService.DeleteAsync("/api/v1/Customers/" + customerId);
            NavigationManager.NavigateTo("/Customers");
        }
    }
}