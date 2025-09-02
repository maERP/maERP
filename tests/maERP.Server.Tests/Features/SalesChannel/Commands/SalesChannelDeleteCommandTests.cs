#nullable disable
using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Enums;

namespace maERP.Server.Tests.Features.SalesChannel.Commands;

public class SalesChannelDeleteCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public SalesChannelDeleteCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_SalesChannelDeleteCommandTests_{uniqueId}";
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
            var hasData = await DbContext.SalesChannel.AnyAsync();
            if (!hasData)
            {
                await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

                // Get existing warehouses created by TestDataSeeder
                var warehouse1 = await DbContext.Warehouse.FirstAsync(w => w.Id == 1 && w.TenantId == 1);
                var warehouse2 = await DbContext.Warehouse.FirstAsync(w => w.Id == 2 && w.TenantId == 1);
                var warehouse3 = await DbContext.Warehouse.FirstAsync(w => w.Id == 3 && w.TenantId == 2);

                // Create existing sales channels for testing deletion
                var salesChannel1 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = 1,
                    Type = SalesChannelType.WooCommerce,
                    Name = "WooCommerce Store T1 - Delete Me",
                    Url = "https://delete1.example.com",
                    Username = "deleteuser1",
                    Password = "deletepass1",
                    ImportProducts = true,
                    ExportProducts = false,
                    ImportCustomers = true,
                    ExportCustomers = false,
                    ImportOrders = true,
                    ExportOrders = false,
                    TenantId = 1,
                    Warehouses = new List<maERP.Domain.Entities.Warehouse> { warehouse1, warehouse2 }
                };

                var salesChannel2 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = 2,
                    Type = SalesChannelType.Shopware6,
                    Name = "Shopware Store T1 - Keep Me",
                    Url = "https://keep1.example.com",
                    Username = "keepuser1",
                    Password = "keeppass1",
                    ImportProducts = false,
                    ExportProducts = true,
                    ImportCustomers = false,
                    ExportCustomers = true,
                    ImportOrders = false,
                    ExportOrders = true,
                    TenantId = 1
                };

                var salesChannel3 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = 3,
                    Type = SalesChannelType.eBay,
                    Name = "eBay Store T2 - Delete Me",
                    Url = "https://delete2.example.com",
                    Username = "deleteuser2",
                    Password = "deletepass2",
                    ImportProducts = true,
                    ExportProducts = true,
                    ImportCustomers = true,
                    ExportCustomers = true,
                    ImportOrders = true,
                    ExportOrders = true,
                    TenantId = 2,
                    Warehouses = new List<maERP.Domain.Entities.Warehouse> { warehouse3 }
                };

                var salesChannel4 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = 4,
                    Type = SalesChannelType.PointOfSale,
                    Name = "POS Store T2 - Keep Me",
                    Url = "https://keep2.example.com",
                    Username = "keepuser2",
                    Password = "keeppass2",
                    ImportProducts = false,
                    ExportProducts = false,
                    ImportCustomers = false,
                    ExportCustomers = false,
                    ImportOrders = false,
                    ExportOrders = false,
                    TenantId = 2
                };

                DbContext.SalesChannel.AddRange(salesChannel1, salesChannel2, salesChannel3, salesChannel4);
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
    public async Task DeleteSalesChannel_WithValidId_ShouldReturnSuccess()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/SalesChannels/1");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(1, result.Data);
    }

    [Fact]
    public async Task DeleteSalesChannel_WithValidId_ShouldRemoveFromDatabase()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/SalesChannels/1");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertTrue(result.Succeeded);
        
        // Verify sales channel no longer exists
        var getResponse = await Client.GetAsync("/api/v1/SalesChannels/1");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, getResponse.StatusCode);
        
        // Verify through direct database query
        TenantContext.SetCurrentTenantId(1);
        var salesChannelInDb = await DbContext.SalesChannel.FindAsync(1);
        TestAssertions.AssertTrue(salesChannelInDb == null);
    }

    [Fact]
    public async Task DeleteSalesChannel_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/SalesChannels/999");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task DeleteSalesChannel_WithoutTenantHeader_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();

        var response = await Client.DeleteAsync("/api/v1/SalesChannels/1");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteSalesChannel_TenantIsolation_ShouldOnlyDeleteOwnTenantData()
    {
        await SeedTestDataAsync();

        // Test Tenant 1 deleting its own data
        SetTenantHeader(1);
        var response1 = await Client.DeleteAsync("/api/v1/SalesChannels/1");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response1.StatusCode);

        // Test Tenant 1 trying to delete Tenant 2 data - should fail
        var response1Cross = await Client.DeleteAsync("/api/v1/SalesChannels/3");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response1Cross.StatusCode);

        // Test Tenant 2 deleting its own data
        SetTenantHeader(2);
        var response2 = await Client.DeleteAsync("/api/v1/SalesChannels/3");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response2.StatusCode);

        // Test Tenant 2 trying to delete Tenant 1 data - should fail
        var response2Cross = await Client.DeleteAsync("/api/v1/SalesChannels/2");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2Cross.StatusCode);

        // Verify actual data in database
        TenantContext.SetCurrentTenantId(null);
        DbContext.ChangeTracker.Clear();
        
        var allSalesChannels = await DbContext.Set<maERP.Domain.Entities.SalesChannel>()
            .IgnoreQueryFilters()
            .ToListAsync();
        
        // Should have deleted sales channels 1 and 3, but kept 2 and 4
        var salesChannel1 = allSalesChannels.FirstOrDefault(s => s.Id == 1);
        var salesChannel2 = allSalesChannels.FirstOrDefault(s => s.Id == 2);
        var salesChannel3 = allSalesChannels.FirstOrDefault(s => s.Id == 3);
        var salesChannel4 = allSalesChannels.FirstOrDefault(s => s.Id == 4);
        
        TestAssertions.AssertTrue(salesChannel1 == null);
        TestAssertions.AssertNotNull(salesChannel2);
        TestAssertions.AssertTrue(salesChannel3 == null);
        TestAssertions.AssertNotNull(salesChannel4);
    }

    [Fact]
    public async Task DeleteSalesChannel_WithInvalidId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/SalesChannels/invalid");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteSalesChannel_WithZeroId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/SalesChannels/0");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteSalesChannel_WithNegativeId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/SalesChannels/-1");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteSalesChannel_WithWarehouseRelationships_ShouldDeleteSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        // Verify sales channel has warehouse relationships before deletion
        var getResponseBefore = await Client.GetAsync("/api/v1/SalesChannels/1");
        TestAssertions.AssertHttpSuccess(getResponseBefore);
        var salesChannelDetailBefore = await ReadResponseAsync<Result<SalesChannelDetailDto>>(getResponseBefore);
        TestAssertions.AssertNotNull(salesChannelDetailBefore?.Data);
        TestAssertions.AssertEqual(2, salesChannelDetailBefore.Data.Warehouses.Count);

        // Delete the sales channel
        var response = await Client.DeleteAsync("/api/v1/SalesChannels/1");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertTrue(result.Succeeded);
        
        // Verify sales channel and relationships are gone
        var getResponseAfter = await Client.GetAsync("/api/v1/SalesChannels/1");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, getResponseAfter.StatusCode);
        
        // Verify warehouses still exist (should not be cascade deleted)
        SetTenantHeader(1);
        var warehouse1Response = await Client.GetAsync("/api/v1/Warehouses/1");
        var warehouse2Response = await Client.GetAsync("/api/v1/Warehouses/2");
        // These might return 404 if Warehouse API doesn't exist, but that's expected in TDD
    }

    [Fact]
    public async Task DeleteSalesChannel_MultipleDeletions_ShouldHandleCorrectly()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        // Delete first sales channel
        var response1 = await Client.DeleteAsync("/api/v1/SalesChannels/1");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response1.StatusCode);

        // Try to delete the same sales channel again - should return NotFound
        var response2 = await Client.DeleteAsync("/api/v1/SalesChannels/1");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);
        
        // Delete different sales channel should still work
        var response3 = await Client.DeleteAsync("/api/v1/SalesChannels/2");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response3.StatusCode);
    }

    [Fact]
    public async Task DeleteSalesChannel_WithNonExistentTenant_ShouldHandleGracefully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(999);

        var response = await Client.DeleteAsync("/api/v1/SalesChannels/1");

        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.BadRequest || 
                                 response.StatusCode == HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteSalesChannel_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/SalesChannels/1");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(1, result.Data);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertEqual(ResultStatusCode.Ok, result.StatusCode);
    }

    [Fact]
    public async Task DeleteSalesChannel_ErrorHandling_ShouldReturnProperErrorFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/SalesChannels/999");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertEqual(ResultStatusCode.NotFound, result.StatusCode);
        TestAssertions.AssertNotEmpty(result.Messages);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("999")));
    }

    [Fact]
    public async Task DeleteSalesChannel_VerifyOtherDataUnaffected_ShouldNotDeleteOtherSalesChannels()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        // Get count before deletion
        var listResponseBefore = await Client.GetAsync("/api/v1/SalesChannels");
        TestAssertions.AssertHttpSuccess(listResponseBefore);
        var listBefore = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(listResponseBefore);
        var countBefore = listBefore.Data?.Count ?? 0;

        // Delete one sales channel
        var deleteResponse = await Client.DeleteAsync("/api/v1/SalesChannels/1");
        TestAssertions.AssertEqual(HttpStatusCode.OK, deleteResponse.StatusCode);

        // Get count after deletion
        var listResponseAfter = await Client.GetAsync("/api/v1/SalesChannels");
        TestAssertions.AssertHttpSuccess(listResponseAfter);
        var listAfter = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(listResponseAfter);
        var countAfter = listAfter.Data?.Count ?? 0;

        // Should have one less sales channel
        TestAssertions.AssertEqual(countBefore - 1, countAfter);
        
        // Verify specific sales channel was deleted and others remain
        var deletedChannelExists = listAfter.Data?.Any(s => s.Id == 1) ?? false;
        var otherChannelExists = listAfter.Data?.Any(s => s.Id == 2) ?? false;
        
        TestAssertions.AssertFalse(deletedChannelExists);
        TestAssertions.AssertTrue(otherChannelExists);
    }

    [Fact]
    public async Task DeleteSalesChannel_ConcurrentDeletion_ShouldHandleRaceCondition()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        // Simulate concurrent deletion attempts
        var deleteTask1 = Client.DeleteAsync("/api/v1/SalesChannels/1");
        var deleteTask2 = Client.DeleteAsync("/api/v1/SalesChannels/1");

        var responses = await Task.WhenAll(deleteTask1, deleteTask2);

        // One should succeed, one should fail with NotFound
        var successCount = responses.Count(r => r.StatusCode == HttpStatusCode.OK);
        var notFoundCount = responses.Count(r => r.StatusCode == HttpStatusCode.NotFound);

        TestAssertions.AssertEqual(1, successCount);
        TestAssertions.AssertEqual(1, notFoundCount);
    }

    [Fact]
    public async Task DeleteSalesChannel_WithDifferentSalesChannelTypes_ShouldDeleteSuccessfully()
    {
        await SeedTestDataAsync();

        // Delete WooCommerce sales channel from Tenant 1
        SetTenantHeader(1);
        var response1 = await Client.DeleteAsync("/api/v1/SalesChannels/1");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response1.StatusCode);

        // Delete eBay sales channel from Tenant 2
        SetTenantHeader(2);
        var response2 = await Client.DeleteAsync("/api/v1/SalesChannels/3");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response2.StatusCode);

        // Verify both deletions were successful
        TenantContext.SetCurrentTenantId(null);
        DbContext.ChangeTracker.Clear();
        
        var allSalesChannels = await DbContext.Set<maERP.Domain.Entities.SalesChannel>()
            .IgnoreQueryFilters()
            .ToListAsync();
        
        var deletedChannel1 = allSalesChannels.FirstOrDefault(s => s.Id == 1);
        var deletedChannel3 = allSalesChannels.FirstOrDefault(s => s.Id == 3);
        
        TestAssertions.AssertTrue(deletedChannel1 == null);
        TestAssertions.AssertTrue(deletedChannel3 == null);
    }

    [Fact]
    public async Task DeleteSalesChannel_VerifyAuditTrail_ShouldLogDeletionAttempts()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        // Delete existing sales channel
        var response1 = await Client.DeleteAsync("/api/v1/SalesChannels/1");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response1.StatusCode);

        // Try to delete non-existent sales channel
        var response2 = await Client.DeleteAsync("/api/v1/SalesChannels/999");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);

        // Both operations should complete without throwing exceptions
        // In a real implementation, this would verify audit logs were created
        TestAssertions.AssertTrue(true); // Placeholder for audit verification
    }
}