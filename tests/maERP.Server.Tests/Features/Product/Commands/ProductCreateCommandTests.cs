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
        
        // Force a small delay to ensure header is set properly
        Task.Delay(10).Wait();
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

                // Seed data for Tenant 1
                var taxClass1 = new maERP.Domain.Entities.TaxClass
                {
                    Id = 1,
                    TaxRate = 19.0,
                    TenantId = 1
                };

                var manufacturer1 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = 1,
                    Name = "Test Manufacturer T1",
                    City = "Test City",
                    Country = "Test Country",
                    TenantId = 1
                };

                // Seed data for Tenant 2
                var taxClass2 = new maERP.Domain.Entities.TaxClass
                {
                    Id = 2,
                    TaxRate = 19.0,
                    TenantId = 2
                };

                var manufacturer2 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = 2,
                    Name = "Test Manufacturer T2",
                    City = "Test City",
                    Country = "Test Country",
                    TenantId = 2
                };

                DbContext.TaxClass.Add(taxClass1);
                DbContext.Manufacturer.Add(manufacturer1);
                DbContext.TaxClass.Add(taxClass2);
                DbContext.Manufacturer.Add(manufacturer2);
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
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data > 0);
        
        // Verify through API that product exists
        var getResponse = await Client.GetAsync($"/api/v1/Products/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var productDetail = await ReadResponseAsync<Result<ProductDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(productDetail?.Data);
        TestAssertions.AssertEqual(productDto.Sku, productDetail!.Data.Sku);
        TestAssertions.AssertEqual(productDto.Name, productDetail.Data.Name);
        TestAssertions.AssertEqual(productDto.Price, productDetail.Data.Price);
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
    public async Task CreateProduct_WithoutTenantHeader_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        var productDto = CreateValidProductDto();

        var response = await PostAsJsonAsync("/api/v1/Products", productDto);

        // The validator returns BadRequest when tenant context is not set properly
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
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
        // ASP.NET Core model validation returns different format than Result<int>
        var responseContent = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(responseContent.Contains("validation errors") || responseContent.Contains("Sku"));
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
        // ASP.NET Core model validation returns different format than Result<int>
        var responseContent = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(responseContent.Contains("validation errors") || responseContent.Contains("Name"));
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
        TestAssertions.AssertTrue(result.Data > 0);
        
        // Verify through API that product exists with optional fields
        var getResponse = await Client.GetAsync($"/api/v1/Products/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var productDetail = await ReadResponseAsync<Result<ProductDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(productDetail?.Data);
        TestAssertions.AssertEqual(productDto.NameOptimized, productDetail!.Data.NameOptimized);
        TestAssertions.AssertEqual(productDto.Ean, productDetail.Data.Ean);
        TestAssertions.AssertEqual(productDto.Asin, productDetail.Data.Asin);
        TestAssertions.AssertTrue(productDetail.Data.UseOptimized);
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

        // Create unique products for each tenant to avoid conflicts
        var product1Dto = CreateValidProductDto();
        product1Dto.Sku = $"TENANT1-{Guid.NewGuid():N}";
        product1Dto.Name = "Product for Tenant 1";
        product1Dto.TaxClassId = 1;
        product1Dto.ManufacturerId = 1;
        
        var product2Dto = CreateValidProductDto();
        product2Dto.Sku = $"TENANT2-{Guid.NewGuid():N}";
        product2Dto.Name = "Product for Tenant 2";
        product2Dto.TaxClassId = 2;
        product2Dto.ManufacturerId = 2;

        // Create product in tenant 1
        SetTenantHeader(1);
        var createResponse1 = await PostAsJsonAsync("/api/v1/Products", product1Dto);
        if (createResponse1.StatusCode != HttpStatusCode.Created)
        {
            var errorContent1 = await createResponse1.Content.ReadAsStringAsync();
            TestAssertions.AssertTrue(false, 
                $"Failed to create product for tenant 1. Expected: Created, Got: {createResponse1.StatusCode}, Error: {errorContent1}");
        }
        
        // Create product in tenant 2
        SetTenantHeader(2);
        var createResponse2 = await PostAsJsonAsync("/api/v1/Products", product2Dto);
        if (createResponse2.StatusCode != HttpStatusCode.Created)
        {
            var errorContent2 = await createResponse2.Content.ReadAsStringAsync();
            TestAssertions.AssertTrue(false, 
                $"Failed to create product for tenant 2. Expected: Created, Got: {createResponse2.StatusCode}, Error: {errorContent2}");
        }
        
        TenantContext.SetCurrentTenantId(null); // Clear tenant filter to see all products
        DbContext.ChangeTracker.Clear();
        
        // Try to query without any filters to see everything
        var allProductsQuery = DbContext.Set<Domain.Entities.Product>()
            .IgnoreQueryFilters(); // Ignore all query filters
        var allProducts = await allProductsQuery.ToListAsync();
        
        
        // Check if both products were created with correct tenant IDs
        var product1InDb = allProducts.FirstOrDefault(p => p.Sku.StartsWith("TENANT1-"));
        var product2InDb = allProducts.FirstOrDefault(p => p.Sku.StartsWith("TENANT2-"));
        
        if (product1InDb == null || product2InDb == null)
        {
            TestAssertions.AssertTrue(false, $"Products not created properly.");
        }
        
        TestAssertions.AssertEqual(1, product1InDb!.TenantId);
        TestAssertions.AssertEqual(2, product2InDb!.TenantId);

        // Verify tenant 1 sees its product - use fresh client to avoid header conflicts
        using var tenant1Client = Factory.CreateClient();
        tenant1Client.DefaultRequestHeaders.Add("X-Tenant-Id", "1");
        
        var listResponse1 = await tenant1Client.GetAsync("/api/v1/Products?t=" + DateTimeOffset.UtcNow.Ticks);
        TestAssertions.AssertHttpSuccess(listResponse1);
        var list1 = await ReadResponseAsync<PaginatedResult<ProductListDto>>(listResponse1);
        var tenant1HasProduct = list1.Data?.Any(p => p.Sku.StartsWith("TENANT1-")) ?? false;
        var tenant1SeesOtherProduct = list1.Data?.Any(p => p.Sku.StartsWith("TENANT2-")) ?? false;
        
        TestAssertions.AssertTrue(tenant1HasProduct, 
            $"Tenant 1 should see its own product. Found products: {string.Join(", ", list1.Data?.Select(p => p.Sku) ?? new string[0])}");
        TestAssertions.AssertFalse(tenant1SeesOtherProduct, 
            "Tenant 1 should not see Tenant 2's products");

        // Verify tenant 2 sees its product - use fresh client to avoid header conflicts
        using var tenant2Client = Factory.CreateClient();
        tenant2Client.DefaultRequestHeaders.Add("X-Tenant-Id", "2");
        
        var listResponse2 = await tenant2Client.GetAsync("/api/v1/Products?t=" + DateTimeOffset.UtcNow.Ticks);
        TestAssertions.AssertHttpSuccess(listResponse2);
        var list2 = await ReadResponseAsync<PaginatedResult<ProductListDto>>(listResponse2);
        var tenant2HasProduct = list2.Data?.Any(p => p.Sku.StartsWith("TENANT2-")) ?? false;
        var tenant2SeesOtherProduct = list2.Data?.Any(p => p.Sku.StartsWith("TENANT1-")) ?? false;
        
        TestAssertions.AssertTrue(tenant2HasProduct, 
            $"Tenant 2 should see its own product. Found products: {string.Join(", ", list2.Data?.Select(p => p.Sku) ?? new string[0])}");
        TestAssertions.AssertFalse(tenant2SeesOtherProduct, 
            "Tenant 2 should not see Tenant 1's products");
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