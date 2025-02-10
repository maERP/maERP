using System.Net;
using System.Net.Http.Json;
using maERP.Application.Features.SalesChannel.Commands.SalesChannelCreate;
using maERP.Application.Features.SalesChannel.Queries.SalesChannelDetail;
using maERP.Application.Features.SalesChannel.Queries.SalesChannelList;
using maERP.Application.Features.SalesChannel.Commands.SalesChannelUpdate;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using SalesChannelType = maERP.Domain.Enums.SalesChannelType;

namespace maERP.Server.Tests;

[Collection("Sequential")]
public class SalesChannelCrudTest : IClassFixture<MaErpWebApplicationFactory<Program>>
{
    private readonly MaErpWebApplicationFactory<Program> _webApplicationFactory;

    public SalesChannelCrudTest(MaErpWebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Theory]
    [InlineData("/api/v1/SalesChannels")]
    public async Task Create(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests();
        var saleschannel = new SalesChannelCreateCommand
        {
            Type = 1,
            Name = "SalesChannel 2",
            Url = string.Empty,
            Username = string.Empty,
            Password = string.Empty
        };

        HttpResponseMessage result = await httpClient.PostAsJsonAsync(url, saleschannel);
        SalesChannelDetailDto? resultContent = await result.Content.ReadFromJsonAsync<SalesChannelDetailDto>();

        Assert.NotNull(resultContent);
        Assert.True(result.IsSuccessStatusCode);
        Assert.True(resultContent != null && resultContent.Id != default);
    }

    [Theory]
    [InlineData("/api/v1/SalesChannels/")]
    public async Task GetAll(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<SalesChannel> {
                new() {
                    Id = 3,
                    Name = "SalesChannel 3"
                }
        });

        var result = await httpClient.GetFromJsonAsync<PaginatedResult<SalesChannelListDto>>(url);

        Assert.NotNull(result);
        Assert.Equal(2, result.TotalCount);
    }

    [Theory]
    [InlineData("/api/v1/SalesChannels/4")]
    public async Task GetDetail(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<SalesChannel> {
                new() {
                    Id = 4,
                    Name = "SalesChannel 4",
                    WarehouseId = 1
                }
        });

        SalesChannelDetailDto? result = await httpClient.GetFromJsonAsync<SalesChannelDetailDto>(url);

        Assert.NotNull(result);
        Assert.True(result.Name.Length > 0);
    }

    [Theory]
    [InlineData("/api/v1/SalesChannels/5")]
    public async Task Update(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests(
            new List<SalesChannel> {
                new() {
                    Id = 5,
                    Type = SalesChannelType.PointOfSale,
                    Name = "SalesChannel 5",
                    Url = string.Empty,
                    Username = string.Empty,
                    Password = string.Empty,
                    ImportProducts = false,
                    ImportCustomers = false,
                    ImportOrders = false,
                    ExportProducts = false,
                    ExportCustomers = false,
                    ExportOrders = false,
                    WarehouseId = 1
                }
        });

        var saleschannel = new SalesChannelUpdateCommand
        {
            // TODO: fix this
            //Type = SalesChannelType.PointOfSale,
            Name = "SalesChannel 5 updated",
            Username = string.Empty,
            Password = string.Empty,
            ImportProducts = false,
            ImportCustomers = false,
            ImportOrders = false,
            ExportProducts = false,
            ExportCustomers = false,
            ExportOrders = false,
            WarehouseId = 1
        };

        HttpResponseMessage result = await httpClient.PutAsJsonAsync(url, saleschannel);
        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Theory]
    [InlineData("/api/v1/SalesChannels/5")]
    public async Task Delete(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<SalesChannel> {
                new() {
                    Id = 5,
                    Name = "SalesChannel 5"
                }
        });

        HttpResponseMessage result = await httpClient.DeleteAsync(url);

        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Theory]
    [InlineData("/api/v1/SalesChannels/999999")]
    public async Task NotExist(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests();

        HttpResponseMessage result = await httpClient.GetAsync(url);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}