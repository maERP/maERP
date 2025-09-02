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

public class SalesChannelUpdateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public SalesChannelUpdateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_SalesChannelUpdateCommandTests_{uniqueId}";
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

    protected async Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        return await Client.PutAsync(requestUri, content);
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
            var hasData = await DbContext.SalesChannel.AnyAsync();
            if (!hasData)
            {
                await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

                // Get existing warehouses created by TestDataSeeder
                var warehouse1 = await DbContext.Warehouse.FirstAsync(w => w.Id == 1 && w.TenantId == 1);
                var warehouse2 = await DbContext.Warehouse.FirstAsync(w => w.Id == 2 && w.TenantId == 1);
                var warehouse3 = await DbContext.Warehouse.FirstAsync(w => w.Id == 3 && w.TenantId == 2);

                // Create existing sales channels for testing updates
                var salesChannel1 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = 1,
                    Type = SalesChannelType.WooCommerce,
                    Name = "Original WooCommerce Store T1",
                    Url = "https://original.example.com",
                    Username = "originaluser",
                    Password = "originalpass",
                    ImportProducts = true,
                    ExportProducts = false,
                    ImportCustomers = true,
                    ExportCustomers = false,
                    ImportOrders = true,
                    ExportOrders = false,
                    TenantId = 1,
                    Warehouses = new List<maERP.Domain.Entities.Warehouse> { warehouse1 }
                };

                var salesChannel2 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = 2,
                    Type = SalesChannelType.Shopware6,
                    Name = "Original Shopware Store T1",
                    Url = "https://shopware.example.com",
                    Username = "shopwareuser",
                    Password = "shopwarepass",
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
                    Name = "Original eBay Store T2",
                    Url = "https://ebay.example.com",
                    Username = "ebayuser",
                    Password = "ebaypass",
                    ImportProducts = true,
                    ExportProducts = true,
                    ImportCustomers = true,
                    ExportCustomers = true,
                    ImportOrders = true,
                    ExportOrders = true,
                    TenantId = 2,
                    Warehouses = new List<maERP.Domain.Entities.Warehouse> { warehouse3 }
                };

                DbContext.SalesChannel.AddRange(salesChannel1, salesChannel2, salesChannel3);
                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private SalesChannelInputDto CreateUpdateSalesChannelDto(int id)
    {
        return new SalesChannelInputDto
        {
            Id = id,
            SalesChannelType = SalesChannelType.Shopware5,
            Name = "Updated Sales Channel Name",
            Url = "https://updated.example.com",
            Username = "updateduser",
            Password = "updatedpassword123",
            ImportProducts = false,
            ExportProducts = true,
            ImportCustomers = false,
            ExportCustomers = true,
            ImportOrders = false,
            ExportOrders = true,
            WarehouseIds = new List<int> { 1, 2 }
        };
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task UpdateSalesChannel_WithValidData_ShouldReturnSuccess()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateSalesChannelDto(1);

        var response = await PutAsJsonAsync("/api/v1/SalesChannels/1", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(1, result.Data);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithValidData_ShouldPersistChanges()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateSalesChannelDto(1);

        var response = await PutAsJsonAsync("/api/v1/SalesChannels/1", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertTrue(result.Succeeded);
        
        // Verify through API that changes were persisted
        var getResponse = await Client.GetAsync("/api/v1/SalesChannels/1");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesChannelDetail = await ReadResponseAsync<Result<SalesChannelDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesChannelDetail?.Data);
        TestAssertions.AssertEqual(updateDto.Name, salesChannelDetail.Data.Name);
        TestAssertions.AssertEqual(updateDto.Url, salesChannelDetail.Data.Url);
        TestAssertions.AssertEqual(updateDto.Username, salesChannelDetail.Data.Username);
        TestAssertions.AssertEqual(updateDto.Password, salesChannelDetail.Data.Password);
        TestAssertions.AssertEqual(SalesChannelType.Shopware5, salesChannelDetail.Data.SalesChannelType);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateSalesChannelDto(999);

        var response = await PutAsJsonAsync("/api/v1/SalesChannels/999", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithoutTenantHeader_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        var updateDto = CreateUpdateSalesChannelDto(1);

        var response = await PutAsJsonAsync("/api/v1/SalesChannels/1", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateSalesChannel_TenantIsolation_ShouldOnlyUpdateOwnTenantData()
    {
        await SeedTestDataAsync();

        // Test Tenant 1 updating its own data
        SetTenantHeader(1);
        var updateDto1 = CreateUpdateSalesChannelDto(1);
        updateDto1.Name = "Updated by Tenant 1";
        
        var response1 = await PutAsJsonAsync("/api/v1/SalesChannels/1", updateDto1);
        TestAssertions.AssertEqual(HttpStatusCode.OK, response1.StatusCode);

        // Test Tenant 1 trying to update Tenant 2 data - should fail
        var updateDto2 = CreateUpdateSalesChannelDto(3);
        updateDto2.Name = "Attempted update by Tenant 1";
        
        var response1Cross = await PutAsJsonAsync("/api/v1/SalesChannels/3", updateDto2);
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response1Cross.StatusCode);

        // Test Tenant 2 updating its own data
        SetTenantHeader(2);
        var updateDto3 = CreateUpdateSalesChannelDto(3);
        updateDto3.Name = "Updated by Tenant 2";
        updateDto3.WarehouseIds = new List<int> { 3 };
        
        var response2 = await PutAsJsonAsync("/api/v1/SalesChannels/3", updateDto3);
        TestAssertions.AssertEqual(HttpStatusCode.OK, response2.StatusCode);

        // Test Tenant 2 trying to update Tenant 1 data - should fail
        var updateDto4 = CreateUpdateSalesChannelDto(1);
        updateDto4.Name = "Attempted update by Tenant 2";
        
        var response2Cross = await PutAsJsonAsync("/api/v1/SalesChannels/1", updateDto4);
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2Cross.StatusCode);

        // Verify actual data in database
        TenantContext.SetCurrentTenantId(null);
        DbContext.ChangeTracker.Clear();
        
        var allSalesChannels = await DbContext.Set<maERP.Domain.Entities.SalesChannel>()
            .IgnoreQueryFilters()
            .ToListAsync();
        
        var salesChannel1 = allSalesChannels.FirstOrDefault(s => s.Id == 1);
        var salesChannel3 = allSalesChannels.FirstOrDefault(s => s.Id == 3);
        
        TestAssertions.AssertNotNull(salesChannel1);
        TestAssertions.AssertNotNull(salesChannel3);
        TestAssertions.AssertEqual("Updated by Tenant 1", salesChannel1.Name);
        TestAssertions.AssertEqual("Updated by Tenant 2", salesChannel3.Name);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithMissingRequiredFields_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = new SalesChannelInputDto
        {
            Id = 1,
            // Missing required fields: Name, SalesChannelType
            Url = "https://incomplete.example.com"
        };

        var response = await PutAsJsonAsync("/api/v1/SalesChannels/1", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithInvalidWarehouseIds_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateSalesChannelDto(1);
        updateDto.WarehouseIds = new List<int> { 999 }; // Non-existent warehouse

        var response = await PutAsJsonAsync("/api/v1/SalesChannels/1", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithCrossTenantWarehouseIds_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateSalesChannelDto(1);
        updateDto.WarehouseIds = new List<int> { 3 }; // Warehouse belongs to tenant 2

        var response = await PutAsJsonAsync("/api/v1/SalesChannels/1", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithTooLongName_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateSalesChannelDto(1);
        updateDto.Name = new string('A', 256); // Exceeds 255 character limit

        var response = await PutAsJsonAsync("/api/v1/SalesChannels/1", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(responseContent.Contains("validation errors") || responseContent.Contains("Name"));
    }

    [Fact]
    public async Task UpdateSalesChannel_WithInvalidUrl_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateSalesChannelDto(1);
        updateDto.Url = "invalid-url";

        var response = await PutAsJsonAsync("/api/v1/SalesChannels/1", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithDuplicateName_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        
        // Try to update first sales channel with the name of the second
        var updateDto = CreateUpdateSalesChannelDto(1);
        updateDto.Name = "Original Shopware Store T1"; // Name of sales channel 2

        var response = await PutAsJsonAsync("/api/v1/SalesChannels/1", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithSameName_ShouldUpdateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        
        // Update with the same name should be allowed
        var updateDto = CreateUpdateSalesChannelDto(1);
        updateDto.Name = "Original WooCommerce Store T1"; // Same name as existing

        var response = await PutAsJsonAsync("/api/v1/SalesChannels/1", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithEmptyWarehouseIds_ShouldUpdateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateSalesChannelDto(1);
        updateDto.WarehouseIds = new List<int>(); // Remove all warehouse associations

        var response = await PutAsJsonAsync("/api/v1/SalesChannels/1", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertTrue(result.Succeeded);
        
        // Verify no warehouses are associated
        var getResponse = await Client.GetAsync("/api/v1/SalesChannels/1");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesChannelDetail = await ReadResponseAsync<Result<SalesChannelDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesChannelDetail?.Data);
        TestAssertions.AssertEqual(0, salesChannelDetail.Data.Warehouses.Count);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithBooleanFlags_ShouldPersistCorrectly()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateSalesChannelDto(1);
        updateDto.ImportProducts = true;
        updateDto.ExportProducts = false;
        updateDto.ImportCustomers = true;
        updateDto.ExportCustomers = false;
        updateDto.ImportOrders = true;
        updateDto.ExportOrders = false;

        var response = await PutAsJsonAsync("/api/v1/SalesChannels/1", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertTrue(result.Succeeded);
        
        // Verify boolean flags are persisted correctly
        var getResponse = await Client.GetAsync("/api/v1/SalesChannels/1");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesChannelDetail = await ReadResponseAsync<Result<SalesChannelDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesChannelDetail?.Data);
        
        TestAssertions.AssertTrue(salesChannelDetail.Data.ImportProducts);
        TestAssertions.AssertFalse(salesChannelDetail.Data.ExportProducts);
        TestAssertions.AssertTrue(salesChannelDetail.Data.ImportCustomers);
        TestAssertions.AssertFalse(salesChannelDetail.Data.ExportCustomers);
        TestAssertions.AssertTrue(salesChannelDetail.Data.ImportOrders);
        TestAssertions.AssertFalse(salesChannelDetail.Data.ExportOrders);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithDifferentSalesChannelType_ShouldUpdateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateSalesChannelDto(1);
        updateDto.SalesChannelType = SalesChannelType.eBay; // Change from WooCommerce to eBay

        var response = await PutAsJsonAsync("/api/v1/SalesChannels/1", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertTrue(result.Succeeded);
        
        // Verify type change was persisted
        var getResponse = await Client.GetAsync("/api/v1/SalesChannels/1");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesChannelDetail = await ReadResponseAsync<Result<SalesChannelDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesChannelDetail?.Data);
        TestAssertions.AssertEqual(SalesChannelType.eBay, salesChannelDetail.Data.SalesChannelType);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithInvalidJson_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var invalidJson = "{ invalid json }";
        var content = new StringContent(invalidJson, System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PutAsync("/api/v1/SalesChannels/1", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithEmptyBody_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var content = new StringContent("", System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PutAsync("/api/v1/SalesChannels/1", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithIdMismatch_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateSalesChannelDto(2); // DTO has ID 2 but URL has ID 1

        var response = await PutAsJsonAsync("/api/v1/SalesChannels/1", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithNonExistentTenant_ShouldHandleGracefully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(999);
        var updateDto = CreateUpdateSalesChannelDto(1);

        var response = await PutAsJsonAsync("/api/v1/SalesChannels/1", updateDto);

        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.BadRequest || 
                                 response.StatusCode == HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateSalesChannel_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateSalesChannelDto(1);

        var response = await PutAsJsonAsync("/api/v1/SalesChannels/1", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data);
        TestAssertions.AssertNotNull(result.Messages);
    }
}