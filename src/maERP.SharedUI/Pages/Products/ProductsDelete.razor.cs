using maERP.SharedUI.Services;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Products;

public partial class ProductsDelete
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required HttpService HttpService { get; set; }
    
    [Parameter]
    public int productId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (productId > 0)
        {
            await HttpService.DeleteAsync("/api/v1/Products/" + productId);
            NavigationManager.NavigateTo("/Products");
        }
    }
}