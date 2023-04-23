using System.Net;
using System.Net.Http.Json;
using maERP.Shared.Dtos.Product;
using maERP.Shared.Models;

namespace maERP.Server.Tests;

public class ProductCrudTest : IClassFixture<maERPWebApplicationFactory<Program>>
{
    private readonly maERPWebApplicationFactory<Program> _webApplicationFactory;

    public ProductCrudTest(maERPWebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Theory]
    [InlineData("/api/Product")]
    public async Task Create(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests();
        ProductCreateDto product = new ProductCreateDto
        {
            Name = "Testprodukt 1 created"
        };

        HttpResponseMessage result = await httpClient.PostAsJsonAsync(url, product);
        ProductDetailDto? resultContent = await result.Content.ReadFromJsonAsync<ProductDetailDto>();

        Assert.NotNull(resultContent);
        Assert.True(result.IsSuccessStatusCode);
        Assert.True(resultContent != null && resultContent.Id != default);
    }

    [Theory]
    [InlineData("/api/Product/GetAll")]
    public async Task GetAll(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<Product> {
                new() {
                    Id = 1, Name = "Testprodukt 1"
                }
        });

        List<ProductListDto>? result = await httpClient.GetFromJsonAsync<List<ProductListDto>>(url);
        
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Theory]
    [InlineData("/api/Product/1")]
    public async Task GetDetail(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<Product> {
                new() {
                    Id = 1, Name = "Testprodukt 1"
                }
        });

        ProductDetailDto? result = await httpClient.GetFromJsonAsync<ProductDetailDto>(url);

        Assert.NotNull(result);
        Assert.NotNull(result);
        Assert.True(result != null && result.Name.Length > 0);
    }

    [Theory]
    [InlineData("/api/Product/999999")]
    public async Task NotExist(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests();

        HttpResponseMessage result = await httpClient.GetAsync(url);

        Assert.True(result.StatusCode == HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData("/api/Product/1")]
    public async Task Update(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests(
            new List<Product> {
                new() {
                    Id = 1,
                    Name = "Testprodukt 1"
                }
        });

        ProductUpdateDto product = new ProductUpdateDto
        {
            Name = "Testprodukt 1 updated",
        };

        HttpResponseMessage result = await httpClient.PutAsJsonAsync(url, product);
        ProductDetailDto? resultContent = await result.Content.ReadFromJsonAsync<ProductDetailDto>();

        Assert.NotNull(resultContent);
        Assert.True(result.IsSuccessStatusCode);
        Assert.True(resultContent != null && resultContent.Name == product.Name);
    }

    [Theory]
    [InlineData("/api/Product/1")]
    public async Task Delete(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<Product> {
                new() {
                    Id = 1,
                    Name = "Testprodukt 1"
                }
        });

        HttpResponseMessage result = await httpClient.DeleteAsync(url);

        Assert.True(result.IsSuccessStatusCode);
    }
}