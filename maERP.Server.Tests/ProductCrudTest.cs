using System.Net;
using System.Net.Http.Json;
using maERP.Application.Features.Product.Commands.ProductCreate;
using maERP.Application.Features.Product.Queries.ProductDetail;
using maERP.Application.Features.Product.Queries.ProductList;
using maERP.Application.Features.Product.Commands.ProductUpdate;
using maERP.Domain.Entities;
using maERP.Shared.Wrapper;

namespace maERP.Server.Tests;

[Collection("Sequential")]
public class ProductCrudTest : IClassFixture<maERPWebApplicationFactory<Program>>
{
    private readonly maERPWebApplicationFactory<Program> _webApplicationFactory;

    public ProductCrudTest(maERPWebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Theory]
    [InlineData("/api/v1/Products")]
    public async Task Create(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests();
        var product = new ProductCreateCommand
        {
            Sku = "SKU-001",
            Name = "Product 1",
            Ean = "1234567890123",
            Asin = "12345",
            Price = 10,
            Msrp = 20
        };

        HttpResponseMessage result = await httpClient.PostAsJsonAsync(url, product);
        ProductDetailResponse? resultContent = await result.Content.ReadFromJsonAsync<ProductDetailResponse>();

        Assert.NotNull(resultContent);
        Assert.True(result.IsSuccessStatusCode);
        Assert.True(resultContent != null && resultContent.Id != default);
    }

    [Theory]
    [InlineData("/api/v1/Products/")]
    public async Task GetAll(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<Product> {
                new() {
                    Id = 2,
                    Name = "Product 2"
                }
        });

        var result = await httpClient.GetFromJsonAsync<PaginatedResult<ProductListResponse>>(url);

        Assert.NotNull(result);
        Assert.Equal(1, result.TotalCount);
    }

    [Theory]
    [InlineData("/api/v1/Products/3")]
    public async Task GetDetail(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<Product> {
                new() {
                    Id = 3,
                    Name = "Product 3"
                }
        });

        ProductDetailResponse? result = await httpClient.GetFromJsonAsync<ProductDetailResponse>(url);

        Assert.NotNull(result);
        Assert.True(result.Name.Length > 0);
    }

    [Theory]
    [InlineData("/api/v1/Products/4")]
    public async Task Update(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests(
            new List<Product> {
                new() {
                    Id = 4,
                    Name = "Product 4"
                }
        });

        var product = new ProductUpdateCommand
        {
            Name = "Product 3 updated",
        };

        HttpResponseMessage result = await httpClient.PutAsJsonAsync(url, product);

        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Theory]
    [InlineData("/api/v1/Products/5")]
    public async Task Delete(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<Product> {
                new() {
                    Id = 5,
                    Sku = "SKU-005",
                    Name = "Product 5"
                }
        });

        HttpResponseMessage result = await httpClient.DeleteAsync(url);

        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Theory]
    [InlineData("/api/v1/Products/999999")]
    public async Task NotExist(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests();

        HttpResponseMessage result = await httpClient.GetAsync(url);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}