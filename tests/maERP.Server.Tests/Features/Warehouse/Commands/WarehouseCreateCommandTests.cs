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

public class WarehouseCreateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public WarehouseCreateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_WarehouseCreateCommandTests_{uniqueId}";
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
        SetTenantHeader(999); // Non-existent tenant ID for testing tenant isolation
    }

    protected async Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        return await Client.PostAsync(requestUri, content);
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
        var hasData = await DbContext.Tenant.IgnoreQueryFilters().AnyAsync();
        if (!hasData)
        {
            await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        }
    }

    private WarehouseInputDto CreateValidWarehouseDto()
    {
        return new WarehouseInputDto
        {
            Name = "Test Warehouse"
        };
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task CreateWarehouse_WithValidData_ShouldReturnCreated()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var warehouseDto = CreateValidWarehouseDto();

        // Act
        var response = await PostAsJsonAsync("/api/v1/Warehouses", warehouseDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data > 0);
    }

    [Fact]
    public async Task CreateWarehouse_WithoutTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseDto = CreateValidWarehouseDto();

        // Act
        var response = await PostAsJsonAsync("/api/v1/Warehouses", warehouseDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateWarehouse_WithInvalidTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        SetInvalidTenantHeader();
        var warehouseDto = CreateValidWarehouseDto();

        // Act
        var response = await PostAsJsonAsync("/api/v1/Warehouses", warehouseDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateWarehouse_WithEmptyName_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var warehouseDto = new WarehouseInputDto
        {
            Name = ""
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/Warehouses", warehouseDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var content = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(content.Contains("Name"));
    }

    [Fact]
    public async Task CreateWarehouse_WithNullName_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var warehouseDto = new WarehouseInputDto
        {
            Name = null!
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/Warehouses", warehouseDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var content = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(content.Contains("Name"));
    }

    [Fact]
    public async Task CreateWarehouse_WithWhitespaceOnlyName_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var warehouseDto = new WarehouseInputDto
        {
            Name = "   "
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/Warehouses", warehouseDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task CreateWarehouse_TenantIsolation_ShouldCreateSeparatelyForEachTenant()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseDto1 = new WarehouseInputDto { Name = "Warehouse Tenant 1" };
        var warehouseDto2 = new WarehouseInputDto { Name = "Warehouse Tenant 2" };

        // Act - Create for tenant 1
        SetTenantHeader(1);
        var response1 = await PostAsJsonAsync("/api/v1/Warehouses", warehouseDto1);
        var result1 = await ReadResponseAsync<Result<int>>(response1);

        // Act - Create for tenant 2
        SetTenantHeader(2);
        var response2 = await PostAsJsonAsync("/api/v1/Warehouses", warehouseDto2);
        var result2 = await ReadResponseAsync<Result<int>>(response2);

        // Assert
        TestAssertions.AssertHttpStatusCode(response1, HttpStatusCode.Created);
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.Created);
        TestAssertions.AssertNotEqual(result1.Data, result2.Data);

        // Verify tenant isolation - tenant 1 can't access tenant 2's warehouse
        SetTenantHeader(1);
        var getResponse = await Client.GetAsync($"/api/v1/Warehouses/{result2.Data}");
        TestAssertions.AssertHttpStatusCode(getResponse, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateWarehouse_WithLongValidName_ShouldSucceed()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var longName = new string('A', 100) + " Warehouse";
        var warehouseDto = new WarehouseInputDto
        {
            Name = longName
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/Warehouses", warehouseDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<int>>(response);
        
        // Verify the created warehouse has correct name
        var getResponse = await Client.GetAsync($"/api/v1/Warehouses/{result.Data}");
        var getResult = await ReadResponseAsync<Result<WarehouseDetailDto>>(getResponse);
        TestAssertions.AssertEqual(longName, getResult.Data!.Name);
    }

    [Fact]
    public async Task CreateWarehouse_WithSpecialCharactersInName_ShouldSucceed()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var warehouseDto = new WarehouseInputDto
        {
            Name = "Warehouse #1 & Co. (Main)"
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/Warehouses", warehouseDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<int>>(response);
        
        // Verify the created warehouse has correct name
        var getResponse = await Client.GetAsync($"/api/v1/Warehouses/{result.Data}");
        var getResult = await ReadResponseAsync<Result<WarehouseDetailDto>>(getResponse);
        TestAssertions.AssertEqual("Warehouse #1 & Co. (Main)", getResult.Data!.Name);
    }

    [Fact]
    public async Task CreateWarehouse_MultipleConcurrentRequests_ShouldHandleCorrectly()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var tasks = new List<Task<HttpResponseMessage>>();

        // Act - Create multiple warehouses concurrently
        for (int i = 0; i < 5; i++)
        {
            var warehouseDto = new WarehouseInputDto
            {
                Name = $"Concurrent Warehouse {i}"
            };
            tasks.Add(PostAsJsonAsync("/api/v1/Warehouses", warehouseDto));
        }

        var responses = await Task.WhenAll(tasks);

        // Assert
        foreach (var response in responses)
        {
            TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
            var result = await ReadResponseAsync<Result<int>>(response);
            TestAssertions.AssertTrue(result.Succeeded);
            TestAssertions.AssertTrue(result.Data > 0);
        }

        // Verify all were created with unique IDs
        var ids = new HashSet<Guid>();
        foreach (var response in responses)
        {
            var result = await ReadResponseAsync<Result<int>>(response);
            TestAssertions.AssertTrue(ids.Add(result.Data)); // Should return true if unique
        }
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("abc")]
    [InlineData("")]
    public async Task CreateWarehouse_WithInvalidTenantHeaderValue_ShouldReturnNotFound(string invalidTenantId)
    {
        // Arrange
        await SeedTestDataAsync();
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", invalidTenantId);
        var warehouseDto = CreateValidWarehouseDto();

        // Act
        var response = await PostAsJsonAsync("/api/v1/Warehouses", warehouseDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateWarehouse_WithNullDto_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);

        // Act
        var response = await PostAsJsonAsync<WarehouseInputDto?>("/api/v1/Warehouses", null);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task CreateWarehouse_ResponseTime_ShouldBeReasonable()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var warehouseDto = CreateValidWarehouseDto();
        var startTime = DateTime.UtcNow;

        // Act
        var response = await PostAsJsonAsync("/api/v1/Warehouses", warehouseDto);
        var endTime = DateTime.UtcNow;

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var responseTime = endTime - startTime;
        TestAssertions.AssertTrue(responseTime.TotalSeconds < 5); // Should respond within 5 seconds
    }

    [Fact]
    public async Task CreateWarehouse_AfterTenantSwitch_ShouldCreateInCorrectTenant()
    {
        // Arrange
        await SeedTestDataAsync();
        var warehouseDto1 = new WarehouseInputDto { Name = "Tenant 1 Warehouse" };
        var warehouseDto2 = new WarehouseInputDto { Name = "Tenant 2 Warehouse" };

        // Act - Create in tenant 1
        SetTenantHeader(1);
        var response1 = await PostAsJsonAsync("/api/v1/Warehouses", warehouseDto1);
        var result1 = await ReadResponseAsync<Result<int>>(response1);

        // Switch to tenant 2 and create
        SetTenantHeader(2);
        var response2 = await PostAsJsonAsync("/api/v1/Warehouses", warehouseDto2);
        var result2 = await ReadResponseAsync<Result<int>>(response2);

        // Assert
        TestAssertions.AssertHttpStatusCode(response1, HttpStatusCode.Created);
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.Created);

        // Verify each tenant can only access their own warehouse
        SetTenantHeader(1);
        var get1 = await Client.GetAsync($"/api/v1/Warehouses/{result1.Data}");
        TestAssertions.AssertHttpSuccess(get1);
        var get2 = await Client.GetAsync($"/api/v1/Warehouses/{result2.Data}");
        TestAssertions.AssertHttpStatusCode(get2, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateWarehouse_VerifyResultStructure_ShouldHaveCorrectFormat()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var warehouseDto = CreateValidWarehouseDto();

        // Act
        var response = await PostAsJsonAsync("/api/v1/Warehouses", warehouseDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<int>>(response);
        
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(ResultStatusCode.Created, result.StatusCode);
        TestAssertions.AssertTrue(result.Data > 0);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task CreateWarehouse_WithUnicodeCharacters_ShouldSucceed()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var warehouseDto = new WarehouseInputDto
        {
            Name = "Lagerhalle München Süd 🏢"
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/Warehouses", warehouseDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<int>>(response);
        
        // Verify the created warehouse has correct name
        var getResponse = await Client.GetAsync($"/api/v1/Warehouses/{result.Data}");
        var getResult = await ReadResponseAsync<Result<WarehouseDetailDto>>(getResponse);
        TestAssertions.AssertEqual("Lagerhalle München Süd 🏢", getResult.Data!.Name);
    }

    [Fact]
    public async Task CreateWarehouse_WithDuplicateName_ShouldAllowCreation()
    {
        // Arrange - Assuming duplicate names are allowed
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var warehouseDto1 = new WarehouseInputDto { Name = "Duplicate Name" };
        var warehouseDto2 = new WarehouseInputDto { Name = "Duplicate Name" };

        // Act
        var response1 = await PostAsJsonAsync("/api/v1/Warehouses", warehouseDto1);
        var response2 = await PostAsJsonAsync("/api/v1/Warehouses", warehouseDto2);

        // Assert - Both should succeed if duplicates are allowed
        TestAssertions.AssertHttpStatusCode(response1, HttpStatusCode.Created);
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.Created);
        
        var result1 = await ReadResponseAsync<Result<int>>(response1);
        var result2 = await ReadResponseAsync<Result<int>>(response2);
        TestAssertions.AssertNotEqual(result1.Data, result2.Data); // Different IDs
    }

    [Fact]
    public async Task CreateWarehouse_WithTrimmedName_ShouldStoreCorrectly()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var warehouseDto = new WarehouseInputDto
        {
            Name = "  Trimmed Warehouse  "
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/Warehouses", warehouseDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<int>>(response);
        
        // Verify the name is stored (possibly trimmed based on business rules)
        var getResponse = await Client.GetAsync($"/api/v1/Warehouses/{result.Data}");
        var getResult = await ReadResponseAsync<Result<WarehouseDetailDto>>(getResponse);
        TestAssertions.AssertFalse(string.IsNullOrWhiteSpace(getResult.Data!.Name));
    }
}