using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.Product;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace maERP.Server.Tests.Features.Product.Queries;

public class ProductListQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public ProductListQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_ProductListQueryTests_{uniqueId}";
        Environment.SetEnvironmentVariable("TEST_DB_NAME", testDbName);

        Factory = new TestWebApplicationFactory<Program>();
        Client = Factory.CreateClient();

        Scope = Factory.Services.CreateScope();
        DbContext = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        TenantContext = Scope.ServiceProvider.GetRequiredService<ITenantContext>();

        DbContext.Database.EnsureCreated();

        TenantContext.SetAssignedTenantIds(new[] { 1, 2 });
        TenantContext.SetCurrentTenantId(null);
    }

    protected void SetTenantHeader(int tenantId)
    {
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", tenantId.ToString());
    }

    protected async Task<T> ReadResponseAsync<T>(HttpResponseMessage response) where T : class
    {
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return result ?? throw new InvalidOperationException("Failed to deserialize response");
    }

    private async Task SeedProductTestDataAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var hasData = await DbContext.Product.AnyAsync();
            if (!hasData)
            {
                await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

                var manufacturer1 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = 1,
                    Name = "Test Manufacturer 1",
                    City = "Test City 1",
                    Country = "Test Country 1",
                    TenantId = 1
                };

                var manufacturer2 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = 2,
                    Name = "Test Manufacturer 2",
                    City = "Test City 2",
                    Country = "Test Country 2",
                    TenantId = 2
                };

                DbContext.Manufacturer.AddRange(manufacturer1, manufacturer2);

                var product1Tenant1 = new maERP.Domain.Entities.Product
                {
                    Id = 1,
                    Sku = "TEST-001",
                    Name = "Test Product 1",
                    Description = "Description for product 1",
                    Ean = "1234567890123",
                    Price = 99.99m,
                    Msrp = 119.99m,
                    ManufacturerId = 1,
                    TaxClassId = 1,
                    TenantId = 1
                };

                var product2Tenant1 = new maERP.Domain.Entities.Product
                {
                    Id = 2,
                    Sku = "TEST-002",
                    Name = "Another Test Product",
                    Description = "Description for product 2",
                    Ean = "2345678901234",
                    Price = 149.99m,
                    Msrp = 179.99m,
                    ManufacturerId = 1,
                    TaxClassId = 1,
                    TenantId = 1
                };

                var product3Tenant2 = new maERP.Domain.Entities.Product
                {
                    Id = 3,
                    Sku = "TEST-003",
                    Name = "Tenant 2 Product",
                    Description = "Product for tenant 2",
                    Ean = "3456789012345",
                    Price = 79.99m,
                    Msrp = 99.99m,
                    ManufacturerId = 2,
                    TaxClassId = 1,
                    TenantId = 2
                };

                DbContext.Product.AddRange(product1Tenant1, product2Tenant1, product3Tenant2);
                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task GetProducts_WithValidTenant_ShouldReturnTenantData()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Products");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetProducts_WithDifferentTenant_ShouldReturnOnlyThatTenantData()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(2);

        var response = await Client.GetAsync("/api/v1/Products");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual("Tenant 2 Product", result.Data?.First().Name);
    }

    [Fact]
    public async Task GetProducts_WithoutTenantHeader_ShouldReturnEmptyResult()
    {
        await SeedProductTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Products");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetProducts_WithPagination_ShouldRespectPageSize()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Products?pageNumber=0&pageSize=1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual(2, result.TotalCount);
        TestAssertions.AssertEqual(2, result.TotalPages);
    }

    [Fact]
    public async Task GetProducts_WithPaginationSecondPage_ShouldReturnSecondPageData()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Products?pageNumber=1&pageSize=1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual(2, result.TotalCount);
        TestAssertions.AssertEqual(2, result.TotalPages);
    }

    [Fact]
    public async Task GetProducts_WithSearchString_ShouldFilterResults()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Products?searchString=Another");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertTrue(result.Data?.First().Name.Contains("Another") ?? false);
    }

    [Fact]
    public async Task GetProducts_WithSearchStringNoMatch_ShouldReturnEmpty()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Products?searchString=NonexistentProduct");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetProducts_WithOrderByName_ShouldReturnOrderedResults()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Products?orderBy=Name");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        
        var names = result.Data?.Select(x => x.Name).ToList();
        TestAssertions.AssertEqual("Another Test Product", names?[0]);
        TestAssertions.AssertEqual("Test Product 1", names?[1]);
    }

    [Fact]
    public async Task GetProducts_WithOrderByNameDescending_ShouldReturnDescOrderedResults()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Products?orderBy=Name desc");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        
        var names = result.Data?.Select(x => x.Name).ToList();
        TestAssertions.AssertEqual("Test Product 1", names?[0]);
        TestAssertions.AssertEqual("Another Test Product", names?[1]);
    }

    [Fact]
    public async Task GetProducts_WithOrderByPrice_ShouldReturnPriceOrderedResults()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Products?orderBy=Price");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        
        var prices = result.Data?.Select(x => x.Price).ToList();
        TestAssertions.AssertEqual(99.99m, prices?[0]);
        TestAssertions.AssertEqual(149.99m, prices?[1]);
    }

    [Fact]
    public async Task GetProducts_WithMultipleOrderBy_ShouldRespectMultipleSorting()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Products?orderBy=Price,Name");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetProducts_WithInvalidPageNumber_ShouldReturnEmptyResults()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Products?pageNumber=10&pageSize=10");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(2, result.TotalCount);
    }

    [Fact]
    public async Task GetProducts_WithZeroPageSize_ShouldUseDefaultPageSize()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Products?pageSize=0");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetProducts_WithNegativePageNumber_ShouldHandleGracefully()
    {
        SetTenantHeader(999);

        var response = await Client.GetAsync("/api/v1/Products?pageNumber=-1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetProducts_WithNonExistentTenant_ShouldReturnEmptyPaginatedResult()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(999);

        var response = await Client.GetAsync("/api/v1/Products");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(0, result.TotalCount);
        TestAssertions.AssertEqual(0, result.TotalPages);
    }

    [Fact]
    public async Task GetProducts_ResponseStructure_ShouldContainRequiredFields()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Products");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);

        var firstProduct = result.Data?.First();
        TestAssertions.AssertNotNull(firstProduct);
        TestAssertions.AssertTrue(firstProduct!.Id > 0);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstProduct.Name));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstProduct.Sku));
        TestAssertions.AssertTrue(firstProduct.Price > 0);
    }

    [Fact]
    public async Task GetProducts_WithManufacturerData_ShouldIncludeManufacturerInfo()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Products");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);

        var firstProduct = result.Data?.First();
        TestAssertions.AssertNotNull(firstProduct?.Manufacturer);
        TestAssertions.AssertEqual("Test Manufacturer 1", firstProduct?.Manufacturer?.Name);
        TestAssertions.AssertEqual("Test City 1", firstProduct?.Manufacturer?.City);
    }

    [Fact]
    public async Task GetProducts_WithSpecificSkuSearch_ShouldFilterBySku()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Products?searchString=TEST-001");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual("TEST-001", result.Data?.First().Sku);
    }

    [Fact]
    public async Task GetProducts_WithSkuSearch_ShouldFilterBySku()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Products?searchString=TEST-002");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual("Another Test Product", result.Data?.First().Name);
    }

    [Fact]
    public async Task GetProducts_WithNameSearch_ShouldFilterByName()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Products?searchString=Another");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual("Another Test Product", result.Data?.First().Name);
    }

    [Fact]
    public async Task GetProducts_WithCaseInsensitiveSearch_ShouldReturnResults()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Products?searchString=test");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetProducts_WithPartialSearch_ShouldReturnMatchingResults()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Products?searchString=Test");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetProducts_WithEmptySearchString_ShouldReturnAllResults()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Products?searchString=");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
    }
}