using System.Net;
using maERP.Domain.Constants;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Xunit;

namespace maERP.Server.Tests.Features.Tenant.Queries;

public class TenantDetailQueryTests : TenantIsolatedTestBase
{
    [Fact]
    public async Task GetTenantDetail_WithoutAuthentication_ShouldReturnOkInTestEnvironment()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SimulateUnauthenticatedRequest();

        // Act
        var response = await Client.GetAsync($"/api/v1/Tenants/{TenantConstants.TestTenant1Id}");

        // Assert - In test environment, auth is bypassed for Tenant endpoints so we get OK instead of Unauthorized
        TestAssertions.AssertHttpSuccess(response);
    }

    [Fact]
    public async Task GetTenantDetail_WithValidIdAsSuperadmin_ShouldReturnTenantDetails()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        
        // Act
        var response = await Client.GetAsync($"/api/v1/Tenants/{TenantConstants.TestTenant1Id}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<TenantDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(TenantConstants.TestTenant1Id, result.Data?.Id ?? Guid.Empty);
        TestAssertions.AssertEqual("Test Tenant 1", result.Data?.Name);
        TestAssertions.AssertEqual("TEST1", result.Data?.TenantCode);
    }

    [Fact]
    public async Task GetTenantDetail_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        var nonExistentId = Guid.NewGuid();

        // Act
        var response = await Client.GetAsync($"/api/v1/Tenants/{nonExistentId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTenantDetail_WithInvalidId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Act
        var response = await Client.GetAsync("/api/v1/Tenants/invalid");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTenantDetail_WithZeroId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Act
        var response = await Client.GetAsync($"/api/v1/Tenants/{Guid.Empty}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTenantDetail_WithInvalidGuid_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Act
        var response = await Client.GetAsync("/api/v1/Tenants/invalid-guid");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTenantDetail_WithValidId_ShouldReturnTenant2Details()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Act
        var response = await Client.GetAsync($"/api/v1/Tenants/{TenantConstants.TestTenant2Id}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<TenantDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(TenantConstants.TestTenant2Id, result.Data?.Id ?? Guid.Empty);
        TestAssertions.AssertEqual("Test Tenant 2", result.Data?.Name);
        TestAssertions.AssertEqual("TEST2", result.Data?.TenantCode);
    }

    [Fact]
    public async Task GetTenantDetail_WithValidId_ShouldReturnTenant3Details()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Act
        var response = await Client.GetAsync($"/api/v1/Tenants/{TenantConstants.TestTenant3Id}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<TenantDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(TenantConstants.TestTenant3Id, result.Data?.Id ?? Guid.Empty);
        TestAssertions.AssertEqual("Test Tenant 3", result.Data?.Name);
        TestAssertions.AssertEqual("TEST3", result.Data?.TenantCode);
    }

    [Fact]
    public async Task GetTenantDetail_EndpointAcceptsGetMethod_ShouldNotReturnMethodNotAllowed()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Act
        var response = await Client.GetAsync($"/api/v1/Tenants/{TenantConstants.TestTenant1Id}");

        // Assert
        TestAssertions.AssertNotEqual(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantDetail_EndpointRejectsPostMethod_ShouldReturnMethodNotAllowed()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Act
        var response = await Client.PostAsync($"/api/v1/Tenants/{TenantConstants.TestTenant1Id}", new StringContent(""));

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.MethodNotAllowed);
    }

    [Fact]
    public async Task GetTenantDetail_WrongApiVersion_ShouldReturnNotFound()
    {
        // Act
        var response = await Client.GetAsync($"/api/v2/Tenants/{TenantConstants.TestTenant1Id}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTenantDetail_ResponseStructure_ShouldContainAllRequiredFields()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Act
        var response = await Client.GetAsync($"/api/v1/Tenants/{TenantConstants.TestTenant1Id}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<TenantDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);

        var tenant = result.Data;
        TestAssertions.AssertTrue(tenant.Id != Guid.Empty);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(tenant.Name));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(tenant.TenantCode));
        TestAssertions.AssertTrue(tenant.IsActive);
        TestAssertions.AssertNotNull(tenant.DateCreated);
        TestAssertions.AssertNotNull(tenant.DateModified);
    }

    [Fact]
    public async Task GetTenantDetail_ResponseFormat_ShouldReturnJsonContent()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Act
        var response = await Client.GetAsync($"/api/v1/Tenants/{TenantConstants.TestTenant1Id}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        TestAssertions.AssertTrue(response.Content.Headers.ContentType?.MediaType?.Contains("application/json") ?? false);
    }
}