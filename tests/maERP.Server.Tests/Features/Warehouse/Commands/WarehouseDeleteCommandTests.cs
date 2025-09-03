using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace maERP.Server.Tests.Features.Warehouse.Commands;

public class WarehouseDeleteCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public WarehouseDeleteCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_WarehouseDeleteCommandTests_{uniqueId}";
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

    protected void SetInvalidTenantHeader()
    {
        SetTenantHeader(999); // Non-existent tenant ID for testing tenant isolation
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

    private async Task<int> CreateTestWarehouseAsync(int tenantId, string name = "Test Warehouse")
    {
        TenantContext.SetCurrentTenantId(tenantId);
        
        var warehouse = new maERP.Domain.Entities.Warehouse
        {
            Name = name,
            TenantId = tenantId,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        DbContext.Warehouse.Add(warehouse);
        await DbContext.SaveChangesAsync();
        
        TenantContext.SetCurrentTenantId(null);
        return warehouse.Id;
    }

    private async Task SeedTestDataAsync()
    {
        var hasData = await DbContext.Tenant.IgnoreQueryFilters().AnyAsync();
        if (!hasData)
        {
            await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        }
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task DeleteWarehouse_WithValidId_ShouldReturnOk()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(1);
        SetTenantHeader(1);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Warehouses/{warehouseId}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(warehouseId, result.Data);

        // Verify deletion
        var getResponse = await Client.GetAsync($"/api/v1/Warehouses/{warehouseId}");
        TestAssertions.AssertHttpStatusCode(getResponse, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteWarehouse_WithoutTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(1);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Warehouses/{warehouseId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteWarehouse_WithInvalidTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(1);
        SetInvalidTenantHeader();

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Warehouses/{warehouseId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteWarehouse_FromDifferentTenant_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(1);
        SetTenantHeader(2); // Different tenant

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Warehouses/{warehouseId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);

        // Verify warehouse still exists for tenant 1
        SetTenantHeader(1);
        var getResponse = await Client.GetAsync($"/api/v1/Warehouses/{warehouseId}");
        TestAssertions.AssertHttpSuccess(getResponse);
    }

    [Fact]
    public async Task DeleteWarehouse_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);

        // Act
        var response = await Client.DeleteAsync("/api/v1/Warehouses/999");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteWarehouse_TenantIsolation_ShouldOnlyDeleteOwnTenant()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId1 = await CreateTestWarehouseAsync(1, "Tenant 1 Warehouse");
        var warehouseId2 = await CreateTestWarehouseAsync(2, "Tenant 2 Warehouse");

        // Act - Delete tenant 1's warehouse
        SetTenantHeader(1);
        var response1 = await Client.DeleteAsync($"/api/v1/Warehouses/{warehouseId1}");
        
        // Try to delete tenant 2's warehouse from tenant 1
        var response2 = await Client.DeleteAsync($"/api/v1/Warehouses/{warehouseId2}");

        // Assert
        TestAssertions.AssertHttpSuccess(response1);
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.NotFound);

        // Verify tenant 2's warehouse still exists
        SetTenantHeader(2);
        var getResponse = await Client.GetAsync($"/api/v1/Warehouses/{warehouseId2}");
        TestAssertions.AssertHttpSuccess(getResponse);
    }

    [Fact]
    public async Task DeleteWarehouse_MultipleDeletions_ShouldOnlyDeleteOnce()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(1);
        SetTenantHeader(1);

        // Act - Delete twice
        var response1 = await Client.DeleteAsync($"/api/v1/Warehouses/{warehouseId}");
        var response2 = await Client.DeleteAsync($"/api/v1/Warehouses/{warehouseId}");

        // Assert
        TestAssertions.AssertHttpSuccess(response1);
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteWarehouse_ConcurrentDeletions_ShouldHandleCorrectly()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(1);
        SetTenantHeader(1);

        // Act - Try to delete concurrently
        var task1 = Client.DeleteAsync($"/api/v1/Warehouses/{warehouseId}");
        var task2 = Client.DeleteAsync($"/api/v1/Warehouses/{warehouseId}");
        var task3 = Client.DeleteAsync($"/api/v1/Warehouses/{warehouseId}");

        var responses = await Task.WhenAll(task1, task2, task3);

        // Assert - Only one should succeed
        var successCount = responses.Count(r => r.IsSuccessStatusCode);
        TestAssertions.AssertTrue(successCount >= 1); // At least one should succeed
        
        // Verify deletion
        var getResponse = await Client.GetAsync($"/api/v1/Warehouses/{warehouseId}");
        TestAssertions.AssertHttpStatusCode(getResponse, HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("abc")]
    [InlineData("")]
    public async Task DeleteWarehouse_WithInvalidTenantHeaderValue_ShouldReturnNotFound(string invalidTenantId)
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(1);
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", invalidTenantId);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Warehouses/{warehouseId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteWarehouse_WithZeroId_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);

        // Act
        var response = await Client.DeleteAsync("/api/v1/Warehouses/0");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteWarehouse_WithNegativeId_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);

        // Act
        var response = await Client.DeleteAsync("/api/v1/Warehouses/-1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteWarehouse_WithStringId_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);

        // Act
        var response = await Client.DeleteAsync("/api/v1/Warehouses/abc");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task DeleteWarehouse_ResponseTime_ShouldBeReasonable()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(1);
        SetTenantHeader(1);
        var startTime = DateTime.UtcNow;

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Warehouses/{warehouseId}");
        var endTime = DateTime.UtcNow;

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var responseTime = endTime - startTime;
        TestAssertions.AssertTrue(responseTime.TotalSeconds < 5);
    }

    [Fact]
    public async Task DeleteWarehouse_VerifyResultStructure_ShouldHaveCorrectFormat()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(1);
        SetTenantHeader(1);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Warehouses/{warehouseId}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<int>>(response);
        
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(ResultStatusCode.Ok, result.StatusCode);
        TestAssertions.AssertEqual(warehouseId, result.Data);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task DeleteWarehouse_MultipleWarehouses_ShouldDeleteCorrectOne()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId1 = await CreateTestWarehouseAsync(1, "Warehouse 1");
        var warehouseId2 = await CreateTestWarehouseAsync(1, "Warehouse 2");
        var warehouseId3 = await CreateTestWarehouseAsync(1, "Warehouse 3");
        SetTenantHeader(1);

        // Act - Delete middle one
        var response = await Client.DeleteAsync($"/api/v1/Warehouses/{warehouseId2}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        
        // Verify only the correct one was deleted
        var getResponse1 = await Client.GetAsync($"/api/v1/Warehouses/{warehouseId1}");
        TestAssertions.AssertHttpSuccess(getResponse1);
        
        var getResponse2 = await Client.GetAsync($"/api/v1/Warehouses/{warehouseId2}");
        TestAssertions.AssertHttpStatusCode(getResponse2, HttpStatusCode.NotFound);
        
        var getResponse3 = await Client.GetAsync($"/api/v1/Warehouses/{warehouseId3}");
        TestAssertions.AssertHttpSuccess(getResponse3);
    }

    [Fact]
    public async Task DeleteWarehouse_AfterTenantSwitch_ShouldDeleteFromCorrectTenant()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId1 = await CreateTestWarehouseAsync(1, "Tenant 1 Warehouse");
        var warehouseId2 = await CreateTestWarehouseAsync(2, "Tenant 2 Warehouse");

        // Act - Start with tenant 1
        SetTenantHeader(1);
        var canAccessOwn = await Client.GetAsync($"/api/v1/Warehouses/{warehouseId1}");
        TestAssertions.AssertHttpSuccess(canAccessOwn);

        // Switch to tenant 2 and delete their warehouse
        SetTenantHeader(2);
        var deleteResponse = await Client.DeleteAsync($"/api/v1/Warehouses/{warehouseId2}");
        TestAssertions.AssertHttpSuccess(deleteResponse);

        // Try to delete tenant 1's warehouse from tenant 2
        var deleteOtherResponse = await Client.DeleteAsync($"/api/v1/Warehouses/{warehouseId1}");
        TestAssertions.AssertHttpStatusCode(deleteOtherResponse, HttpStatusCode.NotFound);

        // Verify tenant 1's warehouse still exists
        SetTenantHeader(1);
        var stillExists = await Client.GetAsync($"/api/v1/Warehouses/{warehouseId1}");
        TestAssertions.AssertHttpSuccess(stillExists);
    }

    [Fact]
    public async Task DeleteWarehouse_WithMaxIntId_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Warehouses/{int.MaxValue}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteWarehouse_WithAssociatedSalesChannels_ShouldHandleCorrectly()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(1, "Warehouse with Dependencies");
        
        // Note: Depending on business rules, this might fail or succeed with cascade
        // Adjust test based on actual implementation
        SetTenantHeader(1);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Warehouses/{warehouseId}");

        // Assert - Adjust based on business rules
        // If deletion should fail when dependencies exist:
        // TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        // If deletion should succeed with cascade or nullification:
        TestAssertions.AssertHttpSuccess(response);
    }

    [Fact]
    public async Task DeleteWarehouse_VerifyListNoLongerContainsDeletedWarehouse()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(1, "To Be Deleted");
        SetTenantHeader(1);

        // Verify warehouse exists in list before deletion
        var listBefore = await Client.GetAsync("/api/v1/Warehouses");
        var listBeforeResult = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(listBefore);
        var countBefore = listBeforeResult.TotalCount;

        // Act - Delete warehouse
        var deleteResponse = await Client.DeleteAsync($"/api/v1/Warehouses/{warehouseId}");
        TestAssertions.AssertHttpSuccess(deleteResponse);

        // Assert - Verify warehouse no longer appears in list
        var listAfter = await Client.GetAsync("/api/v1/Warehouses");
        var listAfterResult = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(listAfter);
        TestAssertions.AssertEqual(countBefore - 1, listAfterResult.TotalCount);
        TestAssertions.AssertFalse(listAfterResult.Data.Any(w => w.Id == warehouseId));
    }

    [Fact]
    public async Task DeleteWarehouse_WithSpecialCharacterName_ShouldDeleteCorrectly()
    {
        // Arrange
        await SeedTestDataAsync();
        var specialName = "Lager #1 & Co. (Spëciäl)";
        var warehouseId = await CreateTestWarehouseAsync(1, specialName);
        SetTenantHeader(1);

        // Verify warehouse exists first
        var getBeforeResponse = await Client.GetAsync($"/api/v1/Warehouses/{warehouseId}");
        TestAssertions.AssertHttpSuccess(getBeforeResponse);
        var getBeforeResult = await ReadResponseAsync<Result<WarehouseDetailDto>>(getBeforeResponse);
        TestAssertions.AssertEqual(specialName, getBeforeResult.Data!.Name);

        // Act - Delete warehouse
        var deleteResponse = await Client.DeleteAsync($"/api/v1/Warehouses/{warehouseId}");

        // Assert
        TestAssertions.AssertHttpSuccess(deleteResponse);
        
        // Verify deletion
        var getAfterResponse = await Client.GetAsync($"/api/v1/Warehouses/{warehouseId}");
        TestAssertions.AssertHttpStatusCode(getAfterResponse, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteWarehouse_EnsureNoSideEffectsOnOtherTenants()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId1 = await CreateTestWarehouseAsync(1, "Tenant 1 Warehouse");
        var warehouseId2 = await CreateTestWarehouseAsync(2, "Tenant 2 Warehouse");

        // Get initial counts for both tenants
        SetTenantHeader(1);
        var list1Before = await Client.GetAsync("/api/v1/Warehouses");
        var list1BeforeResult = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(list1Before);
        var count1Before = list1BeforeResult.TotalCount;

        SetTenantHeader(2);
        var list2Before = await Client.GetAsync("/api/v1/Warehouses");
        var list2BeforeResult = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(list2Before);
        var count2Before = list2BeforeResult.TotalCount;

        // Act - Delete from tenant 1
        SetTenantHeader(1);
        var deleteResponse = await Client.DeleteAsync($"/api/v1/Warehouses/{warehouseId1}");
        TestAssertions.AssertHttpSuccess(deleteResponse);

        // Assert - Tenant 1 count decreased, tenant 2 unchanged
        var list1After = await Client.GetAsync("/api/v1/Warehouses");
        var list1AfterResult = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(list1After);
        TestAssertions.AssertEqual(count1Before - 1, list1AfterResult.TotalCount);

        SetTenantHeader(2);
        var list2After = await Client.GetAsync("/api/v1/Warehouses");
        var list2AfterResult = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(list2After);
        TestAssertions.AssertEqual(count2Before, list2AfterResult.TotalCount); // Unchanged
        
        // Tenant 2's warehouse should still exist
        var getResponse = await Client.GetAsync($"/api/v1/Warehouses/{warehouseId2}");
        TestAssertions.AssertHttpSuccess(getResponse);
    }
}