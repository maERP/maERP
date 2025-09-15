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
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.SalesChannel.Commands;

public class SalesChannelCreateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    // Test Warehouse IDs
    private static readonly Guid TestWarehouse1Id = new("11111111-1111-1111-1111-111111111111");
    private static readonly Guid TestWarehouse2Id = new("22222222-2222-2222-2222-222222222222");
    private static readonly Guid TestWarehouse3Id = new("33333333-3333-3333-3333-333333333333");

    public SalesChannelCreateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_SalesChannelCreateCommandTests_{uniqueId}";
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

        Task.Delay(10).Wait();
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
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var hasData = await DbContext.Warehouse.AnyAsync();
            if (!hasData)
            {
                await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

                // Seed Warehouses for testing
                var warehouse1 = new maERP.Domain.Entities.Warehouse
                {
                    Id = TestWarehouse1Id,
                    Name = "Warehouse T1-1",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var warehouse2 = new maERP.Domain.Entities.Warehouse
                {
                    Id = TestWarehouse2Id,
                    Name = "Warehouse T1-2",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var warehouse3 = new maERP.Domain.Entities.Warehouse
                {
                    Id = TestWarehouse3Id,
                    Name = "Warehouse T2-1",
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Warehouse.AddRange(warehouse1, warehouse2, warehouse3);
                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private SalesChannelInputDto CreateValidSalesChannelDto()
    {
        return new SalesChannelInputDto
        {
            SalesChannelType = SalesChannelType.WooCommerce,
            Name = "Test WooCommerce Store",
            Url = "https://test-store.example.com",
            Username = "testuser",
            Password = "testpassword123",
            ImportProducts = true,
            ExportProducts = false,
            ImportCustomers = true,
            ExportCustomers = false,
            ImportOrders = true,
            ExportOrders = true,
            WarehouseIds = new List<Guid> { TestWarehouse1Id, TestWarehouse2Id }
        };
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task CreateSalesChannel_WithValidData_ShouldReturnCreated()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = CreateValidSalesChannelDto();

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
    }

    [Fact]
    public async Task CreateSalesChannel_WithValidData_ShouldPersistInDatabase()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = CreateValidSalesChannelDto();

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);

        // Verify through API that sales channel exists
        var getResponse = await Client.GetAsync($"/api/v1/SalesChannels/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesChannelDetail = await ReadResponseAsync<Result<SalesChannelDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesChannelDetail?.Data);
        TestAssertions.AssertEqual(salesChannelDto.Name, salesChannelDetail.Data.Name);
        TestAssertions.AssertEqual(salesChannelDto.Url, salesChannelDetail.Data.Url);
        TestAssertions.AssertEqual(salesChannelDto.Username, salesChannelDetail.Data.Username);
    }

    [Fact]
    public async Task CreateSalesChannel_WithMissingRequiredFields_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = new SalesChannelInputDto
        {
            // Missing required fields: Name, SalesChannelType
            Url = "https://incomplete.example.com"
        };

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact(Skip = "Todo: Not implemented yet")]
    public async Task CreateSalesChannel_WithoutTenantHeader_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        var salesChannelDto = CreateValidSalesChannelDto();

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact(Skip = "Todo: Not implemented yet")]
    public async Task CreateSalesChannel_WithNonExistentTenant_ShouldHandleGracefully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(Guid.NewGuid());
        var salesChannelDto = CreateValidSalesChannelDto();

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.BadRequest ||
                                 response.StatusCode == HttpStatusCode.NotFound ||
                                 response.StatusCode == HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CreateSalesChannel_WithInvalidWarehouseIds_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = CreateValidSalesChannelDto();
        salesChannelDto.WarehouseIds = new List<Guid> { Guid.NewGuid() }; // Non-existent warehouse

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact(Skip = "Todo: Not implemented yet")]
    public async Task CreateSalesChannel_WithCrossTenantWarehouseIds_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = CreateValidSalesChannelDto();
        salesChannelDto.WarehouseIds = new List<Guid> { TestWarehouse3Id }; // Warehouse belongs to tenant 2

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateSalesChannel_WithTooLongName_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = CreateValidSalesChannelDto();
        salesChannelDto.Name = new string('A', 101); // Exceeds 100 character limit

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(responseContent.Contains("validation errors") || responseContent.Contains("Name"));
    }

    [Fact]
    public async Task CreateSalesChannel_WithInvalidUrl_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = CreateValidSalesChannelDto();
        salesChannelDto.Url = "invalid-url";

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateSalesChannel_WithDuplicateName_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Create first sales channel
        var firstSalesChannel = CreateValidSalesChannelDto();
        firstSalesChannel.Name = "Unique Store Name";
        await PostAsJsonAsync("/api/v1/SalesChannels", firstSalesChannel);

        // Try to create second sales channel with same name
        var duplicateSalesChannel = CreateValidSalesChannelDto();
        duplicateSalesChannel.Name = "Unique Store Name";
        duplicateSalesChannel.Url = "https://different-url.com";

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", duplicateSalesChannel);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact(Skip = "Todo: Not implemented yet")]
    public async Task CreateSalesChannel_TenantIsolation_ShouldOnlyCreateInCorrectTenant()
    {
        await SeedTestDataAsync();

        // Create sales channels for each tenant
        var salesChannel1Dto = CreateValidSalesChannelDto();
        salesChannel1Dto.Name = $"Store Tenant 1 - {Guid.NewGuid():N}";
        salesChannel1Dto.WarehouseIds = new List<Guid> { TestWarehouse1Id };

        var salesChannel2Dto = CreateValidSalesChannelDto();
        salesChannel2Dto.Name = $"Store Tenant 2 - {Guid.NewGuid():N}";
        salesChannel2Dto.WarehouseIds = new List<Guid> { TestWarehouse3Id };

        // Create sales channel in tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var createResponse1 = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannel1Dto);
        TestAssertions.AssertEqual(HttpStatusCode.Created, createResponse1.StatusCode);

        // Create sales channel in tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var createResponse2 = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannel2Dto);
        TestAssertions.AssertEqual(HttpStatusCode.Created, createResponse2.StatusCode);

        TenantContext.SetCurrentTenantId(null);
        DbContext.ChangeTracker.Clear();

        // Check if both sales channels were created with correct tenant IDs
        var allSalesChannels = await DbContext.Set<maERP.Domain.Entities.SalesChannel>()
            .IgnoreQueryFilters()
            .ToListAsync();

        var salesChannel1InDb = allSalesChannels.FirstOrDefault(s => s.Name.Contains("Store Tenant 1"));
        var salesChannel2InDb = allSalesChannels.FirstOrDefault(s => s.Name.Contains("Store Tenant 2"));

        TestAssertions.AssertNotNull(salesChannel1InDb);
        TestAssertions.AssertNotNull(salesChannel2InDb);
        TestAssertions.AssertEqual(TenantConstants.TestTenant1Id, salesChannel1InDb.TenantId);
        TestAssertions.AssertEqual(TenantConstants.TestTenant2Id, salesChannel2InDb.TenantId);

        // Verify tenant isolation in API responses
        using var tenant1Client = Factory.CreateClient();
        tenant1Client.DefaultRequestHeaders.Add("X-Tenant-Id", TenantConstants.TestTenant1Id.ToString());

        var listResponse1 = await tenant1Client.GetAsync("/api/v1/SalesChannels");
        TestAssertions.AssertHttpSuccess(listResponse1);
        var list1 = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(listResponse1);
        var tenant1SeesTenant2Data = list1.Data?.Any(s => s.Name.Contains("Store Tenant 2")) ?? false;

        TestAssertions.AssertFalse(tenant1SeesTenant2Data, "Tenant 1 should not see Tenant 2's sales channels");
    }

    [Fact]
    public async Task CreateSalesChannel_WithAllSalesChannelTypes_ShouldCreateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var salesChannelTypes = new[]
        {
            SalesChannelType.WooCommerce,
            SalesChannelType.Shopware5,
            SalesChannelType.Shopware6,
            SalesChannelType.eBay,
            SalesChannelType.PointOfSale
        };

        foreach (var type in salesChannelTypes)
        {
            var salesChannelDto = CreateValidSalesChannelDto();
            salesChannelDto.SalesChannelType = type;
            salesChannelDto.Name = $"Test {type} Store";

            var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

            TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
            var result = await ReadResponseAsync<Result<Guid>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertTrue(result.Succeeded);
        }
    }

    [Fact]
    public async Task CreateSalesChannel_WithMinimalRequiredData_ShouldCreateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = new SalesChannelInputDto
        {
            SalesChannelType = SalesChannelType.PointOfSale,
            Name = "Minimal Sales Channel",
            Url = "https://minimal.example.com",
            Username = "minimal",
            Password = "password"
        };

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
    }

    [Fact]
    public async Task CreateSalesChannel_WithMaxValidStringLengths_ShouldCreateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = CreateValidSalesChannelDto();
        salesChannelDto.Name = new string('N', 255); // Max allowed length
        salesChannelDto.Url = new string('h', 4) + new string('t', 4) + "://" + new string('u', 240); // Max URL length
        salesChannelDto.Username = new string('U', 100); // Max username length
        salesChannelDto.Password = new string('P', 255); // Max password length

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task CreateSalesChannel_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = CreateValidSalesChannelDto();

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task CreateSalesChannel_WithInvalidJson_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var invalidJson = "{ invalid json }";
        var content = new StringContent(invalidJson, System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PostAsync("/api/v1/SalesChannels", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateSalesChannel_WithEmptyBody_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var content = new StringContent("", System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PostAsync("/api/v1/SalesChannels", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateSalesChannel_WithEmptyWarehouseIds_ShouldCreateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = CreateValidSalesChannelDto();
        salesChannelDto.WarehouseIds = new List<Guid>(); // Empty list should be allowed

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify no warehouses are associated
        var getResponse = await Client.GetAsync($"/api/v1/SalesChannels/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesChannelDetail = await ReadResponseAsync<Result<SalesChannelDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesChannelDetail?.Data);
        TestAssertions.AssertEqual(0, salesChannelDetail.Data.Warehouses.Count);
    }

    [Fact]
    public async Task CreateSalesChannel_WithBooleanFlags_ShouldPersistCorrectly()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = CreateValidSalesChannelDto();
        salesChannelDto.ImportProducts = false;
        salesChannelDto.ExportProducts = true;
        salesChannelDto.ImportCustomers = false;
        salesChannelDto.ExportCustomers = true;
        salesChannelDto.ImportOrders = false;
        salesChannelDto.ExportOrders = true;

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify boolean flags are persisted correctly
        var getResponse = await Client.GetAsync($"/api/v1/SalesChannels/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesChannelDetail = await ReadResponseAsync<Result<SalesChannelDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesChannelDetail?.Data);

        TestAssertions.AssertFalse(salesChannelDetail.Data.ImportProducts);
        TestAssertions.AssertTrue(salesChannelDetail.Data.ExportProducts);
        TestAssertions.AssertFalse(salesChannelDetail.Data.ImportCustomers);
        TestAssertions.AssertTrue(salesChannelDetail.Data.ExportCustomers);
        TestAssertions.AssertFalse(salesChannelDetail.Data.ImportOrders);
        TestAssertions.AssertTrue(salesChannelDetail.Data.ExportOrders);
    }
}