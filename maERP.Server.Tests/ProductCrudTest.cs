namespace maERP.Server.Tests;

/*
public class ProductCrudTest : IClassFixture<maERPWebApplicationFactory<Program>>
{
    private readonly maERPWebApplicationFactory<Program> _webApplicationFactory;

    public ProductCrudTest(maERPWebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Theory]
    [InlineData("/api/Products")]
    public async Task Create(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests();
        var product = new ProductCreateDto
        {
            Name = "Testprodukt 1 created",
            Sku = "1001",
            Price = 100,
            TaxClass = new Shared.Dtos.BaseDto { Id = 1 }
        };

        HttpResponseMessage result = await httpClient.PostAsJsonAsync(url, product);
        ProductDetailDto? resultContent = await result.Content.ReadFromJsonAsync<ProductDetailDto>();

        Assert.NotNull(resultContent);
        Assert.True(result.IsSuccessStatusCode);
        Assert.True(resultContent != null && resultContent.Id != default);
    }

    [Theory]
    [InlineData("/api/Products/GetAll")]
    public async Task GetAll(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<Product> {
                new() {
                    Id = 2,
                    Name = "Testprodukt 2",
                    Sku = "1002",
                    Price = 100,
                    TaxClass = new TaxClass { Id = 1, TaxRate = 19 }
                }
        });

        ICollection<ProductListDto>? result = await httpClient.GetFromJsonAsync<ICollection<ProductListDto>>(url);

        Assert.NotNull(result);
        Assert.Equal(result?.Count, 1);
    }

    [Theory]
    [InlineData("/api/Products/3")]
    public async Task GetDetail(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<Product> {
                new() {
                    Id = 3,
                    Name = "Testprodukt 3",
                    Sku = "1003",
                    Price = 100
                }
        });

        ProductDetailDto? result = await httpClient.GetFromJsonAsync<ProductDetailDto>(url);

        Assert.NotNull(result);
        Assert.True(result.Name.Length > 0);
    }

    [Theory]
    [InlineData("/api/Products/4")]
    public async Task Update(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests(
            new List<Product> {
                new() {
                    Id = 4,
                    Name = "Testprodukt 4",
                    Sku = "1004",
                    Price = 100
                }
        });

        var product = new ProductUpdateDto
        {
            Name = "Testprodukt 3 updated",
        };

        HttpResponseMessage result = await httpClient.PutAsJsonAsync(url, product);
        ProductDetailDto? resultContent = await result.Content.ReadFromJsonAsync<ProductDetailDto>();

        Assert.NotNull(resultContent);
        Assert.True(result.IsSuccessStatusCode);
        Assert.True(resultContent != null && resultContent.Name == product.Name);
    }

    [Theory]
    [InlineData("/api/Products/5")]
    public async Task Delete(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<Product> {
                new() {
                    Id = 5,
                    Name = "Testprodukt 5",
                    Sku = "1005",
                    Price = 100
                }
        });

        HttpResponseMessage result = await httpClient.DeleteAsync(url);

        Assert.Equal(result?.StatusCode, HttpStatusCode.NoContent);
    }

    [Theory]
    [InlineData("/api/Products/999999")]
    public async Task NotExist(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests();

        HttpResponseMessage result = await httpClient.GetAsync(url);

        Assert.Equal(result?.StatusCode, HttpStatusCode.NotFound);
    }    
}
*/