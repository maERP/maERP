using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.Setting;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.Setting.Queries;

public class SettingDetailQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public SettingDetailQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_SettingDetailQueryTests_{uniqueId}";
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

    protected async Task<T> ReadResponseAsync<T>(HttpResponseMessage response) where T : class
    {
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return result ?? throw new InvalidOperationException("Failed to deserialize response");
    }

    private async Task SeedSettingTestDataAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

            var existingSettings = await DbContext.Setting.IgnoreQueryFilters().ToListAsync();
            if (!existingSettings.Any(s => s.Key == "test.detail.tenant1"))
            {
                var setting1Tenant1 = new maERP.Domain.Entities.Setting
                {
                    Key = "test.detail.tenant1",
                    Value = "detail_value1",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var setting2Tenant1 = new maERP.Domain.Entities.Setting
                {
                    Key = "test.config.tenant1",
                    Value = "config_value1",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var setting3Tenant1 = new maERP.Domain.Entities.Setting
                {
                    Key = "test.boolean.tenant1",
                    Value = "true",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var setting1Tenant2 = new maERP.Domain.Entities.Setting
                {
                    Key = "test.detail.tenant2",
                    Value = "detail_value2",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var setting2Tenant2 = new maERP.Domain.Entities.Setting
                {
                    Key = "test.config.tenant2",
                    Value = "config_value2",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var setting3Tenant2 = new maERP.Domain.Entities.Setting
                {
                    Key = "test.special.tenant2",
                    Value = "special_value",
                    TenantId = TenantConstants.TestTenant1Id
                };

                DbContext.Setting.AddRange(setting1Tenant1, setting2Tenant1, setting3Tenant1,
                                         setting1Tenant2, setting2Tenant2, setting3Tenant2);
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
    public async Task GetSettingDetail_WithValidIdAndTenant_ShouldReturnSettingDetail()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(1);

        // Get tenant 1 settings to find an ID
        var listResponse = await Client.GetAsync("/api/v1/Settings");
        var listResult = await ReadResponseAsync<PaginatedResult<SettingListDto>>(listResponse);
        var tenant1Setting = listResult.Data?.FirstOrDefault(s => s.Key.Contains("tenant1"));
        TestAssertions.AssertNotNull(tenant1Setting);

        var response = await Client.GetAsync($"/api/v1/Settings/{tenant1Setting!.Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(tenant1Setting.Id, result.Data!.Id);
        TestAssertions.AssertEqual(tenant1Setting.Key, result.Data.Key);
        TestAssertions.AssertEqual(tenant1Setting.Value, result.Data.Value);
    }

    [Fact]
    public async Task GetSettingDetail_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Settings/999999");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetSettingDetail_WithWrongTenant_ShouldReturnNotFound()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(1);

        // Get a tenant 1 setting ID
        var listResponse = await Client.GetAsync("/api/v1/Settings");
        var listResult = await ReadResponseAsync<PaginatedResult<SettingListDto>>(listResponse);
        var tenant1Setting = listResult.Data?.FirstOrDefault(s => s.Key.Contains("tenant1"));
        TestAssertions.AssertNotNull(tenant1Setting);

        // Try to access it with tenant 2
        SetTenantHeader(2);
        var response = await Client.GetAsync($"/api/v1/Settings/{tenant1Setting!.Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetSettingDetail_WithoutTenantHeader_ShouldReturnNotFound()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(1);

        // Get a tenant 1 setting ID
        var listResponse = await Client.GetAsync("/api/v1/Settings");
        var listResult = await ReadResponseAsync<PaginatedResult<SettingListDto>>(listResponse);
        var tenant1Setting = listResult.Data?.FirstOrDefault(s => s.Key.Contains("tenant1"));
        TestAssertions.AssertNotNull(tenant1Setting);

        // Remove tenant header and try to access tenant-specific setting
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        var response = await Client.GetAsync($"/api/v1/Settings/{tenant1Setting!.Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetSettingDetail_WithValidId_ShouldIncludeAllSettingFields()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(1);

        // Get a tenant 1 setting
        var listResponse = await Client.GetAsync("/api/v1/Settings");
        var listResult = await ReadResponseAsync<PaginatedResult<SettingListDto>>(listResponse);
        var tenant1Setting = listResult.Data?.FirstOrDefault(s => s.Key == "test.config.tenant1");
        TestAssertions.AssertNotNull(tenant1Setting);

        var response = await Client.GetAsync($"/api/v1/Settings/{tenant1Setting!.Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        
        var setting = result.Data!;
        TestAssertions.AssertEqual(tenant1Setting.Id, setting.Id);
        TestAssertions.AssertEqual("test.config.tenant1", setting.Key);
        TestAssertions.AssertEqual("config_value1", setting.Value);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(setting.Key));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(setting.Value));
    }

    [Fact]
    public async Task GetSettingDetail_WithTenant2Setting_ShouldReturnCorrectSetting()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(2);

        // Get a tenant 2 setting
        var listResponse = await Client.GetAsync("/api/v1/Settings");
        var listResult = await ReadResponseAsync<PaginatedResult<SettingListDto>>(listResponse);
        var tenant2Setting = listResult.Data?.FirstOrDefault(s => s.Key == "test.detail.tenant2");
        TestAssertions.AssertNotNull(tenant2Setting);

        var response = await Client.GetAsync($"/api/v1/Settings/{tenant2Setting!.Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(tenant2Setting.Id, result.Data!.Id);
        TestAssertions.AssertEqual("test.detail.tenant2", result.Data.Key);
        TestAssertions.AssertEqual("detail_value2", result.Data.Value);
    }

    [Fact]
    public async Task GetSettingDetail_WithInvalidId_ShouldReturnBadRequest()
    {
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Settings/invalid");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetSettingDetail_WithZeroId_ShouldReturnNotFound()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Settings/0");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetSettingDetail_WithNegativeId_ShouldReturnNotFound()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Settings/-1");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetSettingDetail_WithNonExistentTenant_ShouldReturnNotFound()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(999);

        var response = await Client.GetAsync("/api/v1/Settings/1");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetSettingDetail_ResponseStructure_ShouldHaveCorrectSuccessFormat()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Settings/1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task GetSettingDetail_ResponseStructure_ShouldHaveCorrectErrorFormat()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Settings/999");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task GetSettingDetail_WithLargeId_ShouldHandleGracefully()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Settings/2147483647");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetSettingDetail_TenantIsolation_ShouldNotReturnOtherTenantSettings()
    {
        await SeedSettingTestDataAsync();

        SetTenantHeader(1);
        var response1 = await Client.GetAsync("/api/v1/Settings/4");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response1.StatusCode);

        var response2 = await Client.GetAsync("/api/v1/Settings/5");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);

        var response3 = await Client.GetAsync("/api/v1/Settings/6");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response3.StatusCode);

        SetTenantHeader(2);
        var response4 = await Client.GetAsync("/api/v1/Settings/1");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response4.StatusCode);

        var response5 = await Client.GetAsync("/api/v1/Settings/2");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response5.StatusCode);

        var response6 = await Client.GetAsync("/api/v1/Settings/3");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response6.StatusCode);
    }

    [Fact]
    public async Task GetSettingDetail_WithBooleanValue_ShouldReturnStringValue()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Settings/3");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        
        var setting = result.Data!;
        TestAssertions.AssertEqual(3, setting.Id);
        TestAssertions.AssertEqual("notifications.email", setting.Key);
        TestAssertions.AssertEqual("true", setting.Value);
    }

    [Fact]
    public async Task GetSettingDetail_WithLanguageCodeValue_ShouldReturnCorrectValue()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(2);

        var response = await Client.GetAsync("/api/v1/Settings/5");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        
        var setting = result.Data!;
        TestAssertions.AssertEqual(5, setting.Id);
        TestAssertions.AssertEqual("app.language", setting.Key);
        TestAssertions.AssertEqual("de-DE", setting.Value);
    }

    [Fact]
    public async Task GetSettingDetail_WithComplexKey_ShouldReturnCorrectSetting()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(2);

        var response = await Client.GetAsync("/api/v1/Settings/6");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        
        var setting = result.Data!;
        TestAssertions.AssertEqual(6, setting.Id);
        TestAssertions.AssertEqual("notifications.sms", setting.Key);
        TestAssertions.AssertEqual("false", setting.Value);
    }

    [Fact]
    public async Task GetSettingDetail_WithCompleteSettingData_ShouldMapAllFields()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Settings/1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        var setting = result.Data!;

        TestAssertions.AssertTrue(setting.Id > 0);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(setting.Key));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(setting.Value));
    }

    [Fact]
    public async Task GetSettingDetail_CrossTenantAccess_ShouldBeCompletelyIsolated()
    {
        await SeedSettingTestDataAsync();

        SetTenantHeader(1);
        var response1 = await Client.GetAsync("/api/v1/Settings/1");
        TestAssertions.AssertHttpSuccess(response1);

        SetTenantHeader(2);
        var response2 = await Client.GetAsync("/api/v1/Settings/1");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);

        SetTenantHeader(1);
        var response3 = await Client.GetAsync("/api/v1/Settings/4");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response3.StatusCode);

        SetTenantHeader(2);
        var response4 = await Client.GetAsync("/api/v1/Settings/4");
        TestAssertions.AssertHttpSuccess(response4);
    }

    [Fact]
    public async Task GetSettingDetail_WithNullTenant_ShouldReturnNotFound()
    {
        await SeedSettingTestDataAsync();
        
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");

        var response = await Client.GetAsync("/api/v1/Settings/1");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetSettingDetail_WithEmptyTenant_ShouldReturnNotFound()
    {
        await SeedSettingTestDataAsync();
        
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", string.Empty);

        var response = await Client.GetAsync("/api/v1/Settings/1");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetSettingDetail_TenantIsolationStressTest_ShouldMaintainIsolation()
    {
        await SeedSettingTestDataAsync();

        var tenant1Ids = new[] { 1, 2, 3 };
        var tenant2Ids = new[] { 4, 5, 6 };

        foreach (var id in tenant1Ids)
        {
            SetTenantHeader(1);
            var response = await Client.GetAsync($"/api/v1/Settings/{id}");
            TestAssertions.AssertHttpSuccess(response);

            SetTenantHeader(2);
            response = await Client.GetAsync($"/api/v1/Settings/{id}");
            TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        foreach (var id in tenant2Ids)
        {
            SetTenantHeader(2);
            var response = await Client.GetAsync($"/api/v1/Settings/{id}");
            TestAssertions.AssertHttpSuccess(response);

            SetTenantHeader(1);
            response = await Client.GetAsync($"/api/v1/Settings/{id}");
            TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}