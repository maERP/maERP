using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Product;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Products;

public partial class ProductsEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IProductService ProductService { get; set; }

    [Parameter]
    public int productId { get; set; }

    protected MudForm? Form;

    protected bool ProductAiHelperVisible;

    protected ProductVm Product = new();

    protected override async Task OnParametersSetAsync()
    {
        if (productId != 0)
        {
            Product = await ProductService.GetProductDetails(productId);
        }
    }

    protected async Task Save()
    {
        if (productId != 0)
        {
            await ProductService.UpdateProduct(productId, Product);
        }
        else
        {
            await ProductService.CreateProduct(Product);
        }

        Cancel();
    }

    public void Cancel()
    {
        NavigationManager.NavigateTo("/Products");
    }

    public void OpenProductAiHelper()
    {
        ProductAiHelperVisible = true;
    }
}