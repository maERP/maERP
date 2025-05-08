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
    public int ProductId { get; set; }

    private string _title = "Produktdetail";

    private ProductDetailDto _productDetail = new();

    protected override async Task OnInitializedAsync()
    {
        if (ProductId == 0)
        {
            _title = "Produkt nicht gefunden";
        }
        else
        {
            _title = $"Produkt {ProductId}";

            var result = await HttpService.GetAsync<Result<ProductDetailDto>>($"/api/v1/Products/{ProductId}");

            if (result != null && result.Succeeded)
            {
                _title = $"Produkt {_productDetail.Name}";
                _productDetail = result.Data;
            }
        }
    }
}