using System.Net;
using System.Text.Json;
using maERP.Domain.Constants;
using maERP.Domain.Dtos.Product;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace maERP.Server.Tests.Features.Product.Queries;

public class ProductDetailQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public ProductDetailQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_ProductDetailQueryTests_{uniqueId}";
        Environment.SetEnvironmentVariable("TEST_DB_NAME", testDbName);

        Factory = new TestWebApplicationFactory<Program>();
        Client = Factory.CreateClient();

        Scope = Factory.Services.CreateScope();
        DbContext = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        TenantContext = Scope.ServiceProvider.GetRequiredService<ITenantContext>();

        DbContext.Database.EnsureCreated();

        TenantContext.SetAssignedTenantIds(new[] { TenantConstants.TestTenant1Id, TenantConstants.TestTenant2Id });
        TenantContext.SetCurrentTenantId(null);
    }

    protected void SetTenantHeader(Guid tenantId)
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

                var taxClass = new maERP.Domain.Entities.TaxClass
                {
                    Id = Guid.Parse("00000001-0001-0001-0001-000000000001"),
                    TaxRate = 19.0,
                    TenantId = TenantConstants.TestTenant1Id
                };

                var manufacturer1 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = Guid.Parse("00000001-0001-0001-0002-000000000001"),
                    Name = "Test Manufacturer 1",
                    Street = "123 Test Street",
                    City = "Test City 1",
                    State = "Test State",
                    Country = "Test Country 1",
                    ZipCode = "12345",
                    Phone = "+1234567890",
                    Email = "test@manufacturer1.com",
                    Website = "https://manufacturer1.com",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var manufacturer2 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = Guid.Parse("00000001-0001-0001-0002-000000000002"),
                    Name = "Test Manufacturer 2",
                    City = "Test City 2",
                    Country = "Test Country 2",
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.TaxClass.Add(taxClass);
                DbContext.Manufacturer.AddRange(manufacturer1, manufacturer2);

                var product1Tenant1 = new maERP.Domain.Entities.Product
                {
                    Id = Guid.Parse("00000001-0001-0001-0003-000000000001"),
                    Sku = "TEST-001",
                    Name = "Test Product 1",
                    NameOptimized = "Optimized Test Product 1",
                    Description = "Description for product 1",
                    DescriptionOptimized = "Optimized description for product 1",
                    UseOptimized = true,
                    Ean = "1234567890123",
                    Asin = "B08TEST001",
                    Price = 99.99m,
                    Msrp = 119.99m,
                    Weight = 1.5m,
                    Width = 10.0m,
                    Height = 5.0m,
                    Depth = 15.0m,
                    ManufacturerId = Guid.Parse("00000001-0001-0001-0002-000000000001"),
                    TaxClassId = Guid.Parse("00000001-0001-0001-0001-000000000001"),
                    TenantId = TenantConstants.TestTenant1Id
                };

                var product2Tenant1 = new maERP.Domain.Entities.Product
                {
                    Id = Guid.Parse("00000001-0001-0001-0003-000000000002"),
                    Sku = "TEST-002",
                    Name = "Test Product 2",
                    Description = "Description for product 2",
                    Ean = "2345678901234",
                    Price = 149.99m,
                    Msrp = 179.99m,
                    Weight = 2.0m,
                    Width = 12.0m,
                    Height = 8.0m,
                    Depth = 18.0m,
                    ManufacturerId = Guid.Parse("00000001-0001-0001-0002-000000000001"),
                    TaxClassId = Guid.Parse("00000001-0001-0001-0001-000000000001"),
                    UseOptimized = false,
                    TenantId = TenantConstants.TestTenant1Id
                };

                var product3Tenant2 = new maERP.Domain.Entities.Product
                {
                    Id = Guid.Parse("00000001-0001-0001-0003-000000000003"),
                    Sku = "TEST-003",
                    Name = "Tenant 2 Product",
                    Description = "Product for tenant 2",
                    Ean = "3456789012345",
                    Price = 79.99m,
                    Msrp = 99.99m,
                    ManufacturerId = Guid.Parse("00000001-0001-0001-0002-000000000002"),
                    TaxClassId = Guid.Parse("00000001-0001-0001-0001-000000000001"),
                    TenantId = TenantConstants.TestTenant2Id
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
    public async Task GetProductDetail_WithValidIdAndTenant_ShouldReturnProductDetail()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Products/{Guid.Parse("00000001-0001-0001-0003-000000000001")}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<ProductDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(Guid.Parse("00000001-0001-0001-0003-000000000001"), result.Data!.Id);
        TestAssertions.AssertEqual("TEST-001", result.Data.Sku);
        TestAssertions.AssertEqual("Test Product 1", result.Data.Name);
    }

    [Fact]
    public async Task GetProductDetail_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Products/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<ProductDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetProductDetail_WithWrongTenant_ShouldReturnNotFound()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var response = await Client.GetAsync($"/api/v1/Products/{Guid.Parse("00000001-0001-0001-0003-000000000001")}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<ProductDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetProductDetail_WithoutTenantHeader_ShouldReturnNotFound()
    {
        await SeedProductTestDataAsync();

        var response = await Client.GetAsync($"/api/v1/Products/{Guid.Parse("00000001-0001-0001-0003-000000000001")}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<ProductDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetProductDetail_WithValidId_ShouldIncludeAllProductFields()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Products/{Guid.Parse("00000001-0001-0001-0003-000000000001")}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<ProductDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var product = result.Data!;
        TestAssertions.AssertEqual(Guid.Parse("00000001-0001-0001-0003-000000000001"), product.Id);
        TestAssertions.AssertEqual("TEST-001", product.Sku);
        TestAssertions.AssertEqual("Test Product 1", product.Name);
        TestAssertions.AssertEqual("Optimized Test Product 1", product.NameOptimized);
        TestAssertions.AssertEqual("1234567890123", product.Ean);
        TestAssertions.AssertEqual("B08TEST001", product.Asin);
        TestAssertions.AssertEqual("Description for product 1", product.Description);
        TestAssertions.AssertEqual("Optimized description for product 1", product.DescriptionOptimized);
        TestAssertions.AssertTrue(product.UseOptimized);
        TestAssertions.AssertEqual(99.99m, product.Price);
        TestAssertions.AssertEqual(119.99m, product.Msrp);
        TestAssertions.AssertEqual(1.5m, product.Weight);
        TestAssertions.AssertEqual(10.0m, product.Width);
        TestAssertions.AssertEqual(5.0m, product.Height);
        TestAssertions.AssertEqual(15.0m, product.Depth);
        TestAssertions.AssertEqual(Guid.Parse("00000001-0001-0001-0001-000000000001"), product.TaxClassId);
    }

    [Fact]
    public async Task GetProductDetail_WithManufacturer_ShouldIncludeManufacturerDetails()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Products/{Guid.Parse("00000001-0001-0001-0003-000000000001")}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<ProductDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var manufacturer = result.Data!.Manufacturer;
        TestAssertions.AssertNotNull(manufacturer);
        TestAssertions.AssertEqual(Guid.Parse("00000001-0001-0001-0002-000000000001"), manufacturer!.Id);
        TestAssertions.AssertEqual("Test Manufacturer 1", manufacturer.Name);
        TestAssertions.AssertEqual("123 Test Street", manufacturer.Street);
        TestAssertions.AssertEqual("Test City 1", manufacturer.City);
        TestAssertions.AssertEqual("Test State", manufacturer.State);
        TestAssertions.AssertEqual("Test Country 1", manufacturer.Country);
        TestAssertions.AssertEqual("12345", manufacturer.ZipCode);
        TestAssertions.AssertEqual("+1234567890", manufacturer.Phone);
        TestAssertions.AssertEqual("test@manufacturer1.com", manufacturer.Email);
        TestAssertions.AssertEqual("https://manufacturer1.com", manufacturer.Website);
    }

    [Fact]
    public async Task GetProductDetail_WithTenant2Product_ShouldReturnCorrectProduct()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var response = await Client.GetAsync($"/api/v1/Products/{Guid.Parse("00000001-0001-0001-0003-000000000003")}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<ProductDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(Guid.Parse("00000001-0001-0001-0003-000000000003"), result.Data!.Id);
        TestAssertions.AssertEqual("Tenant 2 Product", result.Data.Name);
    }

    [Fact]
    public async Task GetProductDetail_WithInvalidId_ShouldReturnBadRequest()
    {
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Products/invalid");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetProductDetail_WithZeroId_ShouldReturnNotFound()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Products/{Guid.Empty}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<ProductDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetProductDetail_WithNegativeId_ShouldReturnNotFound()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Products/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<ProductDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetProductDetail_WithProductWithoutOptimizedContent_ShouldReturnCorrectly()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Products/{Guid.Parse("00000001-0001-0001-0003-000000000002")}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<ProductDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var product = result.Data!;
        TestAssertions.AssertEqual(Guid.Parse("00000001-0001-0001-0003-000000000002"), product.Id);
        TestAssertions.AssertEqual("Test Product 2", product.Name);
        TestAssertions.AssertFalse(product.UseOptimized);
        Assert.Null(product.NameOptimized);
        Assert.Null(product.DescriptionOptimized);
    }

    [Fact]
    public async Task GetProductDetail_WithNonExistentTenant_ShouldReturnNotFound()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(Guid.NewGuid());

        var response = await Client.GetAsync($"/api/v1/Products/{Guid.Parse("00000001-0001-0001-0003-000000000001")}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<ProductDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetProductDetail_ResponseStructure_ShouldHaveCorrectSuccessFormat()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Products/{Guid.Parse("00000001-0001-0001-0003-000000000001")}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<ProductDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task GetProductDetail_ResponseStructure_ShouldHaveCorrectErrorFormat()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Products/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<ProductDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task GetProductDetail_WithLargeId_ShouldHandleGracefully()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Products/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<ProductDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetProductDetail_TenantIsolation_ShouldNotReturnOtherTenantProducts()
    {
        await SeedProductTestDataAsync();

        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync($"/api/v1/Products/{Guid.Parse("00000001-0001-0001-0003-000000000003")}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response1.StatusCode);

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await Client.GetAsync($"/api/v1/Products/{Guid.Parse("00000001-0001-0001-0003-000000000001")}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);

        var response3 = await Client.GetAsync($"/api/v1/Products/{Guid.Parse("00000001-0001-0001-0003-000000000002")}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response3.StatusCode);
    }

    [Fact]
    public async Task GetProductDetail_WithNullProductSalesChannels_ShouldReturnEmptyList()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Products/{Guid.Parse("00000001-0001-0001-0003-000000000001")}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<ProductDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data!.ProductSalesChannel);
        TestAssertions.AssertEmpty(result.Data.ProductSalesChannel);
    }

    [Fact]
    public async Task GetProductDetail_WithProductStocks_ShouldReturnStockIds()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Products/{Guid.Parse("00000001-0001-0001-0003-000000000001")}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<ProductDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data!.ProductStocks);
    }

    [Fact]
    public async Task GetProductDetail_WithCompleteProductData_ShouldMapAllFields()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Products/{Guid.Parse("00000001-0001-0001-0003-000000000001")}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<ProductDetailDto>>(response);
        var product = result.Data!;

        TestAssertions.AssertTrue(product.Id != Guid.Empty);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(product.Sku));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(product.Name));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(product.Description));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(product.Ean));
        TestAssertions.AssertTrue(product.Price >= 0);
        TestAssertions.AssertTrue(product.Msrp >= 0);
        TestAssertions.AssertTrue(product.Weight > 0);
        TestAssertions.AssertTrue(product.Width > 0);
        TestAssertions.AssertTrue(product.Height > 0);
        TestAssertions.AssertTrue(product.Depth > 0);
        TestAssertions.AssertTrue(product.TaxClassId != Guid.Empty);
    }
}