using maERP.Domain.Dtos.Product;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Products;

public partial class ProductsDetail
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }
    
    [Parameter]
    public int productId { get; set; }

    protected string Title = "Produktdetail";

    protected ProductDetailDto Product = new();

    protected override async Task OnInitializedAsync()
    {
        if (productId == 0)
        {
            Title = "Produkt nicht gefunden";
        }
        else
        {
            Title = $"Produkt {productId}";

            var result = await HttpService.GetAsync<Result<ProductDetailDto>>($"/api/v1/Products/{productId}");

            if (result != null && result.Succeeded)
            {
                Product = result.Data;
            }
        }
    }
}