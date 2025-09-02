using System.Net;
using System.Text.Json;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace maERP.Server.Tests.Features.Manufacturer.Commands;

public class ManufacturerDeleteCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public ManufacturerDeleteCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_ManufacturerDeleteCommandTests_{uniqueId}";
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
        
        Task.Delay(10).Wait();
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
            var hasData = await DbContext.Manufacturer.AnyAsync();
            if (!hasData)
            {
                await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

                var manufacturer1 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = 1,
                    Name = "Deletable Manufacturer 1",
                    Street = "123 Delete St",
                    City = "Delete City",
                    Country = "Delete Country",
                    TenantId = 1
                };

                var manufacturer2 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = 2,
                    Name = "Deletable Manufacturer 2",
                    City = "Another Delete City",
                    Country = "Delete Country",
                    TenantId = 1
                };

                var manufacturer3Tenant2 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = 3,
                    Name = "Tenant 2 Deletable Manufacturer",
                    City = "T2 Delete City",
                    Country = "T2 Country",
                    TenantId = 2
                };

                var manufacturer4Tenant2 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = 4,
                    Name = "Another T2 Deletable Manufacturer",
                    City = "Another T2 City",
                    Country = "T2 Country",
                    TenantId = 2
                };

                var manufacturerWithProducts = new maERP.Domain.Entities.Manufacturer
                {
                    Id = 5,
                    Name = "Manufacturer With Products",
                    City = "Products City",
                    Country = "Products Country",
                    TenantId = 1
                };

                DbContext.Manufacturer.AddRange(
                    manufacturer1, 
                    manufacturer2, 
                    manufacturer3Tenant2, 
                    manufacturer4Tenant2,
                    manufacturerWithProducts
                );

                var taxClass = new maERP.Domain.Entities.TaxClass
                {
                    Id = 1,
                    TaxRate = 19.0,
                    TenantId = 1
                };

                var product = new maERP.Domain.Entities.Product
                {
                    Id = 1,
                    Sku = "TEST-001",
                    Name = "Test Product",
                    Description = "Product linked to manufacturer",
                    Price = 99.99m,
                    ManufacturerId = 5,
                    TaxClassId = 1,
                    TenantId = 1
                };

                DbContext.TaxClass.Add(taxClass);
                DbContext.Product.Add(product);
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
    public async Task DeleteManufacturer_WithValidId_ShouldReturnNoContent()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Manufacturers/1");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteManufacturer_WithoutTenantHeader_ShouldReturnUnauthorized()
    {
        await SeedTestDataAsync();

        var response = await Client.DeleteAsync("/api/v1/Manufacturers/1");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteManufacturer_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Manufacturers/999");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task DeleteManufacturer_WithWrongTenant_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(2);

        var response = await Client.DeleteAsync("/api/v1/Manufacturers/1");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task DeleteManufacturer_ShouldActuallyRemoveFromDatabase()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var manufacturerBefore = await DbContext.Manufacturer.FindAsync(2);
        TestAssertions.AssertNotNull(manufacturerBefore);

        var response = await Client.DeleteAsync("/api/v1/Manufacturers/2");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        var manufacturerAfter = await DbContext.Manufacturer.FindAsync(2);
        Assert.Null(manufacturerAfter);
    }

    [Fact]
    public async Task DeleteManufacturer_WithAssociatedProducts_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Manufacturers/5");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task DeleteManufacturer_TenantIsolation_ShouldOnlyDeleteOwnTenantData()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Manufacturers/1");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        var deletedManufacturer = await DbContext.Manufacturer.FindAsync(1);
        Assert.Null(deletedManufacturer);

        var tenant2Manufacturer = await DbContext.Manufacturer.FindAsync(3);
        TestAssertions.AssertNotNull(tenant2Manufacturer);
        TestAssertions.AssertEqual("Tenant 2 Deletable Manufacturer", tenant2Manufacturer!.Name);
        TestAssertions.AssertEqual(2, tenant2Manufacturer.TenantId);
    }

    [Fact]
    public async Task DeleteManufacturer_WithTenant2Data_ShouldDeleteCorrectly()
    {
        await SeedTestDataAsync();
        SetTenantHeader(2);

        var response = await Client.DeleteAsync("/api/v1/Manufacturers/3");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        var deletedManufacturer = await DbContext.Manufacturer.FindAsync(3);
        Assert.Null(deletedManufacturer);
    }

    [Fact]
    public async Task DeleteManufacturer_WithNonExistentTenant_ShouldReturnUnauthorized()
    {
        await SeedTestDataAsync();
        SetTenantHeader(999);

        var response = await Client.DeleteAsync("/api/v1/Manufacturers/1");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteManufacturer_WithZeroId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Manufacturers/0");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task DeleteManufacturer_WithNegativeId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Manufacturers/-1");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task DeleteManufacturer_TenantIsolation_ShouldNotAccessOtherTenantManufacturers()
    {
        await SeedTestDataAsync();

        SetTenantHeader(1);
        var response1 = await Client.DeleteAsync("/api/v1/Manufacturers/3");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response1.StatusCode);

        SetTenantHeader(2);
        var response2 = await Client.DeleteAsync("/api/v1/Manufacturers/1");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);

        var manufacturer1 = await DbContext.Manufacturer.FindAsync(1);
        var manufacturer3 = await DbContext.Manufacturer.FindAsync(3);
        TestAssertions.AssertNotNull(manufacturer1);
        TestAssertions.AssertNotNull(manufacturer3);
    }

    [Fact]
    public async Task DeleteManufacturer_MultipleDeletes_ShouldHandleCorrectly()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response1 = await Client.DeleteAsync("/api/v1/Manufacturers/1");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response1.StatusCode);

        var response2 = await Client.DeleteAsync("/api/v1/Manufacturers/2");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response2.StatusCode);

        var manufacturer1 = await DbContext.Manufacturer.FindAsync(1);
        var manufacturer2 = await DbContext.Manufacturer.FindAsync(2);
        Assert.Null(manufacturer1);
        Assert.Null(manufacturer2);
    }

    [Fact]
    public async Task DeleteManufacturer_AlreadyDeleted_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response1 = await Client.DeleteAsync("/api/v1/Manufacturers/1");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response1.StatusCode);

        var response2 = await Client.DeleteAsync("/api/v1/Manufacturers/1");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);
    }

    [Fact]
    public async Task DeleteManufacturer_ShouldNotAffectOtherManufacturers()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var originalCount = await DbContext.Manufacturer.CountAsync(m => m.TenantId == 1);
        TestAssertions.AssertEqual(3, originalCount);

        var response = await Client.DeleteAsync("/api/v1/Manufacturers/1");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        var remainingCount = await DbContext.Manufacturer.CountAsync(m => m.TenantId == 1);
        TestAssertions.AssertEqual(2, remainingCount);

        var manufacturer2 = await DbContext.Manufacturer.FindAsync(2);
        var manufacturer5 = await DbContext.Manufacturer.FindAsync(5);
        TestAssertions.AssertNotNull(manufacturer2);
        TestAssertions.AssertNotNull(manufacturer5);
    }

    [Fact]
    public async Task DeleteManufacturer_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Manufacturers/999");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task DeleteManufacturer_WithInvalidIdFormat_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Manufacturers/invalid");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteManufacturer_WithLargeId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Manufacturers/2147483647");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task DeleteManufacturer_CrossTenantCheck_ShouldMaintainIsolation()
    {
        await SeedTestDataAsync();

        var tenant1CountBefore = await DbContext.Manufacturer.CountAsync(m => m.TenantId == 1);
        var tenant2CountBefore = await DbContext.Manufacturer.CountAsync(m => m.TenantId == 2);

        SetTenantHeader(1);
        var response = await Client.DeleteAsync("/api/v1/Manufacturers/1");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        var tenant1CountAfter = await DbContext.Manufacturer.CountAsync(m => m.TenantId == 1);
        var tenant2CountAfter = await DbContext.Manufacturer.CountAsync(m => m.TenantId == 2);

        TestAssertions.AssertEqual(tenant1CountBefore - 1, tenant1CountAfter);
        TestAssertions.AssertEqual(tenant2CountBefore, tenant2CountAfter);
    }

    [Fact]
    public async Task DeleteManufacturer_WithProductConstraint_ShouldPreserveManufacturer()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var manufacturerBefore = await DbContext.Manufacturer.FindAsync(5);
        TestAssertions.AssertNotNull(manufacturerBefore);

        var response = await Client.DeleteAsync("/api/v1/Manufacturers/5");
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);

        var manufacturerAfter = await DbContext.Manufacturer.FindAsync(5);
        TestAssertions.AssertNotNull(manufacturerAfter);
        TestAssertions.AssertEqual(manufacturerBefore!.Name, manufacturerAfter!.Name);
    }
}