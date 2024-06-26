using System.Net;
using System.Net.Http.Json;
using maERP.Application.Features.TaxClass.Commands.TaxClassCreate;
using maERP.Application.Features.TaxClass.Commands.TaxClassUpdate;
using maERP.Application.Features.TaxClass.Queries.TaxClassDetail;
using maERP.Application.Features.TaxClass.Queries.TaxClassList;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;

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
        var taxclass = new TaxClassCreateCommand
        {
            TaxRate = 20
        };

        HttpResponseMessage result = await httpClient.PostAsJsonAsync(url, taxclass);
        TaxClassDetailResponse? resultContent = await result.Content.ReadFromJsonAsync<TaxClassDetailResponse>();

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
                    Id = 4,
                    TaxRate = 21
                }
        });

        var result = await httpClient.GetFromJsonAsync<PaginatedResult<TaxClassListResponse>>(url);

        Assert.NotNull(result);
        Assert.Equal(4, result.TotalCount);
    }

    [Theory]
    [InlineData("/api/v1/TaxClasses/1")]
    public async Task GetDetail(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests();

        TaxClassDetailResponse? result = await httpClient.GetFromJsonAsync<TaxClassDetailResponse>(url);

        Assert.NotNull(result);
        Assert.Equal(19, result.TaxRate);
    }

    [Theory]
    [InlineData("/api/v1/TaxClasses/6")]
    public async Task Update(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests(
            new List<TaxClass> {
                new() {
                    Id = 6,
                    TaxRate = 23
                }
        });

        var taxclass = new TaxClassUpdateCommand
        {
            TaxRate = 24
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

        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Theory]
    [InlineData("/api/v1/TaxClasses/999999")]
    public async Task NotExist(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests();

        HttpResponseMessage result = await httpClient.GetAsync(url);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}