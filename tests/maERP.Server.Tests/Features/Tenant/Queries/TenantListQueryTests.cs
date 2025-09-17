using System.Net;
using maERP.Domain.Constants;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace maERP.Server.Tests.Features.Tenant.Queries;

public class TenantListQueryTests : TenantIsolatedTestBase
{
    private async Task SeedTenantListTestDataAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var hasData = await DbContext.Tenant.AnyAsync();
            if (!hasData)
            {
                var tenant1 = new maERP.Domain.Entities.Tenant
                {
                    Id = TenantConstants.TestTenant1Id,
                    Name = "Alpha Tenant",
                    TenantCode = "ALPHA",
                    Description = "First alphabetically sorted tenant",
                    IsActive = true,
                    ContactEmail = "alpha@example.com",
                    DateCreated = DateTime.Now.AddDays(-100),
                    DateModified = DateTime.Now.AddDays(-50),
                };

                var tenant2 = new maERP.Domain.Entities.Tenant
                {
                    Id = TenantConstants.TestTenant2Id,
                    Name = "Beta Corporation",
                    TenantCode = "BETA",
                    Description = "Second test tenant for beta testing",
                    IsActive = false,
                    ContactEmail = "beta@corporation.com",
                    DateCreated = DateTime.Now.AddDays(-80),
                    DateModified = DateTime.Now.AddDays(-30),
                };

                var tenant3 = new maERP.Domain.Entities.Tenant
                {
                    Id = TenantConstants.TestTenant3Id,
                    Name = "Gamma Solutions",
                    TenantCode = "GAMMA",
                    Description = "Third tenant for gamma solutions",
                    IsActive = true,
                    ContactEmail = "info@gamma-solutions.net",
                    DateCreated = DateTime.Now.AddDays(-60),
                    DateModified = DateTime.Now.AddDays(-20),
                };

                DbContext.Tenant.AddRange(tenant1, tenant2, tenant3);
                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    [Fact]
    public async Task GetTenantList_WithoutAuthentication_ShouldReturnOkInTestEnvironment()
    {
        // Arrange
        await SeedTenantListTestDataAsync();
        SimulateUnauthenticatedRequest();

        // Act
        var response = await Client.GetAsync("/api/v1/Tenants");

        // Assert - In test environment, auth is bypassed for Tenant endpoints so we get OK instead of Unauthorized
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
    }

    [Fact]
    public async Task GetTenantList_WithDefaultPagination_ShouldReturnAllTenants()
    {
        // Arrange
        await SeedTenantListTestDataAsync();

        // Act
        var response = await Client.GetAsync("/api/v1/Tenants");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual(3, result.TotalCount);
    }

    [Fact]
    public async Task GetTenantList_WithCustomPageSize_ShouldRespectPageSize()
    {
        // Arrange
        await SeedTenantListTestDataAsync();

        // Act
        var response = await Client.GetAsync("/api/v1/Tenants?pageSize=2");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual(3, result.TotalCount);
    }

    [Fact]
    public async Task GetTenantList_WithPageNumber_ShouldReturnCorrectPage()
    {
        // Arrange
        await SeedTenantListTestDataAsync();

        // Act
        var response = await Client.GetAsync("/api/v1/Tenants?pageNumber=1&pageSize=1");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual(3, result.TotalCount);
    }

    [Fact]
    public async Task GetTenantList_WithSearchString_ShouldFilterResults()
    {
        // Arrange
        await SeedTenantListTestDataAsync();

        // Act
        var response = await Client.GetAsync("/api/v1/Tenants?searchString=Alpha");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual("Alpha Tenant", result.Data?.First().Name);
    }

    [Fact]
    public async Task GetTenantList_WithOrderBy_ShouldReturnOrderedResults()
    {
        // Arrange
        await SeedTenantListTestDataAsync();

        // Act
        var response = await Client.GetAsync("/api/v1/Tenants?orderBy=Name");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
        
        var names = result.Data?.Select(x => x.Name).ToList();
        TestAssertions.AssertEqual("Alpha Tenant", names?[0]);
        TestAssertions.AssertEqual("Beta Corporation", names?[1]);
        TestAssertions.AssertEqual("Gamma Solutions", names?[2]);
    }

    [Fact]
    public async Task GetTenantList_WithOrderByDescending_ShouldReturnDescendingOrder()
    {
        // Arrange
        await SeedTenantListTestDataAsync();

        // Act
        var response = await Client.GetAsync("/api/v1/Tenants?orderBy=Name desc");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
        
        var names = result.Data?.Select(x => x.Name).ToList();
        TestAssertions.AssertEqual("Gamma Solutions", names?[0]);
        TestAssertions.AssertEqual("Beta Corporation", names?[1]);
        TestAssertions.AssertEqual("Alpha Tenant", names?[2]);
    }

    [Fact]
    public async Task GetTenantList_EndpointExists_ShouldNotReturnNotFound()
    {
        // Act
        var response = await Client.GetAsync("/api/v1/Tenants");

        // Assert
        TestAssertions.AssertNotEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantList_WrongApiVersion_ShouldReturnNotFound()
    {
        // Act
        var response = await Client.GetAsync("/api/v2/Tenants");

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantList_WithComplexQueryParameters_ShouldHandleAllParameters()
    {
        // Arrange
        await SeedTenantListTestDataAsync();

        // Act
        var response = await Client.GetAsync("/api/v1/Tenants?pageNumber=0&pageSize=10&searchString=Tenant&orderBy=Name desc");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0); // Only "Alpha Tenant" contains "Tenant"
        TestAssertions.AssertEqual("Alpha Tenant", result.Data?.First().Name);
    }

    [Fact]
    public async Task GetTenantList_HttpGetMethod_ShouldAcceptGetRequests()
    {
        // Act
        var response = await Client.GetAsync("/api/v1/Tenants");

        // Assert
        TestAssertions.AssertNotEqual(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantList_WithInvalidPageParameters_ShouldHandleGracefully()
    {
        // Arrange
        await SeedTenantListTestDataAsync();

        // Act
        var response = await Client.GetAsync("/api/v1/Tenants?pageNumber=-1&pageSize=0");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
    }

    [Fact]
    public async Task GetTenantList_WithEmptySearchString_ShouldReturnAllTenants()
    {
        // Arrange
        await SeedTenantListTestDataAsync();

        // Act
        var response = await Client.GetAsync("/api/v1/Tenants?searchString=");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetTenantList_ResponseFormat_ShouldReturnJsonWithCorrectStructure()
    {
        // Arrange
        await SeedTenantListTestDataAsync();

        // Act
        var response = await Client.GetAsync("/api/v1/Tenants");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        TestAssertions.AssertTrue(response.Content.Headers.ContentType?.MediaType?.Contains("application/json") ?? false);
        
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);

        var firstTenant = result.Data?.First();
        TestAssertions.AssertNotNull(firstTenant);
        TestAssertions.AssertTrue(firstTenant!.Id != Guid.Empty);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstTenant.Name));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstTenant.TenantCode));
    }

    [Theory]
    [InlineData("?pageSize=100")]
    [InlineData("?pageNumber=5")]
    [InlineData("?searchString=test")]
    [InlineData("?orderBy=Name")]
    public async Task GetTenantList_WithVariousParameters_ShouldReturnSuccessfulResponse(string queryParams)
    {
        // Arrange
        await SeedTenantListTestDataAsync();

        // Act
        var response = await Client.GetAsync($"/api/v1/Tenants{queryParams}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
    }

    [Fact]
    public async Task GetTenantList_SearchByTenantCode_ShouldFilterCorrectly()
    {
        // Arrange
        await SeedTenantListTestDataAsync();

        // Act
        var response = await Client.GetAsync("/api/v1/Tenants?searchString=BETA");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual("BETA", result.Data?.First().TenantCode);
    }

    [Fact]
    public async Task GetTenantList_SearchByEmail_ShouldFilterCorrectly()
    {
        // Arrange
        await SeedTenantListTestDataAsync();

        // Act
        var response = await Client.GetAsync("/api/v1/Tenants?searchString=gamma-solutions.net");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual("Gamma Solutions", result.Data?.First().Name);
    }

    [Fact]
    public async Task GetTenantList_WithNonExistentSearchTerm_ShouldReturnEmptyResults()
    {
        // Arrange
        await SeedTenantListTestDataAsync();

        // Act
        var response = await Client.GetAsync("/api/v1/Tenants?searchString=NonExistentTenant");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(0, result.TotalCount);
    }
}