using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Products;

public partial class ProductsDelete
{

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