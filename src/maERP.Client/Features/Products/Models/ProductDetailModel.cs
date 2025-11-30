using maERP.Client.Core.Exceptions;
using maERP.Client.Features.Products.Services;
using maERP.Domain.Dtos.Product;

namespace maERP.Client.Features.Products.Models;

/// <summary>
/// Model for product detail page using MVUX pattern.
/// Receives product ID from navigation data.
/// </summary>
public partial record ProductDetailModel
{
    private readonly IProductService _productService;
    private readonly INavigator _navigator;
    private readonly Guid _productId;

    public ProductDetailModel(
        IProductService productService,
        INavigator navigator,
        ProductDetailData data)
    {
        _productService = productService;
        _navigator = navigator;
        _productId = data.productId;
    }

    /// <summary>
    /// State for error messages from API operations.
    /// </summary>
    public IState<string> ErrorMessage => State<string>.Value(this, () => string.Empty);

    /// <summary>
    /// Feed that loads the product details.
    /// </summary>
    public IFeed<ProductDetailDto> Product => Feed.Async(async ct =>
    {
        var product = await _productService.GetProductAsync(_productId, ct);
        return product ?? throw new InvalidOperationException($"Product {_productId} not found");
    });

    /// <summary>
    /// Navigate to edit product page.
    /// </summary>
    public async Task EditProduct()
    {
        // TODO: Navigate to ProductEditPage when implemented
        // await _navigator.NavigateDataAsync(this, new ProductEditData(_productId));
    }

    /// <summary>
    /// Clear the error message.
    /// </summary>
    public async Task ClearError(CancellationToken ct)
    {
        await ErrorMessage.Set(string.Empty, ct);
    }

    /// <summary>
    /// Navigate back to product list.
    /// </summary>
    public async Task GoBack()
    {
        await _navigator.NavigateBackAsync(this);
    }
}
