using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Products;

public partial class ProductsDelete
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IProductService _productService { get; set; }

    [Parameter]
    public int productId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (productId > 0)
        {
            await _productService.DeleteProduct(productId);
            _navigationManager.NavigateTo("/Products");
        }
    }
}