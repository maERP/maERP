using System.Net;
using System.Net.Http.Json;
using maERP.Application.Features.Setting.Commands.SettingCreate;
using maERP.Application.Features.Setting.Commands.SettingUpdate;
using maERP.Domain.Dtos.Setting;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;

namespace maERP.Server.Tests;

[Collection("Sequential")]
public class SettingsCrudTest : IClassFixture<MaErpWebApplicationFactory<Program>>
{
    private readonly MaErpWebApplicationFactory<Program> _webApplicationFactory;

    public SettingsCrudTest(MaErpWebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Theory]
    [InlineData("/api/v1/Settings")]
    public async Task Create(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests();
        var setting = new SettingCreateCommand
        {
            Id = 1111,
            Key = "TestKey1",
            Value = "TestValue1"
        };

        var httpResponseMessage = await httpClient.PostAsJsonAsync(url, setting);
        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>();

        Assert.True(httpResponseMessage.IsSuccessStatusCode);
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
    }

    [Theory]
    [InlineData("/api/v1/Settings/")]
    public async Task GetAll(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<Setting> {
                new() {
                    Id = 2222,
                    Key = "TestKey2",
                    Value = "TestValue2"
                }
        });

        var result = await httpClient.GetFromJsonAsync<PaginatedResult<SettingListDto>>(url);

        Assert.NotNull(result);
        Assert.InRange(result.TotalCount, 1, 9999);
    }

    [Theory]
    [InlineData("/api/v1/Settings/3333")]
    public async Task GetDetail(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<Setting> {
                new() {
                    Id = 3333,
                    Key = "TestKey3",
                    Value = "TestValue3"
                }
        });

        var result = await httpClient.GetFromJsonAsync<Result<SettingDetailDto>>(url);

        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.True(result.Data.Key.Length > 0);
        Assert.True(result.Data.Value.Length > 0);
    }

    [Theory]
    [InlineData("/api/v1/Settings/4444")]
    public async Task Update(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests(
            new List<Setting> {
                new() {
                    Id = 4444,
                    Key = "TestKey4",
                    Value = "TestValue4"
                }
        });

        var setting = new SettingUpdateCommand
        {
            Key = "TestKey4Updated",
            Value = "TestValue4Updated"
        };

        var httpResponseMessage = await httpClient.PutAsJsonAsync(url, setting);
        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>();

        Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.True(result.StatusCode == ResultStatusCode.Ok);
        Assert.IsType<int>(result.Data);
    }

    [Theory]
    [InlineData("/api/v1/Settings/5555")]
    public async Task Delete(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<Setting> {
                new() {
                    Id = 5555,
                    Key = "TestKey5",
                    Value = "TestValue5"
                }
        });

        HttpResponseMessage result = await httpClient.DeleteAsync(url);

        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Theory]
    [InlineData("/api/v1/Settings/999999")]
    public async Task NotExist(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests();

        HttpResponseMessage result = await httpClient.GetAsync(url);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}
