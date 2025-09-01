using System.Net;
using System.Text.Json;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace maERP.Server.Tests.Features.Product.Commands;

public class ProductDeleteCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public ProductDeleteCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_ProductDeleteCommandTests_{uniqueId}";
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

    private async Task<(int tenant1ProductId, int tenant2ProductId)> SeedTestDataAsync()
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

                var product1 = new maERP.Domain.Entities.Product
                {
                    Id = 1,
                    Sku = "TEST-DELETE-001",
                    Name = "Test Product for Deletion",
                    Description = "Product to be deleted",
                    Price = 50.00m,
                    Msrp = 75.00m,
                    Weight = 1.0m,
                    Width = 8.0m,
                    Height = 4.0m,
                    Depth = 12.0m,
                    TaxClassId = 1,
                    ManufacturerId = 1,
                    UseOptimized = false,
                    TenantId = 1
                };

                var product2 = new maERP.Domain.Entities.Product
                {
                    Id = 2,
                    Sku = "TEST-DELETE-002",
                    Name = "Another Test Product",
                    Description = "Another product for testing",
                    Price = 100.00m,
                    Msrp = 150.00m,
                    TaxClassId = 1,
                    ManufacturerId = 1,
                    UseOptimized = false,
                    TenantId = 1
                };

                var product3 = new maERP.Domain.Entities.Product
                {
                    Id = 3,
                    Sku = "TEST-DELETE-003",
                    Name = "Tenant 2 Product",
                    Description = "Product in tenant 2",
                    Price = 75.00m,
                    Msrp = 100.00m,
                    TaxClassId = 1,
                    ManufacturerId = 1,
                    UseOptimized = false,
                    TenantId = 2
                };

                DbContext.Product.AddRange(product1, product2, product3);
                await DbContext.SaveChangesAsync();

                return (product1.Id, product3.Id);
            }

            var tenant1Product = await DbContext.Product.FirstOrDefaultAsync(p => p.TenantId == 1);
            var tenant2Product = await DbContext.Product.FirstOrDefaultAsync(p => p.TenantId == 2);
            
            return (tenant1Product?.Id ?? 1, tenant2Product?.Id ?? 3);
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
    public async Task DeleteProduct_WithValidId_ShouldReturnNoContent()
    {
        var (productId, _) = await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync($"/api/v1/Products/{productId}");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(productId, result.Data);
    }

    [Fact]
    public async Task DeleteProduct_WithValidId_ShouldRemoveFromDatabase()
    {
        var (productId, _) = await SeedTestDataAsync();
        SetTenantHeader(1);

        // Verify product exists before deletion
        var productBefore = await DbContext.Product.FindAsync(productId);
        TestAssertions.AssertNotNull(productBefore);

        var response = await Client.DeleteAsync($"/api/v1/Products/{productId}");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        // Verify product is deleted from database
        var productAfter = await DbContext.Product.FindAsync(productId);
        Assert.Null(productAfter);
    }

    [Fact]
    public async Task DeleteProduct_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Products/999");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task DeleteProduct_WithWrongTenant_ShouldReturnNotFound()
    {
        var (productId, _) = await SeedTestDataAsync();
        SetTenantHeader(2); // Product belongs to tenant 1, accessing with tenant 2

        var response = await Client.DeleteAsync($"/api/v1/Products/{productId}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);

        // Verify product still exists in database (not deleted by wrong tenant)
        var productStillExists = await DbContext.Product.FindAsync(productId);
        TestAssertions.AssertNotNull(productStillExists);
    }

    [Fact]
    public async Task DeleteProduct_WithoutTenantHeader_ShouldReturnUnauthorized()
    {
        var (productId, _) = await SeedTestDataAsync();

        var response = await Client.DeleteAsync($"/api/v1/Products/{productId}");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);

        // Verify product still exists in database
        var productStillExists = await DbContext.Product.FindAsync(productId);
        TestAssertions.AssertNotNull(productStillExists);
    }

    [Fact]
    public async Task DeleteProduct_WithZeroId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Products/0");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteProduct_WithNegativeId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Products/-1");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteProduct_WithInvalidId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Products/invalid");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteProduct_TwiceWithSameId_ShouldReturnNotFoundSecondTime()
    {
        var (productId, _) = await SeedTestDataAsync();
        SetTenantHeader(1);

        // First deletion should succeed
        var response1 = await Client.DeleteAsync($"/api/v1/Products/{productId}");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response1.StatusCode);

        // Second deletion should return NotFound
        var response2 = await Client.DeleteAsync($"/api/v1/Products/{productId}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);
        
        var result = await ReadResponseAsync<Result<int>>(response2);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task DeleteProduct_TenantIsolation_ShouldOnlyDeleteFromCorrectTenant()
    {
        var (tenant1ProductId, tenant2ProductId) = await SeedTestDataAsync();

        // Delete product in tenant 1
        SetTenantHeader(1);
        var response = await Client.DeleteAsync($"/api/v1/Products/{tenant1ProductId}");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        // Verify product is deleted from tenant 1
        var deletedProduct = await DbContext.Product.FindAsync(tenant1ProductId);
        Assert.Null(deletedProduct);

        // Verify product in tenant 2 still exists
        var tenant2Product = await DbContext.Product.FindAsync(tenant2ProductId);
        TestAssertions.AssertNotNull(tenant2Product);
        TestAssertions.AssertEqual(2, tenant2Product!.TenantId);
    }

    [Fact]
    public async Task DeleteProduct_ResponseStructure_ShouldHaveCorrectFormat()
    {
        var (productId, _) = await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync($"/api/v1/Products/{productId}");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(productId, result.Data);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task DeleteProduct_MultipleProductsInTenant_ShouldDeleteOnlySpecified()
    {
        var (productId1, _) = await SeedTestDataAsync();
        SetTenantHeader(1);

        // Get count of products before deletion
        var productsBeforeCount = await DbContext.Product.CountAsync(p => p.TenantId == 1);

        var response = await Client.DeleteAsync($"/api/v1/Products/{productId1}");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        // Verify only one product was deleted
        var productsAfterCount = await DbContext.Product.CountAsync(p => p.TenantId == 1);
        TestAssertions.AssertEqual(productsBeforeCount - 1, productsAfterCount);

        // Verify the specific product was deleted
        var deletedProduct = await DbContext.Product.FindAsync(productId1);
        Assert.Null(deletedProduct);
    }

    [Fact]
    public async Task DeleteProduct_WithLargeId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Products/2147483647");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task DeleteProduct_ConcurrentDeletion_ShouldHandleGracefully()
    {
        var (productId, _) = await SeedTestDataAsync();
        SetTenantHeader(1);

        // Simulate concurrent deletion by directly deleting from database
        var productToDelete = await DbContext.Product.FindAsync(productId);
        if (productToDelete != null)
        {
            DbContext.Product.Remove(productToDelete);
            await DbContext.SaveChangesAsync();
        }

        // Now try to delete via API - should handle gracefully
        var response = await Client.DeleteAsync($"/api/v1/Products/{productId}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task DeleteProduct_WithValidIdDifferentTenants_ShouldRespectTenantIsolation()
    {
        var (tenant1ProductId, tenant2ProductId) = await SeedTestDataAsync();

        // Try to delete tenant 2 product while authenticated as tenant 1
        SetTenantHeader(1);
        var response1 = await Client.DeleteAsync($"/api/v1/Products/{tenant2ProductId}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response1.StatusCode);

        // Try to delete tenant 1 product while authenticated as tenant 2
        SetTenantHeader(2);
        var response2 = await Client.DeleteAsync($"/api/v1/Products/{tenant1ProductId}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);

        // Verify both products still exist
        var product1 = await DbContext.Product.FindAsync(tenant1ProductId);
        var product2 = await DbContext.Product.FindAsync(tenant2ProductId);
        TestAssertions.AssertNotNull(product1);
        TestAssertions.AssertNotNull(product2);
    }

    [Fact]
    public async Task DeleteProduct_ErrorResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Products/999");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task DeleteProduct_ValidateProductDoesNotExistInListAfterDeletion()
    {
        var (productId, _) = await SeedTestDataAsync();
        SetTenantHeader(1);

        // Delete the product
        var deleteResponse = await Client.DeleteAsync($"/api/v1/Products/{productId}");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        // Try to get the product list and verify deleted product is not there
        var listResponse = await Client.GetAsync("/api/v1/Products");
        TestAssertions.AssertHttpSuccess(listResponse);
        
        var content = await listResponse.Content.ReadAsStringAsync();
        TestAssertions.AssertFalse(content.Contains($"\"Id\":{productId}"));
    }

    [Fact]
    public async Task DeleteProduct_ValidateProductNotFoundInDetailAfterDeletion()
    {
        var (productId, _) = await SeedTestDataAsync();
        SetTenantHeader(1);

        // Delete the product
        var deleteResponse = await Client.DeleteAsync($"/api/v1/Products/{productId}");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        // Try to get the product details - should return NotFound
        var detailResponse = await Client.GetAsync($"/api/v1/Products/{productId}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, detailResponse.StatusCode);
    }
}