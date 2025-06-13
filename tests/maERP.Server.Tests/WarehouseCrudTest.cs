using System.Net;
using System.Net.Http.Json;
using maERP.Application.Features.Warehouse.Commands.WarehouseCreate;
using maERP.Application.Features.Warehouse.Commands.WarehouseUpdate;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using maERP.Persistence.DatabaseContext;
using Microsoft.Extensions.DependencyInjection;

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
        Assert.True(result.Data.ProductCount >= 0);
    }

    [Theory]
    [InlineData("/api/v1/Warehouses/6")]
    public async Task GetDetailWithProducts(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        
        // Initialize database and add test data manually
        using var scope = _webApplicationFactory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        await db.Database.EnsureDeletedAsync();
        await db.Database.EnsureCreatedAsync();
        
        // Add test data
        await db.Warehouse.AddAsync(new Warehouse { Id = 6, Name = "Warehouse 6" });
        await db.Product.AddRangeAsync(
            new Product { Id = 1, Sku = "TEST-SKU-1", Name = "Test Product 1", TaxClassId = 1 },
            new Product { Id = 2, Sku = "TEST-SKU-2", Name = "Test Product 2", TaxClassId = 1 }
        );
        await db.ProductStock.AddRangeAsync(
            new ProductStock { ProductId = 1, WarehouseId = 6, Stock = 10 },
            new ProductStock { ProductId = 2, WarehouseId = 6, Stock = 5 }
        );
        await db.SaveChangesAsync();

        var result = await httpClient.GetFromJsonAsync<Result<WarehouseDetailDto>>(url);
        
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.Equal("Warehouse 6", result.Data.Name);
        Assert.Equal(2, result.Data.ProductCount);
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
            Name = "Warehouse 4 updated",
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
    [InlineData("/api/v1/Warehouses/7?newWarehouseId=8")]
    public async Task DeleteWithProductRedistribution(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        
        // Initialize database and add test data manually
        using var scope = _webApplicationFactory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        await db.Database.EnsureDeletedAsync();
        await db.Database.EnsureCreatedAsync();
        
        // Add test warehouses
        await db.Warehouse.AddRangeAsync(
            new Warehouse { Id = 7, Name = "Warehouse 7 (to delete)" },
            new Warehouse { Id = 8, Name = "Warehouse 8 (target)" }
        );
        
        // Add test products
        await db.Product.AddRangeAsync(
            new Product { Id = 3, Sku = "TEST-SKU-3", Name = "Test Product 3", TaxClassId = 1 },
            new Product { Id = 4, Sku = "TEST-SKU-4", Name = "Test Product 4", TaxClassId = 1 }
        );
        
        // Add product stocks to warehouse 7
        await db.ProductStock.AddRangeAsync(
            new ProductStock { ProductId = 3, WarehouseId = 7, Stock = 15 },
            new ProductStock { ProductId = 4, WarehouseId = 7, Stock = 8 }
        );
        
        await db.SaveChangesAsync();

        // Delete warehouse 7 and redistribute products to warehouse 8
        HttpResponseMessage result = await httpClient.DeleteAsync(url);

        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        
        // Verify products were redistributed to warehouse 8
        var redistributedStocks = db.ProductStock.Where(ps => ps.WarehouseId == 8).ToList();
        Assert.Equal(2, redistributedStocks.Count);
        Assert.Contains(redistributedStocks, ps => ps.ProductId == 3 && ps.Stock == 15);
        Assert.Contains(redistributedStocks, ps => ps.ProductId == 4 && ps.Stock == 8);
        
        // Verify no stocks remain for warehouse 7
        var remainingStocks = db.ProductStock.Where(ps => ps.WarehouseId == 7).ToList();
        Assert.Empty(remainingStocks);
    }

    [Theory]
    [InlineData("/api/v1/Warehouses/9")]
    public async Task DeleteWithoutRedistribution(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        
        // Initialize database and add test data manually
        using var scope = _webApplicationFactory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        await db.Database.EnsureDeletedAsync();
        await db.Database.EnsureCreatedAsync();
        
        // Add test warehouse
        await db.Warehouse.AddAsync(new Warehouse { Id = 9, Name = "Warehouse 9 (to delete)" });
        
        // Add test product
        await db.Product.AddAsync(new Product { Id = 5, Sku = "TEST-SKU-5", Name = "Test Product 5", TaxClassId = 1 });
        
        // Add product stock to warehouse 9
        await db.ProductStock.AddAsync(new ProductStock { ProductId = 5, WarehouseId = 9, Stock = 20 });
        
        await db.SaveChangesAsync();

        // Delete warehouse 9 without redistribution
        HttpResponseMessage result = await httpClient.DeleteAsync(url);

        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        
        // Verify all product stocks for warehouse 9 were deleted
        var remainingStocks = db.ProductStock.Where(ps => ps.WarehouseId == 9).ToList();
        Assert.Empty(remainingStocks);
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