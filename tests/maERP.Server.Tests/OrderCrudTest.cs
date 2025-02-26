using System.Net;
using System.Net.Http.Json;
using maERP.Application.Features.Order.Commands.OrderUpdate;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;

namespace maERP.Server.Tests;

[Collection("Sequential")]
public class OrderCrudTest : IClassFixture<MaErpWebApplicationFactory<Program>>
{
    private readonly MaErpWebApplicationFactory<Program> _webApplicationFactory;

    public OrderCrudTest(MaErpWebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Theory]
    [InlineData("/api/v1/Orders")]
    public async Task Create(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests();
        var order = new OrderUpdateCommand
        {
            SalesChannelId = 1,
            CustomerId = 1,
            Status = 1
        };

        var httpResponseMessage = await httpClient.PostAsJsonAsync(url, order);
        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>();
        
        Assert.True(httpResponseMessage.IsSuccessStatusCode);
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
    }

    [Theory]
    [InlineData("/api/v1/Orders/")]
    public async Task GetAll(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<Order> {
                new() {
                    Id = 2,
                    RemoteOrderId = "222"
                }
        });

        var result = await httpClient.GetFromJsonAsync<PaginatedResult<OrderListDto>>(url);

        Assert.NotNull(result);
        Assert.Equal(1, result.TotalCount);
    }

    [Theory]
    [InlineData("/api/v1/Orders/3")]
    public async Task GetDetail(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<Order> {
                new() {
                    Id = 3,
                    RemoteOrderId = "333"
                }
        });

        var result = await httpClient.GetFromJsonAsync<Result<OrderDetailDto>>(url);
        
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.True(result.Data.RemoteOrderId.Length > 0);
    }

    [Theory]
    [InlineData("/api/v1/Orders/4")]
    public async Task Update(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests(
            new List<Order> {
                new() {
                    Id = 4,
                    RemoteOrderId = "444"
                }
        });

        var order = new OrderUpdateCommand
        {
            RemoteOrderId = "444-updated",
        };

        var httpResponseMessage = await httpClient.PutAsJsonAsync(url, order);
        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>();

        Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.True(result.StatusCode == ResultStatusCode.Ok);
        Assert.IsType<int>(result.Data);
    }

    [Theory]
    [InlineData("/api/v1/Orders/5")]
    public async Task Delete(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<Order> {
                new() {
                    Id = 5,
                    RemoteOrderId = "555"
                }
        });

        HttpResponseMessage result = await httpClient.DeleteAsync(url);

        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Theory]
    [InlineData("/api/v1/Orders/999999")]
    public async Task NotExist(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests();

        HttpResponseMessage result = await httpClient.GetAsync(url);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}