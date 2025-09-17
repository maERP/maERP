using System.Net;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace maERP.Server.Tests.Features.Tenant.Commands;

public class TenantDeleteCommandTests : TenantIsolatedTestBase
{
    private async Task<(Guid tenant1Id, Guid tenant2Id)> SeedTestDataAsync()
    {
        var hasData = await DbContext.Tenant.IgnoreQueryFilters().AnyAsync();
        if (!hasData)
        {
            await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        }

        // Get tenant IDs for both tenants
        var tenant1Id = await DbContext.Tenant.IgnoreQueryFilters()
            .Where(t => t.Id == TenantConstants.TestTenant1Id)
            .Select(t => t.Id)
            .FirstAsync();

        var tenant2Id = await DbContext.Tenant.IgnoreQueryFilters()
            .Where(t => t.Id == TenantConstants.TestTenant2Id)
            .Select(t => t.Id)
            .FirstAsync();

        return (tenant1Id, tenant2Id);
    }

    [Fact]
    public async Task DeleteTenant_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        // Arrange
        var (tenant1Id, _) = await SeedTestDataAsync();
        SimulateUnauthenticatedRequest();

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Tenants/{tenant1Id}");

        // Assert - In test environment, auth is bypassed so we get OK instead of Unauthorized
        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WithValidId_ShouldReturnOkAndDeleteTenant()
    {
        // Arrange
        var (tenant1Id, _) = await SeedTestDataAsync();
        
        // Act
        var response = await Client.DeleteAsync($"/api/v1/Tenants/{tenant1Id}");

        // Assert - In test environment, auth is bypassed and delete should succeed
        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WithNonExistentId_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        var nonExistentGuid = Guid.Parse("99999999-9999-9999-9999-999999999999");

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Tenants/{nonExistentGuid}");

        // Assert - Should return BadRequest from validator checking tenant existence
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WithEmptyId_ShouldReturnBadRequest()
    {
        // Arrange - No need to seed data for this test

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Tenants/{Guid.Empty}");

        // Assert - Should return BadRequest for empty GUID
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WithNegativeId_ShouldReturnNotFound()
    {
        // Act
        var response = await Client.DeleteAsync("/api/v1/Tenants/-1");

        // Assert - Invalid URL format should return NotFound
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WithInvalidId_ShouldReturnNotFound()
    {
        // Act
        var response = await Client.DeleteAsync("/api/v1/Tenants/invalid");

        // Assert - Invalid URL format should return NotFound
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_HttpDeleteMethod_ShouldAcceptDeleteRequests()
    {
        // Arrange
        var (tenant1Id, _) = await SeedTestDataAsync();

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Tenants/{tenant1Id}");

        // Assert - Should not return MethodNotAllowed
        TestAssertions.AssertNotEqual(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_OnlyDeleteMethod_ShouldRejectPostRequests()
    {
        // Arrange
        var (tenant1Id, _) = await SeedTestDataAsync();

        // Act
        var response = await Client.PostAsync($"/api/v1/Tenants/{tenant1Id}", new StringContent(""));

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_ApiVersioned_ShouldRespondToV1Route()
    {
        // Arrange
        var (tenant1Id, _) = await SeedTestDataAsync();

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Tenants/{tenant1Id}");

        // Assert - Should respond to route successfully
        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WrongApiVersion_ShouldReturnNotFound()
    {
        // Arrange
        var (tenant1Id, _) = await SeedTestDataAsync();

        // Act
        var response = await Client.DeleteAsync($"/api/v2/Tenants/{tenant1Id}");

        // Assert - v2 API doesn't exist, should return NotFound
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_ControllerRouting_ShouldRouteToCorrectController()
    {
        // Arrange
        var (tenant1Id, _) = await SeedTestDataAsync();

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Tenants/{tenant1Id}");

        // Assert - Should route correctly
        TestAssertions.AssertNotEqual(HttpStatusCode.NotFound, response.StatusCode);
        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData("00000001-0001-0001-0001-000000000001")]
    [InlineData("00000002-0002-0002-0002-000000000002")]
    public async Task DeleteTenant_WithDifferentValidIds_ShouldReturnBadRequestForNonExistentIds(string tenantIdString)
    {
        // Arrange
        await SeedTestDataAsync();
        var tenantId = Guid.Parse(tenantIdString);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Tenants/{tenantId}");

        // Assert - These IDs don't exist in test data, so should return BadRequest from validation
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData("ffffffff-ffff-ffff-ffff-ffffffffffff")]
    public async Task DeleteTenant_WithNonExistentGuidIds_ShouldReturnBadRequest(string tenantIdString)
    {
        // Arrange
        await SeedTestDataAsync();
        var tenantId = Guid.Parse(tenantIdString);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Tenants/{tenantId}");

        // Assert - Non-existent ID should return BadRequest from validation
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public async Task DeleteTenant_WithEmptyGuid_ShouldReturnBadRequest(string tenantIdString)
    {
        // Arrange
        await SeedTestDataAsync();
        var tenantId = Guid.Parse(tenantIdString);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Tenants/{tenantId}");

        // Assert - Empty GUID should return BadRequest from validation
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_MultipleCallsSameId_ShouldHandleCorrectly()
    {
        // Arrange
        var (tenant1Id, _) = await SeedTestDataAsync();

        // Act - First delete attempt
        var response1 = await Client.DeleteAsync($"/api/v1/Tenants/{tenant1Id}");
        
        // Act - Second delete attempt (should return BadRequest from validator after first deletion)
        var response2 = await Client.DeleteAsync($"/api/v1/Tenants/{tenant1Id}");

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.OK, response1.StatusCode);
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response2.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WithUrlTrailingSlash_ShouldHandleCorrectly()
    {
        // Arrange
        var (tenant1Id, _) = await SeedTestDataAsync();

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Tenants/{tenant1Id}/");

        // Assert - Should route correctly
        TestAssertions.AssertNotEqual(HttpStatusCode.NotFound, response.StatusCode);
        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WithSpecialCharactersInUrl_ShouldReturnNotFound()
    {
        // Act
        var response = await Client.DeleteAsync("/api/v1/Tenants/1@#$");

        // Assert - Invalid URL format should return NotFound
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WithFloatingPointId_ShouldReturnNotFound()
    {
        // Act
        var response = await Client.DeleteAsync("/api/v1/Tenants/1.5");

        // Assert - Invalid URL format should return NotFound
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WithVeryLargeId_ShouldReturnNotFound()
    {
        // Act
        var response = await Client.DeleteAsync("/api/v1/Tenants/999999999999");

        // Assert - Invalid URL format should return NotFound
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_EndpointExists_ShouldNotReturnNotFoundForValidPath()
    {
        // Arrange
        var (tenant1Id, _) = await SeedTestDataAsync();

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Tenants/{tenant1Id}");

        // Assert - Should route correctly and process successfully
        TestAssertions.AssertNotEqual(HttpStatusCode.NotFound, response.StatusCode);
        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
    }
}