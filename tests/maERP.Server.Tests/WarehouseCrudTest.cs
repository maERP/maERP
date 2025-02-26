using System.Net;
using System.Net.Http.Json;
using maERP.Application.Features.Warehouse.Commands.WarehouseCreate;
using maERP.Application.Features.Warehouse.Commands.WarehouseUpdate;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;

namespace maERP.Server.Tests;

[Collection("Sequential")]
public class WarehouseCrudTest : IClassFixture<MaErpWebApplicationFactory<Program>>
{
    private readonly MaErpWebApplicationFactory<Program> _webApplicationFactory;

    public WarehouseCrudTest(MaErpWebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Theory]
    [InlineData("/api/v1/Warehouses")]
    public async Task Create(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests();
        var warehouse = new WarehouseCreateCommand
        {
            Name = "Warehouse 1"
        };

        var httpResponseMessage = await httpClient.PostAsJsonAsync(url, warehouse);
        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>();
        
        Assert.True(httpResponseMessage.IsSuccessStatusCode);
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
    }

    [Theory]
    [InlineData("/api/v1/Warehouses/")]
    public async Task GetAll(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<Warehouse> {
                new() {
                    Id = 2,
                    Name = "Warehouse 2"
                }
        });

        var result = await httpClient.GetFromJsonAsync<PaginatedResult<WarehouseListDto>>(url);

        Assert.NotNull(result);
        Assert.Equal(2, result.TotalCount);
    }

    [Theory]
    [InlineData("/api/v1/Warehouses/3")]
    public async Task GetDetail(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<Warehouse> {
                new() {
                    Id = 3,
                    Name = "Warehouse 3"
                }
        });

        var result = await httpClient.GetFromJsonAsync<Result<WarehouseDetailDto>>(url);
        
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.True(result.Data.Name.Length > 0);
    }

    [Theory]
    [InlineData("/api/v1/Warehouses/4")]
    public async Task Update(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests(
            new List<Warehouse> {
                new() {
                    Id = 4,
                    Name = "Warehouse 4"
                }
        });

        var warehouse = new WarehouseUpdateCommand
        {
            Name = "Warehouse 3 updated",
        };

        var httpResponseMessage = await httpClient.PutAsJsonAsync(url, warehouse);
        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>();
        
        Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.True(result.StatusCode == ResultStatusCode.Ok);
        Assert.IsType<int>(result.Data);
    }

    [Theory]
    [InlineData("/api/v1/Warehouses/5")]
    public async Task Delete(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<Warehouse> {
                new() {
                    Id = 5,
                    Name = "Warehouse 5"
                }
        });

        HttpResponseMessage result = await httpClient.DeleteAsync(url);

        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Theory]
    [InlineData("/api/v1/Warehouses/999999")]
    public async Task NotExist(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests();

        HttpResponseMessage result = await httpClient.GetAsync(url);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}