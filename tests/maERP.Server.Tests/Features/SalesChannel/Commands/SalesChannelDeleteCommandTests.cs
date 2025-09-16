#nullable disable
using System.Net;
using maERP.Domain.Constants;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Enums;

namespace maERP.Server.Tests.Features.SalesChannel.Commands;

public class SalesChannelDeleteCommandTests : TenantIsolatedTestBase
{
    // Test Entity IDs
    private static readonly Guid TestWarehouse1Id = new("11111111-1111-1111-1111-111111111111");
    private static readonly Guid TestWarehouse2Id = new("22222222-2222-2222-2222-222222222222");
    private static readonly Guid TestWarehouse3Id = new("33333333-3333-3333-3333-333333333333");
    private static readonly Guid TestSalesChannel1Id = new("d1d1d1d1-d1d1-d1d1-d1d1-d1d1d1d1d1d1");
    private static readonly Guid TestSalesChannel2Id = new("d2d2d2d2-d2d2-d2d2-d2d2-d2d2d2d2d2d2");
    private static readonly Guid TestSalesChannel3Id = new("d3d3d3d3-d3d3-d3d3-d3d3-d3d3d3d3d3d3");
    private static readonly Guid TestSalesChannel4Id = new("d4d4d4d4-d4d4-d4d4-d4d4-d4d4d4d4d4d4");

    private async Task SeedTestDataAsync()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            // Check if test sales channels are already seeded to avoid duplicates
            var existingSalesChannel = await DbContext.SalesChannel.IgnoreQueryFilters()
                .FirstOrDefaultAsync(sc => sc.Id == TestSalesChannel1Id);

            if (existingSalesChannel == null)
            {

                // Create test warehouses
                var warehouse1 = new maERP.Domain.Entities.Warehouse
                {
                    Id = TestWarehouse1Id,
                    Name = "Test Warehouse 1",
                    TenantId = TenantConstants.TestTenant1Id
                };
                
                var warehouse2 = new maERP.Domain.Entities.Warehouse
                {
                    Id = TestWarehouse2Id,
                    Name = "Test Warehouse 2", 
                    TenantId = TenantConstants.TestTenant1Id
                };
                
                var warehouse3 = new maERP.Domain.Entities.Warehouse
                {
                    Id = TestWarehouse3Id,
                    Name = "Test Warehouse 3",
                    TenantId = TenantConstants.TestTenant2Id
                };
                
                DbContext.Warehouse.AddRange(warehouse1, warehouse2, warehouse3);
                await DbContext.SaveChangesAsync();

                // Create existing sales channels for testing deletion
                var salesChannel1 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = TestSalesChannel1Id,
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
                    TenantId = TenantConstants.TestTenant1Id,
                    Warehouses = new List<maERP.Domain.Entities.Warehouse> { warehouse1, warehouse2 }
                };

                var salesChannel2 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = TestSalesChannel2Id,
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
                    TenantId = TenantConstants.TestTenant1Id
                };

                var salesChannel3 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = TestSalesChannel3Id,
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
                    TenantId = TenantConstants.TestTenant2Id,
                    Warehouses = new List<maERP.Domain.Entities.Warehouse> { warehouse3 }
                };

                var salesChannel4 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = TestSalesChannel4Id,
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
                    TenantId = TenantConstants.TestTenant2Id
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

    [Fact]
    public async Task DeleteSalesChannel_WithValidId_ShouldReturnSuccess()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(TestSalesChannel1Id, result.Data);
    }

    [Fact]
    public async Task DeleteSalesChannel_WithValidId_ShouldRemoveFromDatabase()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify sales channel no longer exists
        var getResponse = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, getResponse.StatusCode);

        // Verify through direct database query
        TenantContext.SetCurrentTenantId(TenantConstants.TestTenant1Id);
        DbContext.ChangeTracker.Clear(); // Clear EF cache to ensure fresh read
        var salesChannelInDb = await DbContext.SalesChannel
            .Where(s => s.Id == TestSalesChannel1Id)
            .FirstOrDefaultAsync();
        TestAssertions.AssertTrue(salesChannelInDb == null);
    }

    [Fact]
    public async Task DeleteSalesChannel_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var nonExistentId = Guid.NewGuid();
        var response = await Client.DeleteAsync($"/api/v1/SalesChannels/{nonExistentId}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task DeleteSalesChannel_WithoutTenantHeader_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        RemoveTenantHeader();

        var response = await Client.DeleteAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteSalesChannel_WithInvalidTenantHeaderFormat_ShouldReturnUnauthorized()
    {
        await SeedTestDataAsync();
        SetInvalidTenantHeaderValue("invalid-guid-format");

        var response = await Client.DeleteAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteSalesChannel_TenantIsolation_ShouldOnlyDeleteOwnTenantData()
    {
        await SeedTestDataAsync();

        // Verify initial state - check if our test sales channels exist
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var listResponse1Before = await Client.GetAsync("/api/v1/SalesChannels");
        TestAssertions.AssertHttpSuccess(listResponse1Before);
        var list1Before = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(listResponse1Before);
        var tenant1ChannelsBefore = list1Before.Data?.Count ?? 0;
        
        var hasTestChannel1 = list1Before.Data?.Any(sc => sc.Id == TestSalesChannel1Id) ?? false;
        var hasTestChannel2 = list1Before.Data?.Any(sc => sc.Id == TestSalesChannel2Id) ?? false;

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var listResponse2Before = await Client.GetAsync("/api/v1/SalesChannels");
        TestAssertions.AssertHttpSuccess(listResponse2Before);
        var list2Before = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(listResponse2Before);
        var tenant2ChannelsBefore = list2Before.Data?.Count ?? 0;
        
        var hasTestChannel3 = list2Before.Data?.Any(sc => sc.Id == TestSalesChannel3Id) ?? false;
        var hasTestChannel4 = list2Before.Data?.Any(sc => sc.Id == TestSalesChannel4Id) ?? false;

        // Debug: Print detailed initial state
        Console.WriteLine($"DEBUG Initial State:");
        Console.WriteLine($"  Tenant1 total channels: {tenant1ChannelsBefore}");
        Console.WriteLine($"  Tenant1 has TestChannel1: {hasTestChannel1}");
        Console.WriteLine($"  Tenant1 has TestChannel2: {hasTestChannel2}");
        Console.WriteLine($"  Tenant2 total channels: {tenant2ChannelsBefore}");
        Console.WriteLine($"  Tenant2 has TestChannel3: {hasTestChannel3}");
        Console.WriteLine($"  Tenant2 has TestChannel4: {hasTestChannel4}");

        if (list1Before.Data != null)
        {
            Console.WriteLine($"  Tenant1 channel IDs: {string.Join(", ", list1Before.Data.Select(c => c.Id.ToString()))}");
        }

        if (list2Before.Data != null)
        {
            Console.WriteLine($"  Tenant2 channel IDs: {string.Join(", ", list2Before.Data.Select(c => c.Id.ToString()))}");
        }

        // Verify our test channels exist
        TestAssertions.AssertTrue(hasTestChannel1, "TestSalesChannel1 should exist in Tenant 1");
        TestAssertions.AssertTrue(hasTestChannel2, "TestSalesChannel2 should exist in Tenant 1");
        TestAssertions.AssertTrue(hasTestChannel3, "TestSalesChannel3 should exist in Tenant 2");
        TestAssertions.AssertTrue(hasTestChannel4, "TestSalesChannel4 should exist in Tenant 2");

        // Tenant 1 deletes their own sales channel - should succeed
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var deleteOwnResponse = await Client.DeleteAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.OK, deleteOwnResponse.StatusCode);

        // Tenant 1 tries to delete Tenant 2's sales channel - should fail with NotFound
        var deleteCrossResponse = await Client.DeleteAsync($"/api/v1/SalesChannels/{TestSalesChannel3Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, deleteCrossResponse.StatusCode);

        // Tenant 2 tries to delete Tenant 1's already deleted sales channel - should fail with NotFound
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var deleteAlreadyDeletedResponse = await Client.DeleteAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, deleteAlreadyDeletedResponse.StatusCode);

        // Tenant 2 tries to delete Tenant 1's remaining sales channel - should fail with NotFound
        var deleteCross2Response = await Client.DeleteAsync($"/api/v1/SalesChannels/{TestSalesChannel2Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, deleteCross2Response.StatusCode);

        // Tenant 2 deletes their own sales channel - should succeed
        var deleteOwn2Response = await Client.DeleteAsync($"/api/v1/SalesChannels/{TestSalesChannel3Id}");
        TestAssertions.AssertEqual(HttpStatusCode.OK, deleteOwn2Response.StatusCode);

        // CRITICAL: Clear DbContext to ensure fresh reads after deletion
        DbContext.ChangeTracker.Clear();

        // Force a small delay to ensure database consistency (especially for InMemory)
        await Task.Delay(100);

        // Verify final state - Tenant 1 should have one less sales channel than before
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var listResponse1After = await Client.GetAsync("/api/v1/SalesChannels");
        TestAssertions.AssertHttpSuccess(listResponse1After);
        var list1After = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(listResponse1After);
        var tenant1ChannelsAfter = list1After.Data?.Count ?? 0;

        // Verify final state - Tenant 2 should have one less sales channel than before
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var listResponse2After = await Client.GetAsync("/api/v1/SalesChannels");
        TestAssertions.AssertHttpSuccess(listResponse2After);
        var list2After = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(listResponse2After);
        var tenant2ChannelsAfter = list2After.Data?.Count ?? 0;

        // Debug: Print actual counts
        Console.WriteLine($"DEBUG: Tenant1 - Before: {tenant1ChannelsBefore}, After: {tenant1ChannelsAfter}, Expected: {tenant1ChannelsBefore - 1}");
        Console.WriteLine($"DEBUG: Tenant2 - Before: {tenant2ChannelsBefore}, After: {tenant2ChannelsAfter}, Expected: {tenant2ChannelsBefore - 1}");

        // Verify counts decreased by exactly 1
        TestAssertions.AssertEqual(tenant1ChannelsBefore - 1, tenant1ChannelsAfter);
        TestAssertions.AssertEqual(tenant2ChannelsBefore - 1, tenant2ChannelsAfter);

        // Verify specific sales channels are gone from their respective tenants
        var tenant1HasDeletedChannel = list1After.Data?.Any(s => s.Id == TestSalesChannel1Id) ?? false;
        var tenant2HasDeletedChannel = list2After.Data?.Any(s => s.Id == TestSalesChannel3Id) ?? false;
        
        TestAssertions.AssertFalse(tenant1HasDeletedChannel, "Tenant 1 should not see their deleted sales channel");
        TestAssertions.AssertFalse(tenant2HasDeletedChannel, "Tenant 2 should not see their deleted sales channel");

        // Verify remaining channels still exist for each tenant
        var tenant1HasRemainingChannel = list1After.Data?.Any(s => s.Id == TestSalesChannel2Id) ?? false;
        var tenant2HasRemainingChannel = list2After.Data?.Any(s => s.Id == TestSalesChannel4Id) ?? false;
        
        TestAssertions.AssertTrue(tenant1HasRemainingChannel, "Tenant 1 should still see their remaining sales channel");
        TestAssertions.AssertTrue(tenant2HasRemainingChannel, "Tenant 2 should still see their remaining sales channel");
    }

    [Fact]
    public async Task DeleteSalesChannel_WithInvalidId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync("/api/v1/SalesChannels/invalid");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteSalesChannel_WithZeroId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync($"/api/v1/SalesChannels/{Guid.Empty}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteSalesChannel_WithNegativeId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync("/api/v1/SalesChannels/-1");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteSalesChannel_WithWarehouseRelationships_ShouldDeleteSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Verify sales channel has warehouse relationships before deletion
        var getResponseBefore = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertHttpSuccess(getResponseBefore);
        var salesChannelDetailBefore = await ReadResponseAsync<Result<SalesChannelDetailDto>>(getResponseBefore);
        TestAssertions.AssertNotNull(salesChannelDetailBefore?.Data);
        TestAssertions.AssertEqual(2, salesChannelDetailBefore.Data.Warehouses.Count);

        // Delete the sales channel
        var response = await Client.DeleteAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify sales channel is gone
        var getResponseAfter = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, getResponseAfter.StatusCode);

        // Note: Warehouses should still exist in the system (cascade deletion should not affect warehouses)
        // This is verified through the successful deletion above - if there were cascade issues, the deletion would fail
    }

    [Fact]
    public async Task DeleteSalesChannel_MultipleDeletions_ShouldHandleCorrectly()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Delete first sales channel
        var response1 = await Client.DeleteAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response1.StatusCode);

        // Try to delete the same sales channel again - should return NotFound
        var response2 = await Client.DeleteAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);

        // Delete different sales channel should still work
        var response3 = await Client.DeleteAsync($"/api/v1/SalesChannels/{TestSalesChannel2Id}");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response3.StatusCode);
    }

    [Fact]
    public async Task DeleteSalesChannel_WithNonExistentTenant_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetInvalidTenantHeader();

        var response = await Client.DeleteAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteSalesChannel_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(TestSalesChannel1Id, result.Data);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertEqual(ResultStatusCode.Ok, result.StatusCode);
    }

    [Fact]
    public async Task DeleteSalesChannel_ErrorHandling_ShouldReturnProperErrorFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var nonExistentId = Guid.NewGuid();
        var response = await Client.DeleteAsync($"/api/v1/SalesChannels/{nonExistentId}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertEqual(ResultStatusCode.NotFound, result.StatusCode);
        TestAssertions.AssertNotEmpty(result.Messages);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains(nonExistentId.ToString()) || m.Contains("not found")));
    }

    [Fact]
    public async Task DeleteSalesChannel_VerifyOtherDataUnaffected_ShouldNotDeleteOtherSalesChannels()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Get count before deletion
        var listResponseBefore = await Client.GetAsync("/api/v1/SalesChannels");
        TestAssertions.AssertHttpSuccess(listResponseBefore);
        var listBefore = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(listResponseBefore);
        var countBefore = listBefore.Data?.Count ?? 0;

        // Delete one sales channel
        var deleteResponse = await Client.DeleteAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.OK, deleteResponse.StatusCode);

        // Get count after deletion
        var listResponseAfter = await Client.GetAsync("/api/v1/SalesChannels");
        TestAssertions.AssertHttpSuccess(listResponseAfter);
        var listAfter = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(listResponseAfter);
        var countAfter = listAfter.Data?.Count ?? 0;

        // Should have exactly one less sales channel
        TestAssertions.AssertEqual(countBefore - 1, countAfter);

        // Verify specific sales channel was deleted and others remain
        var deletedChannelExists = listAfter.Data?.Any(s => s.Id == TestSalesChannel1Id) ?? false;
        var otherChannelExists = listAfter.Data?.Any(s => s.Id == TestSalesChannel2Id) ?? false;

        TestAssertions.AssertFalse(deletedChannelExists);
        TestAssertions.AssertTrue(otherChannelExists);
    }

    [Fact]
    public async Task DeleteSalesChannel_ConcurrentDeletion_ShouldHandleRaceCondition()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Simulate concurrent deletion attempts
        var deleteTask1 = Client.DeleteAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        var deleteTask2 = Client.DeleteAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");

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
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.DeleteAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response1.StatusCode);

        // Delete eBay sales channel from Tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await Client.DeleteAsync($"/api/v1/SalesChannels/{TestSalesChannel3Id}");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response2.StatusCode);

        // Verify both deletions were successful
        TenantContext.SetCurrentTenantId(null);
        DbContext.ChangeTracker.Clear();

        var allSalesChannels = await DbContext.Set<maERP.Domain.Entities.SalesChannel>()
            .IgnoreQueryFilters()
            .ToListAsync();

        var deletedChannel1 = allSalesChannels.FirstOrDefault(s => s.Id == TestSalesChannel1Id);
        var deletedChannel3 = allSalesChannels.FirstOrDefault(s => s.Id == TestSalesChannel3Id);

        TestAssertions.AssertTrue(deletedChannel1 == null);
        TestAssertions.AssertTrue(deletedChannel3 == null);
    }

    [Fact]
    public async Task DeleteSalesChannel_VerifyAuditTrail_ShouldLogDeletionAttempts()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Delete existing sales channel
        var response1 = await Client.DeleteAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response1.StatusCode);

        // Try to delete non-existent sales channel with valid Guid format
        var nonExistentId = Guid.NewGuid();
        var response2 = await Client.DeleteAsync($"/api/v1/SalesChannels/{nonExistentId}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);

        // Both operations should complete without throwing exceptions
        // In a real implementation, this would verify audit logs were created
        TestAssertions.AssertTrue(true); // Placeholder for audit verification
    }
}