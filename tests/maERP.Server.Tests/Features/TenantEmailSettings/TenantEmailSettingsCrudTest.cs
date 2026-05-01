using System.Net;
using maERP.Domain.Constants;
using maERP.Domain.Dtos.TenantEmailSettings;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace maERP.Server.Tests.Features.TenantEmailSettings;

public class TenantEmailSettingsCrudTest : TenantIsolatedTestBase
{
    private async Task SeedAsync()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
    }

    private static TenantEmailSettingsInputDto SmtpDto() => new()
    {
        ProviderType = EmailProviderType.Smtp,
        IsActive = true,
        SmtpHost = "smtp.example.com",
        SmtpPort = 587,
        SmtpUsername = "tenant-user",
        SmtpPassword = "tenant-secret",
        SmtpEnableSsl = true,
        FromAddress = "tenant@example.com",
        FromName = "Tenant"
    };

    [Fact]
    public async Task Get_NoOverride_Returns404()
    {
        await SeedAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/TenantEmailSettings");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Upsert_Creates_NewOverride()
    {
        await SeedAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await PutAsJsonAsync("/api/v1/TenantEmailSettings", SmtpDto());

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotEqual(Guid.Empty, result.Data);

        var stored = await DbContext.TenantEmailSettings
            .FirstOrDefaultAsync(s => s.TenantId == TenantConstants.TestTenant1Id);
        TestAssertions.AssertNotNull(stored);
        Assert.Equal("smtp.example.com", stored!.SmtpHost);
        Assert.Equal("tenant-secret", stored.SmtpPassword);
    }

    [Fact]
    public async Task Upsert_Updates_ExistingOverride_AndKeepsSecretWhenOmitted()
    {
        await SeedAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var initial = await PutAsJsonAsync("/api/v1/TenantEmailSettings", SmtpDto());
        TestAssertions.AssertHttpStatusCode(initial, HttpStatusCode.Created);

        var update = SmtpDto();
        update.SmtpHost = "new.smtp.example.com";
        update.SmtpPassword = null; // Caller omits secret on edit — must be preserved.

        var response = await PutAsJsonAsync("/api/v1/TenantEmailSettings", update);

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.OK);

        var stored = await DbContext.TenantEmailSettings.AsNoTracking()
            .FirstAsync(s => s.TenantId == TenantConstants.TestTenant1Id);
        Assert.Equal("new.smtp.example.com", stored.SmtpHost);
        Assert.Equal("tenant-secret", stored.SmtpPassword);
    }

    [Fact]
    public async Task Get_HidesSecretsButReportsTheirPresence()
    {
        await SeedAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var put = await PutAsJsonAsync("/api/v1/TenantEmailSettings", SmtpDto());
        TestAssertions.AssertHttpStatusCode(put, HttpStatusCode.Created);

        var response = await Client.GetAsync("/api/v1/TenantEmailSettings");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.OK);
        var result = await ReadResponseAsync<Result<TenantEmailSettingsDetailDto>>(response);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data!.SmtpPasswordIsSet);
        Assert.Null(typeof(TenantEmailSettingsDetailDto).GetProperty("SmtpPassword"));
    }

    [Fact]
    public async Task Delete_RemovesOverride()
    {
        await SeedAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var put = await PutAsJsonAsync("/api/v1/TenantEmailSettings", SmtpDto());
        TestAssertions.AssertHttpStatusCode(put, HttpStatusCode.Created);

        var response = await Client.DeleteAsync("/api/v1/TenantEmailSettings");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NoContent);
        var stored = await DbContext.TenantEmailSettings.AsNoTracking()
            .FirstOrDefaultAsync(s => s.TenantId == TenantConstants.TestTenant1Id);
        Assert.Null(stored);
    }

    [Fact]
    public async Task Delete_WithoutOverride_Returns404()
    {
        await SeedAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync("/api/v1/TenantEmailSettings");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Get_Tenant2_DoesNotSeeTenant1Override()
    {
        await SeedAsync();

        SetTenantHeader(TenantConstants.TestTenant1Id);
        var put = await PutAsJsonAsync("/api/v1/TenantEmailSettings", SmtpDto());
        TestAssertions.AssertHttpStatusCode(put, HttpStatusCode.Created);

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response = await Client.GetAsync("/api/v1/TenantEmailSettings");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Upsert_InvalidEmail_ReturnsBadRequest()
    {
        await SeedAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var dto = SmtpDto();
        dto.FromAddress = "not-an-email";

        var response = await PutAsJsonAsync("/api/v1/TenantEmailSettings", dto);

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }
}
