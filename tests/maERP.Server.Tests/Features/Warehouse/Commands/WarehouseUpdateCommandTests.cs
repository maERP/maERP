using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using maERP.Domain.Constants;
using Xunit;

namespace maERP.Server.Tests.Features.Warehouse.Commands;

public class WarehouseUpdateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public WarehouseUpdateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_WarehouseUpdateCommandTests_{uniqueId}";
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

    protected void SetInvalidTenantHeader()
    {
        SetTenantHeader(Guid.NewGuid()); // Non-existent tenant ID for testing tenant isolation
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

    private async Task<Guid> CreateTestWarehouseAsync(Guid tenantId, string name = "Test Warehouse")
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

    private WarehouseInputDto CreateValidUpdateDto(string name = "Updated Warehouse")
    {
        return new WarehouseInputDto
        {
            Name = name
        };
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task UpdateWarehouse_WithValidData_ShouldReturnOk()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(TenantConstants.TestTenant1Id, "Original Name");
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateValidUpdateDto("Updated Name");

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Warehouses/{warehouseId}", updateDto);

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(warehouseId, result.Data);

        // Verify the update
        var getResponse = await Client.GetAsync($"/api/v1/Warehouses/{warehouseId}");
        var getResult = await ReadResponseAsync<Result<WarehouseDetailDto>>(getResponse);
        TestAssertions.AssertEqual("Updated Name", getResult.Data!.Name);
    }

    [Fact(Skip = "Todo: Tenant isolation")]
    public async Task UpdateWarehouse_WithoutTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(TenantConstants.TestTenant1Id);
        var updateDto = CreateValidUpdateDto();

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Warehouses/{warehouseId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact(Skip = "Todo: Tenant isolation")]
    public async Task UpdateWarehouse_WithInvalidTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(TenantConstants.TestTenant1Id);
        SetInvalidTenantHeader();
        var updateDto = CreateValidUpdateDto();

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Warehouses/{warehouseId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact(Skip = "Todo: Tenant isolation")]
    public async Task UpdateWarehouse_FromDifferentTenant_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant2Id); // Different tenant
        var updateDto = CreateValidUpdateDto();

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Warehouses/{warehouseId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateWarehouse_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateValidUpdateDto();

        // Act
        var response = await PutAsJsonAsync("/api/v1/Warehouses/999", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateWarehouse_WithEmptyName_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = new WarehouseInputDto { Name = "" };

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Warehouses/{warehouseId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var content = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(content.Contains("Name"));
    }

    [Fact]
    public async Task UpdateWarehouse_WithNullName_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = new WarehouseInputDto { Name = null! };

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Warehouses/{warehouseId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var content = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(content.Contains("Name"));
    }

    [Fact]
    public async Task UpdateWarehouse_WithWhitespaceOnlyName_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = new WarehouseInputDto { Name = "   " };

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Warehouses/{warehouseId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact(Skip = "Todo: Tenant isolation")]
    public async Task UpdateWarehouse_TenantIsolation_ShouldOnlyUpdateOwnTenant()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId1 = await CreateTestWarehouseAsync(TenantConstants.TestTenant1Id, "Tenant 1 Original");
        var warehouseId2 = await CreateTestWarehouseAsync(TenantConstants.TestTenant2Id, "Tenant 2 Original");
        var updateDto = CreateValidUpdateDto("Updated Name");

        // Act - Update tenant 1's warehouse
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await PutAsJsonAsync($"/api/v1/Warehouses/{warehouseId1}", updateDto);

        // Try to update tenant 2's warehouse from tenant 1
        var response2 = await PutAsJsonAsync($"/api/v1/Warehouses/{warehouseId2}", updateDto);

        // Assert
        TestAssertions.AssertHttpSuccess(response1);
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.NotFound);

        // Verify tenant 2's warehouse was not modified
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var getResponse = await Client.GetAsync($"/api/v1/Warehouses/{warehouseId2}");
        var getResult = await ReadResponseAsync<Result<WarehouseDetailDto>>(getResponse);
        TestAssertions.AssertEqual("Tenant 2 Original", getResult.Data!.Name); // Original value
    }

    [Fact]
    public async Task UpdateWarehouse_WithLongValidName_ShouldSucceed()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Use 50 characters (maximum allowed by validation)
        var longName = new string('B', 45) + " Test"; // 50 characters total
        var updateDto = new WarehouseInputDto { Name = longName };

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Warehouses/{warehouseId}", updateDto);

        // Assert
        TestAssertions.AssertHttpSuccess(response);

        // Verify the update
        var getResponse = await Client.GetAsync($"/api/v1/Warehouses/{warehouseId}");
        var getResult = await ReadResponseAsync<Result<WarehouseDetailDto>>(getResponse);
        TestAssertions.AssertEqual(longName, getResult.Data!.Name);
    }

    [Fact]
    public async Task UpdateWarehouse_WithSpecialCharactersInName_ShouldSucceed()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var specialName = "Updated Warehouse #2 & Co. (Secondary)";
        var updateDto = new WarehouseInputDto { Name = specialName };

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Warehouses/{warehouseId}", updateDto);

        // Assert
        TestAssertions.AssertHttpSuccess(response);

        // Verify the update
        var getResponse = await Client.GetAsync($"/api/v1/Warehouses/{warehouseId}");
        var getResult = await ReadResponseAsync<Result<WarehouseDetailDto>>(getResponse);
        TestAssertions.AssertEqual(specialName, getResult.Data!.Name);
    }

    [Fact]
    public async Task UpdateWarehouse_ConcurrentUpdates_ShouldHandleCorrectly()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(TenantConstants.TestTenant1Id, "Original Name");
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act - Perform concurrent updates
        var tasks = new List<Task<HttpResponseMessage>>();
        for (int i = 0; i < 5; i++)
        {
            var updateDto = new WarehouseInputDto { Name = $"Concurrent Update {i}" };
            tasks.Add(PutAsJsonAsync($"/api/v1/Warehouses/{warehouseId}", updateDto));
        }

        var responses = await Task.WhenAll(tasks);

        // Assert - All should succeed
        foreach (var response in responses)
        {
            TestAssertions.AssertHttpSuccess(response);
        }

        // Verify final state (should be one of the updates)
        var getResponse = await Client.GetAsync($"/api/v1/Warehouses/{warehouseId}");
        var getResult = await ReadResponseAsync<Result<WarehouseDetailDto>>(getResponse);
        TestAssertions.AssertTrue(getResult.Data!.Name.StartsWith("Concurrent Update"));
    }

    [Theory(Skip = "Todo: Tenant isolation")]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("abc")]
    [InlineData("")]
    public async Task UpdateWarehouse_WithInvalidTenantHeaderValue_ShouldReturnNotFound(string invalidTenantId)
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(TenantConstants.TestTenant1Id);
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", invalidTenantId);
        var updateDto = CreateValidUpdateDto();

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Warehouses/{warehouseId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateWarehouse_WithNullDto_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await PutAsJsonAsync<WarehouseInputDto?>($"/api/v1/Warehouses/{warehouseId}", null);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateWarehouse_MultipleUpdatesSequentially_ShouldApplyAllChanges()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(TenantConstants.TestTenant1Id, "Original");
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act - Update multiple times
        var response1 = await PutAsJsonAsync($"/api/v1/Warehouses/{warehouseId}", new WarehouseInputDto { Name = "First Update" });
        TestAssertions.AssertHttpSuccess(response1);

        var response2 = await PutAsJsonAsync($"/api/v1/Warehouses/{warehouseId}", new WarehouseInputDto { Name = "Second Update" });
        TestAssertions.AssertHttpSuccess(response2);

        var response3 = await PutAsJsonAsync($"/api/v1/Warehouses/{warehouseId}", new WarehouseInputDto { Name = "Final Update" });
        TestAssertions.AssertHttpSuccess(response3);

        // Assert - Final value should be the last update
        var getResponse = await Client.GetAsync($"/api/v1/Warehouses/{warehouseId}");
        var getResult = await ReadResponseAsync<Result<WarehouseDetailDto>>(getResponse);
        TestAssertions.AssertEqual("Final Update", getResult.Data!.Name);
    }

    [Fact]
    public async Task UpdateWarehouse_ResponseTime_ShouldBeReasonable()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateValidUpdateDto();
        var startTime = DateTime.UtcNow;

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Warehouses/{warehouseId}", updateDto);
        var endTime = DateTime.UtcNow;

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var responseTime = endTime - startTime;
        TestAssertions.AssertTrue(responseTime.TotalSeconds < 5);
    }

    [Fact]
    public async Task UpdateWarehouse_VerifyResultStructure_ShouldHaveCorrectFormat()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateValidUpdateDto();

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Warehouses/{warehouseId}", updateDto);

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<Guid>>(response);

        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(ResultStatusCode.Ok, result.StatusCode);
        TestAssertions.AssertEqual(warehouseId, result.Data);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task UpdateWarehouse_WithUnicodeCharacters_ShouldSucceed()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var unicodeName = "Aktualisiertes Lager Berlin 🏢";
        var updateDto = new WarehouseInputDto { Name = unicodeName };

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Warehouses/{warehouseId}", updateDto);

        // Assert
        TestAssertions.AssertHttpSuccess(response);

        // Verify the update
        var getResponse = await Client.GetAsync($"/api/v1/Warehouses/{warehouseId}");
        var getResult = await ReadResponseAsync<Result<WarehouseDetailDto>>(getResponse);
        TestAssertions.AssertEqual(unicodeName, getResult.Data!.Name);
    }

    [Fact]
    public async Task UpdateWarehouse_WithTrimmedName_ShouldStoreCorrectly()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseId = await CreateTestWarehouseAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = new WarehouseInputDto { Name = "  Trimmed Update  " };

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Warehouses/{warehouseId}", updateDto);

        // Assert
        TestAssertions.AssertHttpSuccess(response);

        // Verify the name is stored (possibly trimmed based on business rules)
        var getResponse = await Client.GetAsync($"/api/v1/Warehouses/{warehouseId}");
        var getResult = await ReadResponseAsync<Result<WarehouseDetailDto>>(getResponse);
        TestAssertions.AssertFalse(string.IsNullOrWhiteSpace(getResult.Data!.Name));
    }

    [Fact]
    public async Task UpdateWarehouse_WithSameName_ShouldSucceed()
    {
        // Arrange
        await SeedTestDataAsync();
        var originalName = "Same Name Test";
        var warehouseId = await CreateTestWarehouseAsync(TenantConstants.TestTenant1Id, originalName);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = new WarehouseInputDto { Name = originalName };

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Warehouses/{warehouseId}", updateDto);

        // Assert
        TestAssertions.AssertHttpSuccess(response);

        // Verify the name remains the same
        var getResponse = await Client.GetAsync($"/api/v1/Warehouses/{warehouseId}");
        var getResult = await ReadResponseAsync<Result<WarehouseDetailDto>>(getResponse);
        TestAssertions.AssertEqual(originalName, getResult.Data!.Name);
    }
}