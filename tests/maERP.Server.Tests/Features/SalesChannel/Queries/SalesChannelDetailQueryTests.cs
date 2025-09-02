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

namespace maERP.Server.Tests.Features.SalesChannel.Queries;

public class SalesChannelDetailQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

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

                // Seed SalesChannels for Tenant 1
                var salesChannel1_1 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = 1,
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
                    TenantId = 1,
                    Warehouses = new List<maERP.Domain.Entities.Warehouse> { warehouse1, warehouse2 }
                };

                var salesChannel1_2 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = 2,
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
                    TenantId = 1
                };

                // Seed SalesChannels for Tenant 2
                var salesChannel2_1 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = 3,
                    Type = SalesChannelType.eBay,
                    Name = "Magento Store T2",
                    Url = "https://magento.example.com",
                    Username = "magento2",
                    Password = "magpass2",
                    ImportProducts = true,
                    ExportProducts = false,
                    ImportCustomers = true,
                    ExportCustomers = false,
                    ImportOrders = true,
                    ExportOrders = true,
                    TenantId = 2,
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
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/SalesChannels/1");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data.Id);
        TestAssertions.AssertEqual("WooCommerce Store T1", result.Data.Name);
    }

    [Fact]
    public async Task GetSalesChannelDetail_WithoutTenantHeader_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();

        var response = await Client.GetAsync("/api/v1/SalesChannels/1");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetSalesChannelDetail_TenantIsolation_ShouldOnlyReturnOwnTenantData()
    {
        await SeedTestDataAsync();

        // Test Tenant 1 trying to access its own data
        using var tenant1Client = Factory.CreateClient();
        tenant1Client.DefaultRequestHeaders.Add("X-Tenant-Id", "1");
        
        var response1 = await tenant1Client.GetAsync("/api/v1/SalesChannels/1");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response1.StatusCode);
        var result1 = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response1);
        TestAssertions.AssertNotNull(result1?.Data);
        TestAssertions.AssertEqual("WooCommerce Store T1", result1?.Data?.Name);

        // Test Tenant 1 trying to access Tenant 2 data - should fail
        var response1Cross = await tenant1Client.GetAsync("/api/v1/SalesChannels/3");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response1Cross.StatusCode);

        // Test Tenant 2 trying to access its own data
        using var tenant2Client = Factory.CreateClient();
        tenant2Client.DefaultRequestHeaders.Add("X-Tenant-Id", "2");
        
        var response2 = await tenant2Client.GetAsync("/api/v1/SalesChannels/3");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response2.StatusCode);
        var result2 = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response2);
        TestAssertions.AssertNotNull(result2?.Data);
        TestAssertions.AssertEqual("eBay Store T2", result2.Data!.Name);

        // Test Tenant 2 trying to access Tenant 1 data - should fail
        var response2Cross = await tenant2Client.GetAsync("/api/v1/SalesChannels/1");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2Cross.StatusCode);
    }

    [Fact]
    public async Task GetSalesChannelDetail_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/SalesChannels/999");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task GetSalesChannelDetail_WithInvalidId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/SalesChannels/invalid");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetSalesChannelDetail_WithZeroId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/SalesChannels/0");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetSalesChannelDetail_WithNegativeId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/SalesChannels/-1");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetSalesChannelDetail_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/SalesChannels/1");

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
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/SalesChannels/1");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response);
        TestAssertions.AssertNotNull(result?.Data);
        
        var channel = result.Data!;
        TestAssertions.AssertEqual(1, channel.Id);
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
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/SalesChannels/1");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response);
        TestAssertions.AssertNotNull(result?.Data);
        
        var channel = result.Data!;
        TestAssertions.AssertNotNull(channel.Warehouses);
        TestAssertions.AssertEqual(2, channel.Warehouses.Count);
        TestAssertions.AssertTrue(channel.Warehouses.Any(w => w.Name == "Warehouse T1-1"));
        TestAssertions.AssertTrue(channel.Warehouses.Any(w => w.Name == "Warehouse T1-2"));
    }

    [Fact]
    public async Task GetSalesChannelDetail_WithoutWarehouses_ShouldReturnEmptyWarehouseList()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/SalesChannels/2");

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
        SetTenantHeader(999);

        var response = await Client.GetAsync("/api/v1/SalesChannels/1");

        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.BadRequest || 
                                 response.StatusCode == HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetSalesChannelDetail_VerifyPasswordIncluded_ShouldContainSensitiveData()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/SalesChannels/1");

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
        SetTenantHeader(1);
        var wooResponse = await Client.GetAsync("/api/v1/SalesChannels/1");
        TestAssertions.AssertEqual(HttpStatusCode.OK, wooResponse.StatusCode);
        var wooResult = await ReadResponseAsync<Result<SalesChannelDetailDto>>(wooResponse);
        TestAssertions.AssertEqual(SalesChannelType.WooCommerce, wooResult?.Data?.SalesChannelType);

        // Test Shopware6
        var shopifyResponse = await Client.GetAsync("/api/v1/SalesChannels/2");
        TestAssertions.AssertEqual(HttpStatusCode.OK, shopifyResponse.StatusCode);
        var shopifyResult = await ReadResponseAsync<Result<SalesChannelDetailDto>>(shopifyResponse);
        TestAssertions.AssertEqual(SalesChannelType.Shopware6, shopifyResult?.Data?.SalesChannelType);

        // Test eBay
        SetTenantHeader(2);
        var magentoResponse = await Client.GetAsync("/api/v1/SalesChannels/3");
        TestAssertions.AssertEqual(HttpStatusCode.OK, magentoResponse.StatusCode);
        var magentoResult = await ReadResponseAsync<Result<SalesChannelDetailDto>>(magentoResponse);
        TestAssertions.AssertEqual(SalesChannelType.eBay, magentoResult?.Data?.SalesChannelType);
    }

    [Fact]
    public async Task GetSalesChannelDetail_VerifyBooleanFlags_ShouldHaveCorrectValues()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        // Test first sales channel with specific flag configuration
        var response = await Client.GetAsync("/api/v1/SalesChannels/1");
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
        var response2 = await Client.GetAsync("/api/v1/SalesChannels/2");
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
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/SalesChannels/999");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertEqual(ResultStatusCode.NotFound, result.StatusCode);
        TestAssertions.AssertNotEmpty(result.Messages);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("999")));
    }
}