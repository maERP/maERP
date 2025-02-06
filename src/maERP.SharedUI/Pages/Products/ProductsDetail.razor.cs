using maERP.Domain.Dtos.Product;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Services;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Products;

public partial class ProductsDetail
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required HttpService HttpService { get; set; }
    
    [Parameter]
    public int productId { get; set; }

    protected string Title = "Produktdetail";

    protected ProductDetailDto Product = new();

    protected override async Task OnParametersSetAsync()
    {
        if (productId != 0)
        {
            Product = await HttpService.GetAsync<ProductDetailDto>("/api/v1/Products/" + productId) ?? new ProductDetailDto();
        }
    }
}