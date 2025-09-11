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

namespace maERP.Server.Tests.Features.Product.Commands;

public class ProductUpdateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    // Test Entity IDs
    private static readonly Guid TestTaxClassId = new("11111111-1111-1111-1111-111111111111");
    private static readonly Guid TestManufacturer1Id = new("22222222-2222-2222-2222-222222222222");
    private static readonly Guid TestManufacturer2Id = new("33333333-3333-3333-3333-333333333333");
    private static readonly Guid TestProduct1Id = new("44444444-4444-4444-4444-444444444444");
    private static readonly Guid TestProduct2Id = new("55555555-5555-5555-5555-555555555555");

    public ProductUpdateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_ProductUpdateCommandTests_{uniqueId}";
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

    private async Task<Guid> SeedTestDataAsync()
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
                    Id = TestTaxClassId,
                    TaxRate = 19.0,
                    TenantId = TenantConstants.TestTenant1Id
                };

                // Check if manufacturers already exist - use IgnoreQueryFilters to see all data
                var existingManufacturer1 = await DbContext.Manufacturer
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(m => m.Id == TestManufacturer1Id);
                var existingManufacturer2 = await DbContext.Manufacturer
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(m => m.Id == TestManufacturer2Id);

                if (existingManufacturer1 == null)
                {
                    var manufacturer1 = new maERP.Domain.Entities.Manufacturer
                    {
                        Id = TestManufacturer1Id,
                        Name = "Test Manufacturer 1",
                        City = "Test City",
                        Country = "Test Country",
                        TenantId = TenantConstants.TestTenant1Id
                    };
                    DbContext.Manufacturer.Add(manufacturer1);
                }

                if (existingManufacturer2 == null)
                {
                    var manufacturer2 = new maERP.Domain.Entities.Manufacturer
                    {
                        Id = TestManufacturer2Id,
                        Name = "Test Manufacturer 2",
                        City = "Test City 2",
                        Country = "Test Country 2",
                        TenantId = TenantConstants.TestTenant1Id
                    };
                    DbContext.Manufacturer.Add(manufacturer2);
                }

                DbContext.TaxClass.Add(taxClass);

                // Save manufacturers and tax class before adding products
                await DbContext.SaveChangesAsync();

                var product1 = new maERP.Domain.Entities.Product
                {
                    Id = TestProduct1Id,
                    Sku = "TEST-UPDATE-001",
                    Name = "Test Product for Update",
                    Description = "Original description",
                    Price = 50.00m,
                    Msrp = 75.00m,
                    Weight = 1.0m,
                    Width = 8.0m,
                    Height = 4.0m,
                    Depth = 12.0m,
                    TaxClassId = TestTaxClassId,
                    ManufacturerId = TestManufacturer1Id,
                    UseOptimized = false,
                    TenantId = TenantConstants.TestTenant1Id
                };

                var product2 = new maERP.Domain.Entities.Product
                {
                    Id = TestProduct2Id,
                    Sku = "TEST-UPDATE-002",
                    Name = "Another Test Product",
                    Description = "Another description",
                    Price = 100.00m,
                    Msrp = 150.00m,
                    TaxClassId = TestTaxClassId,
                    ManufacturerId = TestManufacturer2Id,
                    UseOptimized = false,
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Product.AddRange(product1, product2);
                await DbContext.SaveChangesAsync();

                return TestProduct1Id;
            }

            var existingProduct = await DbContext.Product.FirstOrDefaultAsync(p => p.TenantId == TenantConstants.TestTenant1Id);
            return existingProduct?.Id ?? TestProduct1Id;
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private ProductInputDto CreateUpdateProductDto(Guid id)
    {
        return new ProductInputDto
        {
            Id = id,
            Sku = "TEST-UPDATED-001",
            Name = "Updated Test Product",
            Description = "Updated description",
            Price = 99.99m,
            Msrp = 129.99m,
            Weight = 1.5m,
            Width = 10.0m,
            Height = 5.0m,
            Depth = 15.0m,
            TaxClassId = TestTaxClassId,
            ManufacturerId = TestManufacturer1Id,
            UseOptimized = true
        };
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task UpdateProduct_WithValidData_ShouldReturnOk()
    {
        var productId = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateProductDto(productId);

        var response = await PutAsJsonAsync($"/api/v1/Products/{productId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(productId, result.Data);
    }

    [Fact]
    public async Task UpdateProduct_WithValidData_ShouldUpdateInDatabase()
    {
        var productId = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateProductDto(productId);

        var response = await PutAsJsonAsync($"/api/v1/Products/{productId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Clear the change tracker to force reload from database
        DbContext.ChangeTracker.Clear();

        // Set tenant context for database query
        TenantContext.SetCurrentTenantId(TenantConstants.TestTenant1Id);

        var updatedProduct = await DbContext.Product.FindAsync(productId);
        TestAssertions.AssertNotNull(updatedProduct);
        TestAssertions.AssertEqual(updateDto.Sku, updatedProduct!.Sku);
        TestAssertions.AssertEqual(updateDto.Name, updatedProduct.Name);
        TestAssertions.AssertEqual(updateDto.Description, updatedProduct.Description);
        TestAssertions.AssertEqual(updateDto.Price, updatedProduct.Price);
        TestAssertions.AssertEqual(updateDto.UseOptimized, updatedProduct.UseOptimized);
    }

    [Fact]
    public async Task UpdateProduct_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateProductDto(Guid.NewGuid());

        var nonExistentId = Guid.NewGuid();
        var response = await PutAsJsonAsync($"/api/v1/Products/{nonExistentId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateProduct_WithWrongTenant_ShouldReturnNotFound()
    {
        var productId = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id); // Product belongs to tenant 1, accessing with tenant 2
        var updateDto = CreateUpdateProductDto(productId);

        var response = await PutAsJsonAsync($"/api/v1/Products/{productId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateProduct_WithMissingRequiredFields_ShouldReturnBadRequest()
    {
        var productId = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = new ProductInputDto
        {
            Id = productId,
            // Missing required fields like Sku, Name, Price
            Description = "Updated description"
        };

        var response = await PutAsJsonAsync($"/api/v1/Products/{productId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateProduct_WithDuplicateSku_ShouldReturnBadRequest()
    {
        var productId = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Get the SKU of another product
        var anotherProduct = await DbContext.Product
            .FirstOrDefaultAsync(p => p.TenantId == TenantConstants.TestTenant1Id && p.Id != productId);

        if (anotherProduct != null)
        {
            var updateDto = CreateUpdateProductDto(productId);
            updateDto.Sku = anotherProduct.Sku; // Use existing SKU

            var response = await PutAsJsonAsync($"/api/v1/Products/{productId}", updateDto);

            TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
            var result = await ReadResponseAsync<Result<Guid>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertFalse(result.Succeeded);
            TestAssertions.AssertNotEmpty(result.Messages);
        }
    }

    [Fact]
    public async Task UpdateProduct_WithInvalidTaxClassId_ShouldReturnBadRequest()
    {
        var productId = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateProductDto(productId);
        updateDto.TaxClassId = Guid.NewGuid(); // Non-existent tax class

        var response = await PutAsJsonAsync($"/api/v1/Products/{productId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateProduct_WithInvalidManufacturerId_ShouldReturnBadRequest()
    {
        var productId = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateProductDto(productId);
        updateDto.ManufacturerId = Guid.NewGuid(); // Non-existent manufacturer

        var response = await PutAsJsonAsync($"/api/v1/Products/{productId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateProduct_WithNegativePrice_ShouldReturnBadRequest()
    {
        var productId = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateProductDto(productId);
        updateDto.Price = -10.00m;

        var response = await PutAsJsonAsync($"/api/v1/Products/{productId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateProduct_WithTooLongSku_ShouldReturnBadRequest()
    {
        var productId = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateProductDto(productId);
        updateDto.Sku = new string('A', 256); // Exceeds 255 character limit

        var response = await PutAsJsonAsync($"/api/v1/Products/{productId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);

        // When model validation fails, ASP.NET Core returns a ValidationProblemDetails response
        // instead of our custom Result<int> format
        var responseContent = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertNotNull(responseContent);
        TestAssertions.AssertTrue(responseContent.Contains("Sku") || responseContent.Contains("sku"));
        TestAssertions.AssertTrue(responseContent.Contains("255") || responseContent.Contains("maximum length"));
    }

    [Fact]
    public async Task UpdateProduct_WithOptionalFields_ShouldUpdateSuccessfully()
    {
        var productId = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateProductDto(productId);
        updateDto.NameOptimized = "Updated Optimized Name";
        updateDto.DescriptionOptimized = "Updated Optimized Description";
        updateDto.Ean = "9876543210987";
        updateDto.Asin = "B08UPDATED";

        var response = await PutAsJsonAsync($"/api/v1/Products/{productId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Clear the change tracker to force reload from database
        DbContext.ChangeTracker.Clear();

        // Set tenant context for database query
        TenantContext.SetCurrentTenantId(TenantConstants.TestTenant1Id);

        var updatedProduct = await DbContext.Product.FindAsync(productId);
        TestAssertions.AssertNotNull(updatedProduct);
        TestAssertions.AssertEqual(updateDto.NameOptimized, updatedProduct!.NameOptimized);
        TestAssertions.AssertEqual(updateDto.DescriptionOptimized, updatedProduct.DescriptionOptimized);
        TestAssertions.AssertEqual(updateDto.Ean, updatedProduct.Ean);
        TestAssertions.AssertEqual(updateDto.Asin, updatedProduct.Asin);
    }

    [Fact]
    public async Task UpdateProduct_PartialUpdate_ShouldUpdateOnlyProvidedFields()
    {
        var productId = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Set tenant context for database query
        TenantContext.SetCurrentTenantId(TenantConstants.TestTenant1Id);

        // Get original values
        var originalProduct = await DbContext.Product.AsNoTracking().FirstAsync(p => p.Id == productId);

        var updateDto = new ProductInputDto
        {
            Id = productId,
            Sku = originalProduct.Sku, // Keep original SKU
            Name = "Only Name Updated",
            Price = originalProduct.Price, // Keep original price
            TaxClassId = originalProduct.TaxClassId,
            ManufacturerId = originalProduct.ManufacturerId,
            Description = originalProduct.Description, // Keep original description
            Weight = originalProduct.Weight,
            Width = originalProduct.Width,
            Height = originalProduct.Height,
            Depth = originalProduct.Depth,
            Msrp = originalProduct.Msrp,
            UseOptimized = originalProduct.UseOptimized
        };

        var response = await PutAsJsonAsync($"/api/v1/Products/{productId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Clear the change tracker to force reload from database
        DbContext.ChangeTracker.Clear();

        // Set tenant context for database query
        TenantContext.SetCurrentTenantId(TenantConstants.TestTenant1Id);

        var updatedProduct = await DbContext.Product.FindAsync(productId);
        TestAssertions.AssertNotNull(updatedProduct);
        TestAssertions.AssertEqual("Only Name Updated", updatedProduct!.Name);
        TestAssertions.AssertEqual(originalProduct.Description, updatedProduct.Description); // Should remain unchanged
        TestAssertions.AssertEqual(originalProduct.Price, updatedProduct.Price); // Should remain unchanged
    }

    [Fact]
    public async Task UpdateProduct_WithNullManufacturerId_ShouldUpdateSuccessfully()
    {
        var productId = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateProductDto(productId);
        updateDto.ManufacturerId = null; // Remove manufacturer

        var response = await PutAsJsonAsync($"/api/v1/Products/{productId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Clear the change tracker to force reload from database
        DbContext.ChangeTracker.Clear();

        // Set tenant context for database query
        TenantContext.SetCurrentTenantId(TenantConstants.TestTenant1Id);

        var updatedProduct = await DbContext.Product.FindAsync(productId);
        TestAssertions.AssertNotNull(updatedProduct);
        Assert.Null(updatedProduct!.ManufacturerId);
    }

    [Fact]
    public async Task UpdateProduct_ChangeManufacturer_ShouldUpdateSuccessfully()
    {
        var productId = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Clear change tracker to avoid tracking conflicts
        DbContext.ChangeTracker.Clear();

        // Verify manufacturer 2 exists - use IgnoreQueryFilters to see it
        var manufacturer2 = await DbContext.Manufacturer
            .IgnoreQueryFilters()
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == TestManufacturer2Id && m.TenantId == TenantConstants.TestTenant1Id);
        TestAssertions.AssertNotNull(manufacturer2);

        var updateDto = CreateUpdateProductDto(productId);
        updateDto.ManufacturerId = TestManufacturer2Id; // Change to different manufacturer

        var response = await PutAsJsonAsync($"/api/v1/Products/{productId}", updateDto);

        // Check for validation errors
        if (response.StatusCode != HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Response: {response.StatusCode}, Content: {content}");
        }

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Clear tracking to force reload from database
        DbContext.ChangeTracker.Clear();

        var updatedProduct = await DbContext.Product
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(p => p.Id == productId);
        TestAssertions.AssertNotNull(updatedProduct);
        TestAssertions.AssertEqual(TestManufacturer2Id, updatedProduct!.ManufacturerId);
    }

    [Fact]
    public async Task UpdateProduct_TenantIsolation_ShouldOnlyUpdateInCorrectTenant()
    {
        var productId = await SeedTestDataAsync();

        // Update product in tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateProductDto(productId);
        updateDto.Name = "Updated in Tenant 1";

        var response = await PutAsJsonAsync($"/api/v1/Products/{productId}", updateDto);
        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Clear the change tracker to force reload from database
        DbContext.ChangeTracker.Clear();

        // Verify product was updated in tenant 1
        var productInTenant1 = await DbContext.Product.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.Id == productId && p.TenantId == TenantConstants.TestTenant1Id);
        TestAssertions.AssertNotNull(productInTenant1);
        TestAssertions.AssertEqual("Updated in Tenant 1", productInTenant1!.Name);

        // Verify product in tenant 2 remains unchanged
        var productInTenant2 = await DbContext.Product.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.TenantId == TenantConstants.TestTenant2Id);
        if (productInTenant2 != null)
        {
            TestAssertions.AssertNotEqual("Updated in Tenant 1", productInTenant2.Name);
        }
    }

    [Fact]
    public async Task UpdateProduct_ResponseStructure_ShouldHaveCorrectFormat()
    {
        var productId = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateProductDto(productId);

        var response = await PutAsJsonAsync($"/api/v1/Products/{productId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(productId, result.Data);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task UpdateProduct_WithInvalidJson_ShouldReturnBadRequest()
    {
        var productId = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var invalidJson = "{ invalid json }";
        var content = new StringContent(invalidJson, System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PutAsync($"/api/v1/Products/{productId}", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateProduct_WithMismatchedIds_ShouldReturnBadRequest()
    {
        var productId = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var mismatchedId = Guid.NewGuid();
        var updateDto = CreateUpdateProductDto(mismatchedId); // Different ID in DTO than in URL

        var response = await PutAsJsonAsync($"/api/v1/Products/{productId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Microsoft.AspNetCore.Mvc.ProblemDetails>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertEqual("Invalid Request", result.Title);
        TestAssertions.AssertEqual($"ID in URL ({productId}) must match ID in request body ({mismatchedId})", result.Detail);
    }

    [Fact]
    public async Task UpdateProduct_WithZeroId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateProductDto(Guid.Empty);

        var response = await PutAsJsonAsync($"/api/v1/Products/{Guid.Empty}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }
}