using System.Net;
using System.Net.Http.Json;
using maERP.Application.Features.AIPrompt.Commands.AIPromptCreate;
using maERP.Application.Features.AIPrompt.Commands.AIPromptUpdate;
using maERP.Application.Features.AIPrompt.Queries.AIPromptDetail;
using maERP.Application.Features.AIPrompt.Queries.AIPromptList;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;

namespace maERP.Server.Tests;

[Collection("Sequential")]
public class AIPromptCrudTest : IClassFixture<maERPWebApplicationFactory<Program>>
{
    private readonly maERPWebApplicationFactory<Program> _webApplicationFactory;

    public AIPromptCrudTest(maERPWebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Theory]
    [InlineData("/api/v1/AIPrompts")]
    public async Task Create(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests();
        var aiPrompt = new AIPromptCreateCommand
        {
            Identifier = "prompt_test_1",
            PromptText = "Prompt Text 1"
        };

        HttpResponseMessage result = await httpClient.PostAsJsonAsync(url, aiPrompt);
        AIPromptDetailResponse? resultContent = await result.Content.ReadFromJsonAsync<AIPromptDetailResponse>();

        Assert.NotNull(resultContent);
        Assert.True(result.IsSuccessStatusCode);
        Assert.True(resultContent != null && resultContent.Id != default);
    }

    [Theory]
    [InlineData("/api/v1/AIPrompts/")]
    public async Task GetAll(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<AIPrompt> {
                new() {
                    Id = 2,
                    AiModelType = 0,
                    Identifier = "prompt_test_2",
                    PromptText = "Prompt Text 2"
                }
        });

        var result = await httpClient.GetFromJsonAsync<PaginatedResult<AIPromptListResponse>>(url);

        Assert.NotNull(result);
        Assert.Equal(1, result.TotalCount);
    }

    [Theory]
    [InlineData("/api/v1/AIPrompts/3")]
    public async Task GetDetail(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<AIPrompt> {
                new() {
                    Id = 3,
                    Identifier = "prompt_test_3",
                    PromptText = "Prompt Text 3"
                }
        });

        AIPromptDetailResponse? result = await httpClient.GetFromJsonAsync<AIPromptDetailResponse>(url);

        Assert.NotNull(result);
        Assert.True(result.PromptText.Length > 0);
    }

    [Theory]
    [InlineData("/api/v1/AIPrompts/4")]
    public async Task Update(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests(
            new List<AIPrompt> {
                new() {
                    Id = 4,
                    Identifier = "prompt_test_4"
                }
        });

        var aiPrompt = new AIPromptUpdateCommand
        {
            Identifier = "prompt_test_4_updated",
        };

        HttpResponseMessage result = await httpClient.PutAsJsonAsync(url, aiPrompt);

        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Theory]
    [InlineData("/api/v1/AIPrompts/5")]
    public async Task Delete(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<AIPrompt> {
                new() {
                    Id = 5,
                    Identifier = "prompt_test_5"
                }
        });

        HttpResponseMessage result = await httpClient.DeleteAsync(url);

        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Theory]
    [InlineData("/api/v1/AIPrompts/999999")]
    public async Task NotExist(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests();

        HttpResponseMessage result = await httpClient.GetAsync(url);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}