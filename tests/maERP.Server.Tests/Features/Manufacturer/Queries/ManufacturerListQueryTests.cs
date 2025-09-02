using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.Manufacturer;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace maERP.Server.Tests.Features.Manufacturer.Queries;

public class ManufacturerListQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public ManufacturerListQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_ManufacturerListQueryTests_{uniqueId}";
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

    private async Task SeedManufacturerTestDataAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var hasData = await DbContext.Manufacturer.AnyAsync();
            if (!hasData)
            {
                await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

                var manufacturer1Tenant1 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = 1,
                    Name = "Alpha Manufacturing",
                    Street = "123 Industrial Blvd",
                    City = "New York",
                    State = "NY",
                    Country = "USA",
                    ZipCode = "10001",
                    Phone = "+1-555-0101",
                    Email = "contact@alpha.com",
                    Website = "https://alpha.com",
                    Logo = "alpha-logo.png",
                    TenantId = 1
                };

                var manufacturer2Tenant1 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = 2,
                    Name = "Beta Industries",
                    Street = "456 Commerce St",
                    City = "Los Angeles",
                    State = "CA",
                    Country = "USA",
                    ZipCode = "90210",
                    Phone = "+1-555-0202",
                    Email = "info@beta.com",
                    Website = "https://beta.com",
                    TenantId = 1
                };

                var manufacturer3Tenant1 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = 3,
                    Name = "Gamma Corp",
                    City = "Chicago",
                    Country = "USA",
                    TenantId = 1
                };

                var manufacturer4Tenant2 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = 4,
                    Name = "Delta Systems",
                    City = "Berlin",
                    Country = "Germany",
                    TenantId = 2
                };

                var manufacturer5Tenant2 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = 5,
                    Name = "Echo Solutions",
                    City = "Munich",
                    Country = "Germany",
                    TenantId = 2
                };

                DbContext.Manufacturer.AddRange(
                    manufacturer1Tenant1, 
                    manufacturer2Tenant1, 
                    manufacturer3Tenant1, 
                    manufacturer4Tenant2, 
                    manufacturer5Tenant2
                );
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
    public async Task GetManufacturers_WithValidTenant_ShouldReturnTenantData()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Manufacturers");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetManufacturers_WithDifferentTenant_ShouldReturnOnlyThatTenantData()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(2);

        var response = await Client.GetAsync("/api/v1/Manufacturers");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        TestAssertions.AssertTrue(result.Data?.Any(m => m.Name == "Delta Systems") ?? false);
        TestAssertions.AssertTrue(result.Data?.Any(m => m.Name == "Echo Solutions") ?? false);
    }

    [Fact]
    public async Task GetManufacturers_WithoutTenantHeader_ShouldReturnEmptyResult()
    {
        await SeedManufacturerTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Manufacturers");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetManufacturers_WithPagination_ShouldRespectPageSize()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Manufacturers?pageNumber=0&pageSize=2");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual(3, result.TotalCount);
        TestAssertions.AssertEqual(2, result.TotalPages);
    }

    [Fact]
    public async Task GetManufacturers_WithPaginationSecondPage_ShouldReturnSecondPageData()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Manufacturers?pageNumber=1&pageSize=2");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual(3, result.TotalCount);
        TestAssertions.AssertEqual(2, result.TotalPages);
    }

    [Fact]
    public async Task GetManufacturers_WithSearchString_ShouldFilterResults()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Manufacturers?searchString=Alpha");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual("Alpha Manufacturing", result.Data?.First().Name);
    }

    [Fact]
    public async Task GetManufacturers_WithSearchStringNoMatch_ShouldReturnEmpty()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Manufacturers?searchString=NonexistentManufacturer");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetManufacturers_WithOrderByName_ShouldReturnOrderedResults()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Manufacturers?orderBy=Name");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
        
        var names = result.Data?.Select(x => x.Name).ToList();
        TestAssertions.AssertEqual("Alpha Manufacturing", names?[0]);
        TestAssertions.AssertEqual("Beta Industries", names?[1]);
        TestAssertions.AssertEqual("Gamma Corp", names?[2]);
    }

    [Fact]
    public async Task GetManufacturers_WithOrderByNameDescending_ShouldReturnDescOrderedResults()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Manufacturers?orderBy=Name desc");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
        
        var names = result.Data?.Select(x => x.Name).ToList();
        TestAssertions.AssertEqual("Gamma Corp", names?[0]);
        TestAssertions.AssertEqual("Beta Industries", names?[1]);
        TestAssertions.AssertEqual("Alpha Manufacturing", names?[2]);
    }

    [Fact]
    public async Task GetManufacturers_WithOrderByCity_ShouldReturnCityOrderedResults()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Manufacturers?orderBy=City");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetManufacturers_WithMultipleOrderBy_ShouldRespectMultipleSorting()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Manufacturers?orderBy=Country,Name");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetManufacturers_WithInvalidPageNumber_ShouldReturnEmptyResults()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Manufacturers?pageNumber=10&pageSize=10");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(3, result.TotalCount);
    }

    [Fact]
    public async Task GetManufacturers_WithZeroPageSize_ShouldUseDefaultPageSize()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Manufacturers?pageSize=0");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetManufacturers_WithNegativePageNumber_ShouldHandleGracefully()
    {
        SetTenantHeader(999);

        var response = await Client.GetAsync("/api/v1/Manufacturers?pageNumber=-1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetManufacturers_WithNonExistentTenant_ShouldReturnEmptyPaginatedResult()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(999);

        var response = await Client.GetAsync("/api/v1/Manufacturers");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(0, result.TotalCount);
        TestAssertions.AssertEqual(0, result.TotalPages);
    }

    [Fact]
    public async Task GetManufacturers_ResponseStructure_ShouldContainRequiredFields()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Manufacturers");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);

        var firstManufacturer = result.Data?.First();
        TestAssertions.AssertNotNull(firstManufacturer);
        TestAssertions.AssertTrue(firstManufacturer!.Id > 0);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstManufacturer.Name));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstManufacturer.City));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstManufacturer.Country));
    }

    [Fact]
    public async Task GetManufacturers_WithSearchByCity_ShouldFilterResults()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Manufacturers?searchString=New York");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual("Alpha Manufacturing", result.Data?.First().Name);
    }

    [Fact]
    public async Task GetManufacturers_WithSearchByCountry_ShouldFilterResults()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Manufacturers?searchString=USA");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetManufacturers_WithCaseInsensitiveSearch_ShouldReturnResults()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Manufacturers?searchString=alpha");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual("Alpha Manufacturing", result.Data?.First().Name);
    }

    [Fact]
    public async Task GetManufacturers_WithPartialNameSearch_ShouldReturnMatchingResults()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Manufacturers?searchString=Beta");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual("Beta Industries", result.Data?.First().Name);
    }

    [Fact]
    public async Task GetManufacturers_WithEmptySearchString_ShouldReturnAllResults()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Manufacturers?searchString=");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetManufacturers_TenantIsolation_ShouldNotReturnOtherTenantData()
    {
        await SeedManufacturerTestDataAsync();

        SetTenantHeader(1);
        var response1 = await Client.GetAsync("/api/v1/Manufacturers");
        var result1 = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response1);
        TestAssertions.AssertEqual(3, result1.Data?.Count ?? 0);
        TestAssertions.AssertFalse(result1.Data?.Any(m => m.Name == "Delta Systems") ?? true);
        TestAssertions.AssertFalse(result1.Data?.Any(m => m.Name == "Echo Solutions") ?? true);

        SetTenantHeader(2);
        var response2 = await Client.GetAsync("/api/v1/Manufacturers");
        var result2 = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response2);
        TestAssertions.AssertEqual(2, result2.Data?.Count ?? 0);
        TestAssertions.AssertFalse(result2.Data?.Any(m => m.Name == "Alpha Manufacturing") ?? true);
        TestAssertions.AssertFalse(result2.Data?.Any(m => m.Name == "Beta Industries") ?? true);
        TestAssertions.AssertFalse(result2.Data?.Any(m => m.Name == "Gamma Corp") ?? true);
    }

    [Fact]
    public async Task GetManufacturers_WithComplexSearch_ShouldFilterCorrectly()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Manufacturers?searchString=Industries");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ManufacturerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual("Beta Industries", result.Data?.First().Name);
    }
}