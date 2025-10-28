using maERP.Application.Features.Product.Commands.ProductCreate;
using maERP.Application.Features.Product.Commands.ProductUpdate;
using maERP.Domain.Dtos.Product;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// Implementation of products API client
/// </summary>
public class ProductsApiClient : ApiClientBase, IProductsApiClient
{
    private const string BaseEndpoint = "api/v1/Products";

    public ProductsApiClient(HttpClient httpClient, ILogger<ProductsApiClient> logger)
        : base(httpClient, logger)
    {
    }

    public async Task<PaginatedResult<ProductListDto>?> GetAllAsync(
        int pageNumber = 0,
        int pageSize = 10,
        string searchString = "",
        string orderBy = "",
        CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string?>
        {
            { "pageNumber", pageNumber.ToString() },
            { "pageSize", pageSize.ToString() },
            { "searchString", searchString },
            { "orderBy", orderBy }
        };

        var url = BuildUrl(BaseEndpoint, queryParams);
        return await GetAsync<PaginatedResult<ProductListDto>>(url, cancellationToken);
    }

    public async Task<ProductDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetAsync<ProductDetailDto>($"{BaseEndpoint}/{id}", cancellationToken);
    }

    public async Task<ProductDetailDto?> CreateAsync(
        ProductCreateCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PostAsync<ProductCreateCommand, ProductDetailDto>(
            BaseEndpoint,
            command,
            cancellationToken);
    }

    public async Task<HttpResponseMessage> UpdateAsync(
        Guid id,
        ProductUpdateCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PutAsync($"{BaseEndpoint}/{id}", command, cancellationToken);
    }

    public async Task<HttpResponseMessage> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DeleteAsync($"{BaseEndpoint}/{id}", cancellationToken);
    }
}
