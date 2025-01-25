using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Product;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Products;

public partial class ProductsDetail
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IProductService ProductService { get; set; }

    [Parameter]
    public int productId { get; set; }

    protected string Title = "Produktdetail";

    protected ProductVm Product = new();

    protected override async Task OnParametersSetAsync()
    {
        if (productId != 0)
        {
            Product = await ProductService.GetProductDetails(productId);
        }
    }
}