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

namespace maERP.Server.Tests.Features.SalesChannel.Queries;

public class SalesChannelDetailQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;
    
    // Test Entity IDs
    private static readonly Guid TestSalesChannel1Id = new("11111111-1111-1111-1111-111111111111");
    private static readonly Guid TestSalesChannel2Id = new("22222222-2222-2222-2222-222222222222");
    private static readonly Guid TestSalesChannel3Id = new("33333333-3333-3333-3333-333333333333");

    public SalesChannelDetailQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_SalesChannelDetailQueryTests_{uniqueId}";
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

                var TestWarehouse1Id = new Guid("44444444-4444-4444-4444-444444444444");
                var TestWarehouse2Id = new Guid("55555555-5555-5555-5555-555555555555");
                var TestWarehouse3Id = new Guid("66666666-6666-6666-6666-666666666666");

                // Get existing warehouses created by TestDataSeeder or create them
                var warehouse1 = await DbContext.Warehouse.FirstOrDefaultAsync(w => w.TenantId == TenantConstants.TestTenant1Id);
                var warehouse2 = await DbContext.Warehouse.Skip(1).FirstOrDefaultAsync(w => w.TenantId == TenantConstants.TestTenant1Id);
                var warehouse3 = await DbContext.Warehouse.FirstOrDefaultAsync(w => w.TenantId == TenantConstants.TestTenant2Id);

                if (warehouse1 == null)
                {
                    warehouse1 = new maERP.Domain.Entities.Warehouse
                    {
                        Id = TestWarehouse1Id,
                        Name = "Warehouse T1-1",
                        TenantId = TenantConstants.TestTenant1Id
                    };
                    DbContext.Warehouse.Add(warehouse1);
                }

                if (warehouse2 == null)
                {
                    warehouse2 = new maERP.Domain.Entities.Warehouse
                    {
                        Id = TestWarehouse2Id,
                        Name = "Warehouse T1-2",
                        TenantId = TenantConstants.TestTenant1Id
                    };
                    DbContext.Warehouse.Add(warehouse2);
                }

                if (warehouse3 == null)
                {
                    warehouse3 = new maERP.Domain.Entities.Warehouse
                    {
                        Id = TestWarehouse3Id,
                        Name = "Warehouse T2-1",
                        TenantId = TenantConstants.TestTenant2Id
                    };
                    DbContext.Warehouse.Add(warehouse3);
                }

                await DbContext.SaveChangesAsync();

                // Seed SalesChannels for Tenant 1
                var salesChannel1_1 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = TestSalesChannel1Id,
                    Type = SalesChannelType.WooCommerce,
                    Name = "WooCommerce Store T1",
                    Url = "https://store1.example.com",
                    Username = "user1",
                    Password = "pass1",
                    ImportProducts = true,
                    ExportProducts = true,
                    ImportCustomers = false,
                    ExportCustomers = false,
                    ImportOrders = true,
                    ExportOrders = false,
                    TenantId = TenantConstants.TestTenant1Id,
                    Warehouses = new List<maERP.Domain.Entities.Warehouse> { warehouse1, warehouse2 }
                };

                var salesChannel1_2 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = TestSalesChannel2Id,
                    Type = SalesChannelType.Shopware6,
                    Name = "Shopify Store T1",
                    Url = "https://shopify1.example.com",
                    Username = "shopify1",
                    Password = "shoppass1",
                    ImportProducts = false,
                    ExportProducts = true,
                    ImportCustomers = true,
                    ExportCustomers = true,
                    ImportOrders = false,
                    ExportOrders = true,
                    TenantId = TenantConstants.TestTenant1Id
                };

                // Seed SalesChannels for Tenant 2
                var salesChannel2_1 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = TestSalesChannel3Id,
                    Type = SalesChannelType.eBay,
                    Name = "eBay Store T2",
                    Url = "https://magento.example.com",
                    Username = "magento2",
                    Password = "magpass2",
                    ImportProducts = true,
                    ExportProducts = false,
                    ImportCustomers = true,
                    ExportCustomers = false,
                    ImportOrders = true,
                    ExportOrders = true,
                    TenantId = TenantConstants.TestTenant2Id,
                    Warehouses = new List<maERP.Domain.Entities.Warehouse> { warehouse3 }
                };

                DbContext.SalesChannel.AddRange(salesChannel1_1, salesChannel1_2, salesChannel2_1);
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
    public async Task GetSalesChannelDetail_WithValidIdAndTenant_ShouldReturnSuccess()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(TestSalesChannel1Id, result.Data.Id);
        TestAssertions.AssertEqual("WooCommerce Store T1", result.Data.Name);
    }

    [Fact(Skip = "Todo: implement feature")]
    public async Task GetSalesChannelDetail_WithoutTenantHeader_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();

        var response = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetSalesChannelDetail_TenantIsolation_ShouldOnlyReturnOwnTenantData()
    {
        await SeedTestDataAsync();

        // Test Tenant 1 trying to access its own data
        using var tenant1Client = Factory.CreateClient();
        tenant1Client.DefaultRequestHeaders.Add("X-Tenant-Id", TenantConstants.TestTenant1Id.ToString());

        var response1 = await tenant1Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response1.StatusCode);
        var result1 = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response1);
        TestAssertions.AssertNotNull(result1?.Data);
        TestAssertions.AssertEqual("WooCommerce Store T1", result1?.Data?.Name);

        // Test Tenant 1 trying to access Tenant 2 data - should fail
        var response1Cross = await tenant1Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel3Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response1Cross.StatusCode);

        // Test Tenant 2 trying to access its own data
        using var tenant2Client = Factory.CreateClient();
        tenant2Client.DefaultRequestHeaders.Add("X-Tenant-Id", TenantConstants.TestTenant2Id.ToString());

        var response2 = await tenant2Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel3Id}");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response2.StatusCode);
        var result2 = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response2);
        TestAssertions.AssertNotNull(result2?.Data);
        TestAssertions.AssertEqual("eBay Store T2", result2.Data!.Name);

        // Test Tenant 2 trying to access Tenant 1 data - should fail
        var response2Cross = await tenant2Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2Cross.StatusCode);
    }

    [Fact]
    public async Task GetSalesChannelDetail_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/SalesChannels/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task GetSalesChannelDetail_WithInvalidId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels/invalid");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetSalesChannelDetail_WithZeroId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/SalesChannels/{Guid.Empty}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetSalesChannelDetail_WithNegativeId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels/invalid-guid");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetSalesChannelDetail_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertEqual(ResultStatusCode.Ok, result.StatusCode);
    }

    [Fact]
    public async Task GetSalesChannelDetail_VerifyDtoProperties_ShouldContainAllExpectedFields()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response);
        TestAssertions.AssertNotNull(result?.Data);

        var channel = result.Data!;
        TestAssertions.AssertEqual(TestSalesChannel1Id, channel.Id);
        TestAssertions.AssertEqual(SalesChannelType.WooCommerce, channel.SalesChannelType);
        TestAssertions.AssertEqual("WooCommerce Store T1", channel.Name);
        TestAssertions.AssertEqual("https://store1.example.com", channel.Url);
        TestAssertions.AssertEqual("user1", channel.Username);
        TestAssertions.AssertEqual("pass1", channel.Password);
        TestAssertions.AssertTrue(channel.ImportProducts);
        TestAssertions.AssertTrue(channel.ExportProducts);
        TestAssertions.AssertFalse(channel.ImportCustomers);
        TestAssertions.AssertFalse(channel.ExportCustomers);
        TestAssertions.AssertTrue(channel.ImportOrders);
        TestAssertions.AssertFalse(channel.ExportOrders);
        TestAssertions.AssertNotNull(channel.Warehouses);
    }

    [Fact]
    public async Task GetSalesChannelDetail_WithWarehouses_ShouldIncludeWarehouseData()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response);
        TestAssertions.AssertNotNull(result?.Data);

        var channel = result.Data!;
        TestAssertions.AssertNotNull(channel.Warehouses);
        TestAssertions.AssertEqual(2, channel.Warehouses.Count);
        TestAssertions.AssertTrue(channel.Warehouses.Any(w => w.Name == "Main Warehouse Tenant 1"));
        TestAssertions.AssertTrue(channel.Warehouses.Any(w => w.Name == "Secondary Warehouse Tenant 1"));
    }

    [Fact]
    public async Task GetSalesChannelDetail_WithoutWarehouses_ShouldReturnEmptyWarehouseList()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel2Id}");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response);
        TestAssertions.AssertNotNull(result?.Data);

        var channel = result.Data!;
        TestAssertions.AssertNotNull(channel.Warehouses);
        TestAssertions.AssertEqual(0, channel.Warehouses.Count);
    }

    [Fact]
    public async Task GetSalesChannelDetail_WithNonExistentTenant_ShouldHandleGracefully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(Guid.NewGuid());

        var response = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");

        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.BadRequest ||
                                 response.StatusCode == HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetSalesChannelDetail_VerifyPasswordIncluded_ShouldContainSensitiveData()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response);
        TestAssertions.AssertNotNull(result?.Data);

        // DetailDto should include password (unlike ListDto)
        TestAssertions.AssertEqual("pass1", result.Data!.Password);
    }

    [Fact]
    public async Task GetSalesChannelDetail_VerifyAllSalesChannelTypes_ShouldHandleDifferentTypes()
    {
        await SeedTestDataAsync();

        // Test WooCommerce
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var wooResponse = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.OK, wooResponse.StatusCode);
        var wooResult = await ReadResponseAsync<Result<SalesChannelDetailDto>>(wooResponse);
        TestAssertions.AssertEqual(SalesChannelType.WooCommerce, wooResult?.Data?.SalesChannelType);

        // Test Shopware6
        var shopifyResponse = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel2Id}");
        TestAssertions.AssertEqual(HttpStatusCode.OK, shopifyResponse.StatusCode);
        var shopifyResult = await ReadResponseAsync<Result<SalesChannelDetailDto>>(shopifyResponse);
        TestAssertions.AssertEqual(SalesChannelType.Shopware6, shopifyResult?.Data?.SalesChannelType);

        // Test eBay
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var magentoResponse = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel3Id}");
        TestAssertions.AssertEqual(HttpStatusCode.OK, magentoResponse.StatusCode);
        var magentoResult = await ReadResponseAsync<Result<SalesChannelDetailDto>>(magentoResponse);
        TestAssertions.AssertEqual(SalesChannelType.eBay, magentoResult?.Data?.SalesChannelType);
    }

    [Fact]
    public async Task GetSalesChannelDetail_VerifyBooleanFlags_ShouldHaveCorrectValues()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Test first sales channel with specific flag configuration
        var response = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response);

        var channel = result.Data!;
        TestAssertions.AssertTrue(channel.ImportProducts);
        TestAssertions.AssertTrue(channel.ExportProducts);
        TestAssertions.AssertFalse(channel.ImportCustomers);
        TestAssertions.AssertFalse(channel.ExportCustomers);
        TestAssertions.AssertTrue(channel.ImportOrders);
        TestAssertions.AssertFalse(channel.ExportOrders);

        // Test second sales channel with different flag configuration
        var response2 = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel2Id}");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response2.StatusCode);
        var result2 = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response2);

        var channel2 = result2.Data!;
        TestAssertions.AssertFalse(channel2.ImportProducts);
        TestAssertions.AssertTrue(channel2.ExportProducts);
        TestAssertions.AssertTrue(channel2.ImportCustomers);
        TestAssertions.AssertTrue(channel2.ExportCustomers);
        TestAssertions.AssertFalse(channel2.ImportOrders);
        TestAssertions.AssertTrue(channel2.ExportOrders);
    }

    [Fact]
    public async Task GetSalesChannelDetail_ErrorHandling_ShouldReturnProperErrorFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/SalesChannels/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertEqual(ResultStatusCode.NotFound, result.StatusCode);
        TestAssertions.AssertNotEmpty(result.Messages);
        TestAssertions.AssertNotEmpty(result.Messages);
    }
}