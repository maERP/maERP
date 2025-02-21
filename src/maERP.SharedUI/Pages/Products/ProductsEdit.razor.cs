using maERP.Domain.Dtos.Product;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Products;

public partial class ProductsEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Parameter]
    public int productId { get; set; }

    protected MudForm? Form;

    protected bool ProductAiHelperVisible;

    protected ProductDetailDto Product = new();

    protected override async Task OnParametersSetAsync()
    {
        if (productId != 0)
        {
            Product = await HttpService.GetAsync<ProductDetailDto>($"/api/v1/Products/{productId}") ?? new ProductDetailDto();
        }
    }

    protected async Task Save()
    {
        if (productId != 0)
        {
            await HttpService.PutAsJsonAsync<ProductDetailDto>($"/api/v1/Products/{productId}" + productId, Product);
        }
        else
        {
            await HttpService.PostAsJsonAsync<ProductDetailDto>("/api/v1/Products", Product);
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