namespace maERP.Client.Features.Products.Models;

public partial record ProductListModel
{
    private readonly IProductsApiClient _productsApiClient;

    public ProductListModel(IProductsApiClient productsApiClient)
    {
        _productsApiClient = productsApiClient;
    }
}
