using System.Net;
using System.Net.Http.Json;
using maERP.Application.Features.Product.Commands.ProductCreate;
using maERP.Application.Features.Product.Commands.ProductUpdate;
using maERP.Domain.Dtos.Product;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;

namespace maERP.Server.Tests;

[Collection("Sequential")]
public class ProductCrudTest : IClassFixture<MaErpWebApplicationFactory<Program>>
{
    private readonly MaErpWebApplicationFactory<Program> _webApplicationFactory;

    public ProductCrudTest(MaErpWebApplicationFactory<Program> webApplicationFactory)
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

        var httpResponseMessage = await httpClient.PostAsJsonAsync(url, product);
        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>();
        
        Assert.True(httpResponseMessage.IsSuccessStatusCode);
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
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

        var result = await httpClient.GetFromJsonAsync<PaginatedResult<ProductListDto>>(url);

        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.Equal(ResultStatusCode.Ok, result.StatusCode);
        Assert.Equal(1, result.TotalCount);
        Assert.NotNull(result.Data);
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

        var response = await httpClient.GetAsync(url);
        var result = await response.Content.ReadFromJsonAsync<Result<ProductDetailDto>>();

        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.Equal(ResultStatusCode.Ok, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.True(result.Data.Name.Length > 0);
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
            Name = "Product 4 updated",
        };

        var httpResponseMessage = await httpClient.PutAsJsonAsync(url, product);
        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>();
        
        Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.Equal(ResultStatusCode.Ok, result.StatusCode);
        Assert.IsType<int>(result.Data);
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

        var response = await httpClient.DeleteAsync(url);
        
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Theory]
    [InlineData("/api/v1/Products/999999")]
    public async Task NotExist(string url)
    {
        var httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests();

        var response = await httpClient.GetAsync(url);
        var result = await response.Content.ReadFromJsonAsync<Result>();
        
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.NotNull(result);
        Assert.False(result.Succeeded);
        Assert.Equal(ResultStatusCode.NotFound, result.StatusCode);
        Assert.NotEmpty(result.Messages);
    }
}