using maERP.SharedUI.Models.Product;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Products;

public partial class ProductsDetail
{

    [Parameter]
    public int productId { get; set; }

    protected string Title = "Produktdetail";

    protected ProductVM product = new();

    protected override async Task OnParametersSetAsync()
    {
        if (productId != 0)
        {
            product = await _productService.GetProductDetails(productId);
        }
    }
}