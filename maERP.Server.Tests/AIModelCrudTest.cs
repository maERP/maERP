using System.Net;
using System.Net.Http.Json;
using maERP.Application.Features.AIModel.Commands.AIModelCreate;
using maERP.Application.Features.AIModel.Commands.AIModelUpdate;
using maERP.Application.Features.AIModel.Queries.AIModelDetail;
using maERP.Application.Features.AIModel.Queries.AIModelList;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;

namespace maERP.Server.Tests;

[Collection("Sequential")]
public class AIModelCrudTest : IClassFixture<maERPWebApplicationFactory<Program>>
{
    private readonly maERPWebApplicationFactory<Program> _webApplicationFactory;

    public AIModelCrudTest(maERPWebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Theory]
    [InlineData("/api/v1/AIModels")]
    public async Task Create(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests();
        var aiModel = new AIModelCreateCommand
        {
            AIType = 0,
            Name = "AIModel 1"
        };

        HttpResponseMessage result = await httpClient.PostAsJsonAsync(url, aiModel);
        AIModelDetailResponse? resultContent = await result.Content.ReadFromJsonAsync<AIModelDetailResponse>();

        Assert.NotNull(resultContent);
        Assert.True(result.IsSuccessStatusCode);
        Assert.True(resultContent != null && resultContent.Id != default);
    }

    [Theory]
    [InlineData("/api/v1/AIModels/")]
    public async Task GetAll(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<AIModel> {
                new() {
                    Id = 2,
                    AiModelType = 0,
                    Name = "AIModel 2"
                }
        });

        var result = await httpClient.GetFromJsonAsync<PaginatedResult<AIModelListResponse>>(url);

        Assert.NotNull(result);
        Assert.Equal(1, result.TotalCount);
    }

    [Theory]
    [InlineData("/api/v1/AIModels/3")]
    public async Task GetDetail(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<AIModel> {
                new() {
                    Id = 3,
                    Name = "AIModel 3"
                }
        });

        AIModelDetailResponse? result = await httpClient.GetFromJsonAsync<AIModelDetailResponse>(url);

        Assert.NotNull(result);
        Assert.True(result.Name.Length > 0);
    }

    [Theory]
    [InlineData("/api/v1/AIModels/4")]
    public async Task Update(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests(
            new List<AIModel> {
                new() {
                    Id = 4,
                    Name = "AIModel 4"
                }
        });

        var aiModel = new AIModelUpdateCommand
        {
            Name = "AIModel 3 updated",
        };

        HttpResponseMessage result = await httpClient.PutAsJsonAsync(url, aiModel);

        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Theory]
    [InlineData("/api/v1/AIModels/5")]
    public async Task Delete(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<AIModel> {
                new() {
                    Id = 5,
                    Name = "AIModel 5"
                }
        });

        HttpResponseMessage result = await httpClient.DeleteAsync(url);

        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Theory]
    [InlineData("/api/v1/AIModels/999999")]
    public async Task NotExist(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests();

        HttpResponseMessage result = await httpClient.GetAsync(url);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}