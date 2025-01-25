using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Products;

public partial class ProductsDelete
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IProductService ProductService { get; set; }

    [Parameter]
    public int productId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (productId > 0)
        {
            await ProductService.DeleteProduct(productId);
            NavigationManager.NavigateTo("/Products");
        }
    }
}