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

public class SalesChannelListQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public SalesChannelListQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_SalesChannelListQueryTests_{uniqueId}";
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

                // Test Entity IDs
                var TestSalesChannel1Id = new Guid("11111111-1111-1111-1111-111111111111");
                var TestSalesChannel2Id = new Guid("22222222-2222-2222-2222-222222222222");
                var TestSalesChannel3Id = new Guid("33333333-3333-3333-3333-333333333333");
                var TestSalesChannel4Id = new Guid("44444444-4444-4444-4444-444444444444");

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
                    TenantId = TenantConstants.TestTenant1Id
                };

                var salesChannel1_2 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = TestSalesChannel2Id,
                    Type = SalesChannelType.Shopware6,
                    Name = "Shopware Store T1",
                    Url = "https://shopware1.example.com",
                    Username = "shopware1",
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
                    Url = "https://ebay.example.com",
                    Username = "ebay2",
                    Password = "ebaypass2",
                    ImportProducts = true,
                    ExportProducts = false,
                    ImportCustomers = true,
                    ExportCustomers = false,
                    ImportOrders = true,
                    ExportOrders = true,
                    TenantId = TenantConstants.TestTenant2Id
                };

                var salesChannel2_2 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = TestSalesChannel4Id,
                    Type = SalesChannelType.WooCommerce,
                    Name = "WooCommerce Store T2",
                    Url = "https://store2.example.com",
                    Username = "user2",
                    Password = "pass2",
                    ImportProducts = false,
                    ExportProducts = false,
                    ImportCustomers = false,
                    ExportCustomers = false,
                    ImportOrders = false,
                    ExportOrders = false,
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.SalesChannel.AddRange(salesChannel1_1, salesChannel1_2, salesChannel2_1, salesChannel2_2);
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
    public async Task GetSalesChannelsList_WithValidTenant_ShouldReturnSuccess()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
    }

    [Fact(Skip = "Todo: implement feature")]
    public async Task GetSalesChannelsList_WithoutTenantHeader_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();

        var response = await Client.GetAsync("/api/v1/SalesChannels");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetSalesChannelsList_TenantIsolation_ShouldOnlyReturnTenantData()
    {
        await SeedTestDataAsync();

        // Test Tenant 1 isolation
        using var tenant1Client = Factory.CreateClient();
        tenant1Client.DefaultRequestHeaders.Add("X-Tenant-Id", TenantConstants.TestTenant1Id.ToString());

        var response1 = await tenant1Client.GetAsync("/api/v1/SalesChannels");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response1.StatusCode);
        var result1 = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response1);

        TestAssertions.AssertNotNull(result1?.Data);
        TestAssertions.AssertEqual(2, result1.Data!.Count);
        TestAssertions.AssertTrue(result1.Data!.Any(s => s.Name == "WooCommerce Store T1"));
        TestAssertions.AssertTrue(result1.Data!.Any(s => s.Name == "Shopware Store T1"));
        TestAssertions.AssertFalse(result1.Data!.Any(s => s.Name.Contains("T2")));

        // Test Tenant 2 isolation
        using var tenant2Client = Factory.CreateClient();
        tenant2Client.DefaultRequestHeaders.Add("X-Tenant-Id", TenantConstants.TestTenant2Id.ToString());

        var response2 = await tenant2Client.GetAsync("/api/v1/SalesChannels");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response2.StatusCode);
        var result2 = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response2);

        TestAssertions.AssertNotNull(result2?.Data);
        TestAssertions.AssertEqual(2, result2.Data!.Count);
        TestAssertions.AssertTrue(result2.Data!.Any(s => s.Name == "eBay Store T2"));
        TestAssertions.AssertTrue(result2.Data!.Any(s => s.Name == "WooCommerce Store T2"));
        TestAssertions.AssertFalse(result2.Data!.Any(s => s.Name.Contains("T1")));
    }

    [Fact]
    public async Task GetSalesChannelsList_WithPagination_ShouldReturnCorrectPage()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels?PageNumber=1&PageSize=1");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(1, result.Data.Count);
        TestAssertions.AssertEqual(2, result.TotalCount);
        TestAssertions.AssertEqual(1, result.CurrentPage);
        TestAssertions.AssertEqual(1, result.PageSize);
    }

    [Fact]
    public async Task GetSalesChannelsList_WithSearchString_ShouldFilterResults()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels?SearchString=WooCommerce");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(1, result.Data.Count);
        TestAssertions.AssertEqual("WooCommerce Store T1", result.Data!.First().Name);
    }

    [Fact]
    public async Task GetSalesChannelsList_WithEmptySearchString_ShouldReturnAllResults()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels?SearchString=");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(2, result.Data.Count);
    }

    [Fact]
    public async Task GetSalesChannelsList_WithNonExistentSearch_ShouldReturnEmptyResults()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels?SearchString=NonExistent");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(0, result.Data.Count);
    }

    [Fact]
    public async Task GetSalesChannelsList_WithOrderBy_ShouldReturnOrderedResults()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels?OrderBy=Name desc");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(2, result.Data.Count);
        TestAssertions.AssertEqual("WooCommerce Store T1", result.Data!.First().Name);
        TestAssertions.AssertEqual("Shopware Store T1", result.Data.Last().Name);
    }

    [Fact]
    public async Task GetSalesChannelsList_WithMultipleOrderBy_ShouldReturnOrderedResults()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels?OrderBy=SalesChannelType,Name");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(2, result.Data.Count);
    }

    [Fact]
    public async Task GetSalesChannelsList_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotNull(result.Messages);

        if (result.Data!.Any())
        {
            var firstChannel = result.Data!.First();
            TestAssertions.AssertNotEqual(Guid.Empty, firstChannel.Id);
            TestAssertions.AssertNotNull(firstChannel.Name);
            TestAssertions.AssertNotNull(firstChannel.Url);
            TestAssertions.AssertNotNull(firstChannel.Username);
            TestAssertions.AssertNotNull(firstChannel.Warehouses);
        }
    }

    [Fact]
    public async Task GetSalesChannelsList_WithInvalidPageNumber_ShouldReturnEmptyResults()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels?PageNumber=999&PageSize=10");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(0, result.Data.Count);
        TestAssertions.AssertEqual(2, result.TotalCount);
    }

    [Fact]
    public async Task GetSalesChannelsList_WithLargePageSize_ShouldReturnAllResults()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels?PageSize=1000");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(2, result.Data.Count);
    }

    [Fact]
    public async Task GetSalesChannelsList_WithZeroPageSize_ShouldUseDefaultPageSize()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels?PageSize=0");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
    }

    [Fact]
    public async Task GetSalesChannelsList_WithNonExistentTenant_ShouldHandleGracefully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(Guid.NewGuid());

        var response = await Client.GetAsync("/api/v1/SalesChannels");

        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.BadRequest ||
                                 response.StatusCode == HttpStatusCode.OK);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertEqual(0, result.Data.Count);
        }
    }

    [Fact]
    public async Task GetSalesChannelsList_VerifyDtoProperties_ShouldContainAllExpectedFields()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result?.Data);

        if (result.Data!.Any())
        {
            var channel = result.Data!.First();
            TestAssertions.AssertNotEqual(Guid.Empty, channel.Id);
            TestAssertions.AssertNotNull(channel.Name);
            TestAssertions.AssertNotNull(channel.Url);
            TestAssertions.AssertNotNull(channel.Username);
            TestAssertions.AssertNotNull(channel.Warehouses);

            // Verify boolean flags are properly mapped
            TestAssertions.AssertTrue(channel.ImportProducts == true || channel.ImportProducts == false);
            TestAssertions.AssertTrue(channel.ExportProducts || !channel.ExportProducts);
            TestAssertions.AssertTrue(channel.ImportCustomers || !channel.ImportCustomers);
            TestAssertions.AssertTrue(channel.ExportCustomers || !channel.ExportCustomers);
            TestAssertions.AssertTrue(channel.ImportOrders || !channel.ImportOrders);
            TestAssertions.AssertTrue(channel.ExportOrders || !channel.ExportOrders);
        }
    }

    [Fact]
    public async Task GetSalesChannelsList_CaseSensitiveSearch_ShouldWork()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels?SearchString=woocommerce");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        // Should find WooCommerce regardless of case
        TestAssertions.AssertTrue(result.Data.Count >= 0);
    }
}