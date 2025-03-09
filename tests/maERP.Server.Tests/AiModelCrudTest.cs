using System.Net;
using System.Net.Http.Json;
using maERP.Application.Features.AiModel.Commands.AiModelCreate;
using maERP.Application.Features.AiModel.Commands.AiModelUpdate;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;

namespace maERP.Server.Tests;

[Collection("Sequential")]
public class AiModelCrudTest : IClassFixture<MaErpWebApplicationFactory<Program>>
{
    private readonly MaErpWebApplicationFactory<Program> _webApplicationFactory;

    public AiModelCrudTest(MaErpWebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Theory]
    [InlineData("/api/v1/AiModels")]
    public async Task Create(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests();
        var aiModel = new AiModelCreateCommand
        {
            AiModelType = AiModelType.None,
            Name = "AiModel 1",
            ApiKey = "1234567890",
            ApiUsername = "username",
            ApiPassword = "password"
        };

        var httpResponseMessage = await httpClient.PostAsJsonAsync(url, aiModel);
        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>();
        
        Assert.True(httpResponseMessage.IsSuccessStatusCode);
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
    }

    [Theory]
    [InlineData("/api/v1/AiModels/")]
    public async Task GetAll(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<AiModel> {
                new() {
                    Id = 2,
                    AiModelType = AiModelType.None,
                    Name = "AiModel 2",
                    ApiKey = "1234567890",
                }
        });

        var result = await httpClient.GetFromJsonAsync<PaginatedResult<AiModelListDto>>(url);

        Assert.NotNull(result);
        Assert.Equal(1, result.TotalCount);
    }

    [Theory]
    [InlineData("/api/v1/AiModels/3")]
    public async Task GetDetail(string url)
    {
        var httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<AiModel> {
                new() {
                    Id = 3,
                    Name = "AiModel 3",
                    AiModelType = AiModelType.Claude35,
                    ApiUsername = "AI Model Username",
                    ApiPassword = "AI Model Password"
                }
        });

        var result = await httpClient.GetFromJsonAsync<Result<AiModelDetailDto>>(url);
        
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.True(result.Data.Name.Length > 0);
    }

    [Theory]
    [InlineData("/api/v1/AiModels/4")]
    public async Task Update(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests(
            new List<AiModel> {
                new() {
                    Id = 4,
                    AiModelType = AiModelType.ChatGpt4O,
                    Name = "AiModel 4",
                    ApiKey = "1234567890",
                }
        });

        var aiModel = new AiModelUpdateCommand
        {
            Name = "AiModel 3 updated",
            ApiKey = "123456789111",
        };

        var httpResponseMessage = await httpClient.PutAsJsonAsync(url, aiModel);
        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>();

        Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.True(result.StatusCode == ResultStatusCode.Ok);
        Assert.IsType<int>(result.Data);
    }

    [Theory]
    [InlineData("/api/v1/AiModels/5")]
    public async Task Delete(string url)
    {
        var httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<AiModel> {
                new() {
                    Id = 5,
                    Name = "AiModel 5"
                }
        });

        var result = await httpClient.DeleteAsync(url);

        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Theory]
    [InlineData("/api/v1/AiModels/999999")]
    public async Task NotExist(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests();

        HttpResponseMessage result = await httpClient.GetAsync(url);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Theory]
    [InlineData("/api/v1/AiModels")]
    public async Task Create_InvalidName_ShouldReturnBadRequest(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests();
        var aiModel = new AiModelCreateCommand
        {
            AiModelType = AiModelType.None,
            Name = "", // Invalid name (empty)
            ApiKey = "1234567890",
            ApiUsername = "username",
            ApiPassword = "password"
        };

        var httpResponseMessage = await httpClient.PostAsJsonAsync(url, aiModel);

        Assert.Equal(HttpStatusCode.BadRequest, httpResponseMessage.StatusCode);
    }

    [Theory]
    [InlineData("/api/v1/AiModels")]
    public async Task Create_InvalidApiKey_ShouldReturnBadRequest(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests();
        var aiModel = new AiModelCreateCommand
        {
            AiModelType = AiModelType.None,
            Name = "AiModel 1",
            ApiKey = "123", // Invalid API key (too short)
            ApiUsername = "username",
            ApiPassword = "password"
        };

        var httpResponseMessage = await httpClient.PostAsJsonAsync(url, aiModel);

        Assert.Equal(HttpStatusCode.BadRequest, httpResponseMessage.StatusCode);
    }

    [Theory]
    [InlineData("/api/v1/AiModels")]
    public async Task Create_MissingApiCredentials_ShouldReturnBadRequest(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests();
        var aiModel = new AiModelCreateCommand
        {
            AiModelType = AiModelType.None,
            Name = "AiModel 1",
            ApiKey = "", // Missing API key
            ApiUsername = "", // Missing username
            ApiPassword = "" // Missing password
        };

        var httpResponseMessage = await httpClient.PostAsJsonAsync(url, aiModel);

        Assert.Equal(HttpStatusCode.BadRequest, httpResponseMessage.StatusCode);
    }

    [Theory]
    [InlineData("/api/v1/AiModels")]
    public async Task Create_ValidModel_ShouldReturnSuccess(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests();
        var aiModel = new AiModelCreateCommand
        {
            AiModelType = AiModelType.None,
            Name = "Valid AiModel",
            ApiKey = "1234567890",
            ApiUsername = "username",
            ApiPassword = "password"
        };

        var httpResponseMessage = await httpClient.PostAsJsonAsync(url, aiModel);
        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>();

        Assert.True(httpResponseMessage.IsSuccessStatusCode);
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
    }

}