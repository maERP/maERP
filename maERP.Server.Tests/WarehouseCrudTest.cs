using maERP.Application.Dtos;
using maERP.Application.Dtos.Warehouse;
using maERP.Domain.Models;
using System.Net;
using System.Net.Http.Json;

namespace maERP.Server.Tests;
public class WarehouseCrudTest : IClassFixture<maERPWebApplicationFactory<Program>>
{
    private readonly maERPWebApplicationFactory<Program> _webApplicationFactory;

    public WarehouseCrudTest(maERPWebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Theory]
    [InlineData("/api/v1/Warehouses")]
    public async Task Create(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests();
        var warehouse = new WarehouseCreateDto
        {
            Name = "Warehouse 1"
        };

        HttpResponseMessage result = await httpClient.PostAsJsonAsync(url, warehouse);
        WarehouseDetailDto? resultContent = await result.Content.ReadFromJsonAsync<WarehouseDetailDto>();

        Assert.NotNull(resultContent);
        Assert.True(result.IsSuccessStatusCode);
        Assert.True(resultContent != null && resultContent.Id != default);
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

        ICollection<WarehouseListDto>? result = await httpClient.GetFromJsonAsync<ICollection<WarehouseListDto>>(url);

        Assert.NotNull(result);
        Assert.Equal(result?.Count, 1);
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

        WarehouseDetailDto? result = await httpClient.GetFromJsonAsync<WarehouseDetailDto>(url);

        Assert.NotNull(result);
        Assert.True(result.Name.Length > 0);
    }

    /*
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

        var warehouse = new WarehouseUpdateDto
        {
            Name = "Warehouse 3 updated",
        };

        HttpResponseMessage result = await httpClient.PutAsJsonAsync(url, warehouse);
        WarehouseDetailDto? resultContent = await result.Content.ReadFromJsonAsync<WarehouseDetailDto>();

        Assert.NotNull(resultContent);
        Assert.True(result.IsSuccessStatusCode);
        Assert.True(resultContent != null && resultContent.Name == warehouse.Name);
    }
    */

    /*
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

        Assert.Equal(result?.StatusCode, HttpStatusCode.NoContent);
    }
    */

    [Theory]
    [InlineData("/api/v1/Warehouses/999999")]
    public async Task NotExist(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests();

        HttpResponseMessage result = await httpClient.GetAsync(url);

        Assert.Equal(result?.StatusCode, HttpStatusCode.NotFound);
    }
}