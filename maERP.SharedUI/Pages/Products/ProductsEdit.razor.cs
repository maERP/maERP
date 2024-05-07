using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Product;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Products;

public partial class ProductsEdit
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IProductService _productService { get; set; }

    [Parameter]
    public int productId { get; set; }

    protected MudForm? _form;

    protected ProductVM product = new();

    protected override async Task OnParametersSetAsync()
    {
        if (productId != 0)
        {
            product = await _productService.GetProductDetails(productId);
        }
    }

    protected async Task Save()
    {
        if (productId != 0)
        {
            await _productService.UpdateProduct(productId, product);
        }
        else
        {
            await _productService.CreateProduct(product);
        }

        Cancel();
    }

    public void Cancel()
    {
        _navigationManager.NavigateTo("/Products");
    }
}