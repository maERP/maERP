using System.Net;
using System.Net.Http.Json;
using maERP.Application.Features.SalesChannel.Commands.SalesChannelCreate;
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
            SalesChannelType = SalesChannelType.Shopware5,
            Name = "SalesChannel 2",
            Url = string.Empty,
            Username = string.Empty,
            Password = string.Empty
        };

        var httpResponseMessage = await httpClient.PostAsJsonAsync(url, saleschannel);
        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>();
        
        Assert.True(httpResponseMessage.IsSuccessStatusCode);
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
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
                    Warehouses = new List<Warehouse> {
                        new() {
                            Id = 3,
                            Name = "Warehouse 3"
                        },
                        new() {
                            Id = 4,
                            Name = "Warehouse 4"
                        }
                    }
                }
        });

        var result = await httpClient.GetFromJsonAsync<Result<SalesChannelDetailDto>>(url);
        
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.True(result.Data.Name.Length > 0);
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
                    Warehouses = new List<Warehouse> {
                        new() {
                            Id = 5,
                            Name = "Warehouse 5"
                        },
                        new() {
                            Id = 6,
                            Name = "Warehouse 6"
                        }
                    }
                }
        });

        var saleschannel = new SalesChannelUpdateCommand
        {
            SalesChannelType = SalesChannelType.PointOfSale,
            Name = "SalesChannel 5 updated",
            Username = string.Empty,
            Password = string.Empty,
            ImportProducts = false,
            ImportCustomers = false,
            ImportOrders = false,
            ExportProducts = false,
            ExportCustomers = false,
            ExportOrders = false,
            WarehouseIds = new List<int> { 1, 2 },
        };

        var httpResponseMessage = await httpClient.PutAsJsonAsync(url, saleschannel);
        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>();
        
        Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.True(result.StatusCode == ResultStatusCode.Ok);
        Assert.IsType<int>(result.Data);
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