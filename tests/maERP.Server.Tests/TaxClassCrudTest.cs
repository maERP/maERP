using System.Net;
using System.Net.Http.Json;
using maERP.Application.Features.TaxClass.Commands.TaxClassCreate;
using maERP.Application.Features.TaxClass.Commands.TaxClassUpdate;
using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;

namespace maERP.Server.Tests;

[Collection("Sequential")]
public class TaxClassCrudTest : IClassFixture<MaErpWebApplicationFactory<Program>>
{
    private readonly MaErpWebApplicationFactory<Program> _webApplicationFactory;

    public TaxClassCrudTest(MaErpWebApplicationFactory<Program> webApplicationFactory)
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

        var httpResponseMessage = await httpClient.PostAsJsonAsync(url, taxclass);
        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>();
        
        Assert.True(httpResponseMessage.IsSuccessStatusCode);
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
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

        var result = await httpClient.GetFromJsonAsync<PaginatedResult<TaxClassListDto>>(url);

        Assert.NotNull(result);
        Assert.Equal(4, result.TotalCount);
    }

    [Theory]
    [InlineData("/api/v1/TaxClasses/1")]
    public async Task GetDetail(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests();

        var result = await httpClient.GetFromJsonAsync<Result<TaxClassDetailDto>>(url);
        
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.Equal(19, result.Data.TaxRate);
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

        var httpResponseMessage = await httpClient.PutAsJsonAsync(url, taxclass);
        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>();
        
        Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.True(result.StatusCode == ResultStatusCode.Ok);
        Assert.IsType<int>(result.Data);
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