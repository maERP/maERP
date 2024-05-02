using maERP.Application.Dtos.TaxClass;
using maERP.Domain.Models;
using System.Net;
using System.Net.Http.Json;

namespace maERP.Server.Tests;

[Collection("Sequential")]
public class TaxClassCrudTest : IClassFixture<maERPWebApplicationFactory<Program>>
{
    private readonly maERPWebApplicationFactory<Program> _webApplicationFactory;

    public TaxClassCrudTest(maERPWebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Theory]
    [InlineData("/api/v1/TaxClasses")]
    public async Task Create(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests();
        var taxclass = new TaxClassCreateDto
        {
            TaxRate = 19
        };

        HttpResponseMessage result = await httpClient.PostAsJsonAsync(url, taxclass);
        TaxClassDetailDto? resultContent = await result.Content.ReadFromJsonAsync<TaxClassDetailDto>();

        Assert.NotNull(resultContent);
        Assert.True(result.IsSuccessStatusCode);
        Assert.True(resultContent != null && resultContent.Id != default);
    }

    [Theory]
    [InlineData("/api/v1/TaxClasses/")]
    public async Task GetAll(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<TaxClass> {
                new() {
                    Id = 2,
                    TaxRate = 19
                }
        });

        ICollection<TaxClassListDto>? result = await httpClient.GetFromJsonAsync<ICollection<TaxClassListDto>>(url);

        Assert.NotNull(result);
        Assert.Equal(result?.Count, 1);
    }

    [Theory]
    [InlineData("/api/v1/TaxClasses/3")]
    public async Task GetDetail(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<TaxClass> {
                new() {
                    Id = 3,
                    TaxRate = 19
                }
        });

        TaxClassDetailDto? result = await httpClient.GetFromJsonAsync<TaxClassDetailDto>(url);

        Assert.NotNull(result);
        Assert.Equal(19, result.TaxRate);
    }

    [Theory]
    [InlineData("/api/v1/TaxClasses/4")]
    public async Task Update(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests(
            new List<TaxClass> {
                new() {
                    Id = 4,
                    TaxRate = 19
                }
        });

        var taxclass = new TaxClassUpdateDto
        {
            TaxRate = 20
        };

        HttpResponseMessage result = await httpClient.PutAsJsonAsync(url, taxclass);

        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Theory]
    [InlineData("/api/v1/TaxClasses/5")]
    public async Task Delete(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<TaxClass> {
                new() {
                    Id = 5,
                    TaxRate = 19
                }
        });

        HttpResponseMessage result = await httpClient.DeleteAsync(url);

        Assert.Equal(HttpStatusCode.NoContent, result?.StatusCode);
    }

    [Theory]
    [InlineData("/api/v1/TaxClasses/999999")]
    public async Task NotExist(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests();

        HttpResponseMessage result = await httpClient.GetAsync(url);

        Assert.Equal(result?.StatusCode, HttpStatusCode.NotFound);
    }
}