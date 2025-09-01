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

namespace maERP.Server.Tests.Features.Product.Commands;

public class ProductCreateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public ProductCreateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_ProductCreateCommandTests_{uniqueId}";
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

    protected async Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        return await Client.PostAsync(requestUri, content);
    }

    protected async Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        return await Client.PutAsync(requestUri, content);
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

    private async Task SeedTestDataAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var hasData = await DbContext.TaxClass.AnyAsync();
            if (!hasData)
            {
                await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

                var taxClass = new maERP.Domain.Entities.TaxClass
                {
                    Id = 1,
                    TaxRate = 19.0,
                    TenantId = 1
                };

                var manufacturer = new maERP.Domain.Entities.Manufacturer
                {
                    Id = 1,
                    Name = "Test Manufacturer",
                    City = "Test City",
                    Country = "Test Country",
                    TenantId = 1
                };

                DbContext.TaxClass.Add(taxClass);
                DbContext.Manufacturer.Add(manufacturer);
                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private ProductInputDto CreateValidProductDto()
    {
        return new ProductInputDto
        {
            Sku = "TEST-NEW-001",
            Name = "New Test Product",
            Description = "A test product for creation",
            Price = 99.99m,
            Msrp = 129.99m,
            Weight = 1.5m,
            Width = 10.0m,
            Height = 5.0m,
            Depth = 15.0m,
            TaxClassId = 1,
            ManufacturerId = 1,
            UseOptimized = false
        };
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task CreateProduct_WithValidData_ShouldReturnCreated()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var productDto = CreateValidProductDto();

        var response = await PostAsJsonAsync("/api/v1/Products", productDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data > 0);
    }

    [Fact]
    public async Task CreateProduct_WithValidData_ShouldPersistInDatabase()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var productDto = CreateValidProductDto();

        var response = await PostAsJsonAsync("/api/v1/Products", productDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        
        var createdProduct = await DbContext.Product.FindAsync(result.Data);
        TestAssertions.AssertNotNull(createdProduct);
        TestAssertions.AssertEqual(productDto.Sku, createdProduct!.Sku);
        TestAssertions.AssertEqual(productDto.Name, createdProduct.Name);
        TestAssertions.AssertEqual(productDto.Price, createdProduct.Price);
        TestAssertions.AssertEqual(1, createdProduct.TenantId);
    }

    [Fact]
    public async Task CreateProduct_WithMissingRequiredFields_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var productDto = new ProductInputDto
        {
            // Missing required fields Sku, Name, Price
            Description = "Incomplete product"
        };

        var response = await PostAsJsonAsync("/api/v1/Products", productDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateProduct_WithDuplicateSku_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        
        // Create first product
        var firstProduct = CreateValidProductDto();
        await PostAsJsonAsync("/api/v1/Products", firstProduct);

        // Try to create second product with same SKU
        var duplicateProduct = CreateValidProductDto();
        duplicateProduct.Name = "Duplicate SKU Product";

        var response = await PostAsJsonAsync("/api/v1/Products", duplicateProduct);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateProduct_WithoutTenantHeader_ShouldReturnUnauthorized()
    {
        await SeedTestDataAsync();
        var productDto = CreateValidProductDto();

        var response = await PostAsJsonAsync("/api/v1/Products", productDto);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateProduct_WithNonExistentTenant_ShouldHandleGracefully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(999);
        var productDto = CreateValidProductDto();

        var response = await PostAsJsonAsync("/api/v1/Products", productDto);

        // Should still try to create but may fail validation or return error
        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.BadRequest || 
                                 response.StatusCode == HttpStatusCode.NotFound ||
                                 response.StatusCode == HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CreateProduct_WithInvalidTaxClassId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var productDto = CreateValidProductDto();
        productDto.TaxClassId = 999; // Non-existent tax class

        var response = await PostAsJsonAsync("/api/v1/Products", productDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateProduct_WithInvalidManufacturerId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var productDto = CreateValidProductDto();
        productDto.ManufacturerId = 999; // Non-existent manufacturer

        var response = await PostAsJsonAsync("/api/v1/Products", productDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateProduct_WithNegativePrice_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var productDto = CreateValidProductDto();
        productDto.Price = -10.00m;

        var response = await PostAsJsonAsync("/api/v1/Products", productDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateProduct_WithTooLongSku_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var productDto = CreateValidProductDto();
        productDto.Sku = new string('A', 256); // Exceeds 255 character limit

        var response = await PostAsJsonAsync("/api/v1/Products", productDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateProduct_WithTooLongName_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var productDto = CreateValidProductDto();
        productDto.Name = new string('A', 256); // Exceeds 255 character limit

        var response = await PostAsJsonAsync("/api/v1/Products", productDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateProduct_WithOptionalFields_ShouldCreateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var productDto = CreateValidProductDto();
        productDto.NameOptimized = "Optimized Name";
        productDto.DescriptionOptimized = "Optimized Description";
        productDto.Ean = "1234567890123";
        productDto.Asin = "B08TEST001";
        productDto.UseOptimized = true;

        var response = await PostAsJsonAsync("/api/v1/Products", productDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        
        var createdProduct = await DbContext.Product.FindAsync(result.Data);
        TestAssertions.AssertNotNull(createdProduct);
        TestAssertions.AssertEqual(productDto.NameOptimized, createdProduct!.NameOptimized);
        TestAssertions.AssertEqual(productDto.Ean, createdProduct.Ean);
        TestAssertions.AssertEqual(productDto.Asin, createdProduct.Asin);
        TestAssertions.AssertTrue(createdProduct.UseOptimized);
    }

    [Fact]
    public async Task CreateProduct_WithMinimalRequiredData_ShouldCreateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var productDto = new ProductInputDto
        {
            Sku = "MINIMAL-001",
            Name = "Minimal Product",
            Price = 1.00m,
            TaxClassId = 1
        };

        var response = await PostAsJsonAsync("/api/v1/Products", productDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data > 0);
    }

    [Fact]
    public async Task CreateProduct_WithMaxValidStringLengths_ShouldCreateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var productDto = CreateValidProductDto();
        productDto.Sku = new string('A', 255); // Max allowed length
        productDto.Name = new string('B', 255); // Max allowed length
        productDto.Ean = new string('1', 32); // Max allowed length
        productDto.Asin = new string('C', 32); // Max allowed length

        var response = await PostAsJsonAsync("/api/v1/Products", productDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task CreateProduct_TenantIsolation_ShouldOnlyCreateInCorrectTenant()
    {
        await SeedTestDataAsync();

        // Create product in tenant 1
        SetTenantHeader(1);
        var productDto = CreateValidProductDto();
        var response1 = await PostAsJsonAsync("/api/v1/Products", productDto);
        TestAssertions.AssertEqual(HttpStatusCode.Created, response1.StatusCode);

        // Verify product exists in tenant 1
        var response2 = await Client.GetAsync("/api/v1/Products");
        TestAssertions.AssertHttpSuccess(response2);
        var listResult = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response2);
        TestAssertions.AssertEqual(1, listResult.Data?.Count ?? 0);

        // Verify product does not exist in tenant 2
        SetTenantHeader(2);
        var response3 = await Client.GetAsync("/api/v1/Products");
        TestAssertions.AssertHttpSuccess(response3);
        var listResult2 = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response3);
        TestAssertions.AssertEmpty(listResult2.Data ?? new List<ProductListDto>());
    }

    [Fact]
    public async Task CreateProduct_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var productDto = CreateValidProductDto();

        var response = await PostAsJsonAsync("/api/v1/Products", productDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data > 0);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task CreateProduct_WithInvalidJson_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var invalidJson = "{ invalid json }";
        var content = new StringContent(invalidJson, System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PostAsync("/api/v1/Products", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateProduct_WithEmptyBody_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var content = new StringContent("", System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PostAsync("/api/v1/Products", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateProduct_WithNullManufacturerId_ShouldCreateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var productDto = CreateValidProductDto();
        productDto.ManufacturerId = null; // Should be optional

        var response = await PostAsJsonAsync("/api/v1/Products", productDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        
        // Set the tenant context for querying
        TenantContext.SetCurrentTenantId(1);
        
        // Refresh the DbContext to ensure we get the latest data
        DbContext.ChangeTracker.Clear();
        var createdProduct = await DbContext.Product.FindAsync(result.Data);
        TestAssertions.AssertNotNull(createdProduct);
        Assert.Null(createdProduct!.ManufacturerId);
    }
}