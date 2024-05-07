using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Product;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Products;

public partial class Products
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IProductService _productService { get; set; }

    private ICollection<ProductVM>? products;
    public string? filter { get; set; }

    protected override async Task OnInitializedAsync()
    {
        products = await _productService.GetProducts();
    }

    public bool IsVisible(ProductVM product)
    {
        if (string.IsNullOrEmpty(filter))
            return true;

        if (product.Sku.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
            product.Name.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
            product.Ean.Contains(filter, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }
}