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
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.Manufacturer.Queries;

public class ManufacturerDetailQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;
    private static readonly Guid Manufacturer1Id = Guid.NewGuid();
    private static readonly Guid Manufacturer2Id = Guid.NewGuid();
    private static readonly Guid Manufacturer3Id = Guid.NewGuid();
    private static readonly Guid Manufacturer4Id = Guid.NewGuid();

    public ManufacturerDetailQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_ManufacturerDetailQueryTests_{uniqueId}";
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
                    Id = Manufacturer1Id,
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
                    TenantId = TenantConstants.TestTenant1Id
                };

                var manufacturer2Tenant1 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = Manufacturer2Id,
                    Name = "Beta Industries",
                    Street = "456 Commerce St",
                    City = "Los Angeles",
                    State = "CA",
                    Country = "USA",
                    ZipCode = "90210",
                    Phone = "+1-555-0202",
                    Email = "info@beta.com",
                    Website = "https://beta.com",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var manufacturer3Tenant2 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = Manufacturer3Id,
                    Name = "Gamma Systems",
                    Street = "789 Tech Avenue",
                    City = "Berlin",
                    State = "Berlin",
                    Country = "Germany",
                    ZipCode = "10115",
                    Phone = "+49-30-12345678",
                    Email = "hello@gamma.de",
                    Website = "https://gamma.de",
                    Logo = "gamma-logo.png",
                    TenantId = TenantConstants.TestTenant2Id
                };

                var manufacturer4Tenant2 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = Manufacturer4Id,
                    Name = "Delta Corp",
                    City = "Munich",
                    Country = "Germany",
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Manufacturer.AddRange(
                    manufacturer1Tenant1,
                    manufacturer2Tenant1,
                    manufacturer3Tenant2,
                    manufacturer4Tenant2
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
    public async Task GetManufacturerDetail_WithValidIdAndTenant_ShouldReturnManufacturerDetail()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Manufacturers/{Manufacturer1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<ManufacturerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual<Guid>(Manufacturer1Id, result.Data!.Id);
        TestAssertions.AssertEqual("Alpha Manufacturing", result.Data.Name);
        TestAssertions.AssertEqual("New York", result.Data.City);
        TestAssertions.AssertEqual("USA", result.Data.Country);
    }

    [Fact]
    public async Task GetManufacturerDetail_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Manufacturers/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<ManufacturerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetManufacturerDetail_WithWrongTenant_ShouldReturnNotFound()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var response = await Client.GetAsync($"/api/v1/Manufacturers/{Manufacturer1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<ManufacturerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetManufacturerDetail_WithoutTenantHeader_ShouldReturnNotFound()
    {
        await SeedManufacturerTestDataAsync();

        var response = await Client.GetAsync($"/api/v1/Manufacturers/{Manufacturer1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<ManufacturerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetManufacturerDetail_WithValidId_ShouldIncludeAllManufacturerFields()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Manufacturers/{Manufacturer1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<ManufacturerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var manufacturer = result.Data!;
        TestAssertions.AssertEqual<Guid>(Manufacturer1Id, manufacturer.Id);
        TestAssertions.AssertEqual("Alpha Manufacturing", manufacturer.Name);
        TestAssertions.AssertEqual("123 Industrial Blvd", manufacturer.Street);
        TestAssertions.AssertEqual("New York", manufacturer.City);
        TestAssertions.AssertEqual("NY", manufacturer.State);
        TestAssertions.AssertEqual("USA", manufacturer.Country);
        TestAssertions.AssertEqual("10001", manufacturer.ZipCode);
        TestAssertions.AssertEqual("+1-555-0101", manufacturer.Phone);
        TestAssertions.AssertEqual("contact@alpha.com", manufacturer.Email);
        TestAssertions.AssertEqual("https://alpha.com", manufacturer.Website);
        TestAssertions.AssertEqual("alpha-logo.png", manufacturer.Logo);
    }

    [Fact]
    public async Task GetManufacturerDetail_WithSecondManufacturer_ShouldReturnCorrectData()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Manufacturers/{Manufacturer2Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<ManufacturerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual<Guid>(Manufacturer2Id, result.Data!.Id);
        TestAssertions.AssertEqual("Beta Industries", result.Data.Name);
        TestAssertions.AssertEqual("Los Angeles", result.Data.City);
        TestAssertions.AssertEqual("USA", result.Data.Country);
    }

    [Fact]
    public async Task GetManufacturerDetail_WithTenant2Manufacturer_ShouldReturnCorrectManufacturer()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var response = await Client.GetAsync($"/api/v1/Manufacturers/{Manufacturer3Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<ManufacturerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual<Guid>(Manufacturer3Id, result.Data!.Id);
        TestAssertions.AssertEqual("Gamma Systems", result.Data.Name);
        TestAssertions.AssertEqual("Berlin", result.Data.City);
        TestAssertions.AssertEqual("Germany", result.Data.Country);
    }

    [Fact]
    public async Task GetManufacturerDetail_WithInvalidId_ShouldReturnBadRequest()
    {
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Manufacturers/invalid");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetManufacturerDetail_WithZeroId_ShouldReturnNotFound()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Manufacturers/00000000-0000-0000-0000-000000000000");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<ManufacturerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetManufacturerDetail_WithNegativeId_ShouldReturnNotFound()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Manufacturers/invalid-guid");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<ManufacturerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetManufacturerDetail_WithManufacturerWithMinimalData_ShouldReturnCorrectly()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var response = await Client.GetAsync($"/api/v1/Manufacturers/{Manufacturer4Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<ManufacturerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var manufacturer = result.Data!;
        TestAssertions.AssertEqual<Guid>(Manufacturer4Id, manufacturer.Id);
        TestAssertions.AssertEqual("Delta Corp", manufacturer.Name);
        TestAssertions.AssertEqual("Munich", manufacturer.City);
        TestAssertions.AssertEqual("Germany", manufacturer.Country);
        Assert.Null(manufacturer.Street);
        Assert.Null(manufacturer.State);
        Assert.Null(manufacturer.ZipCode);
        Assert.Null(manufacturer.Phone);
        Assert.Null(manufacturer.Email);
        Assert.Null(manufacturer.Website);
        Assert.Null(manufacturer.Logo);
    }

    [Fact]
    public async Task GetManufacturerDetail_WithNonExistentTenant_ShouldReturnNotFound()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(Guid.Parse("99999999-9999-9999-9999-999999999999"));

        var response = await Client.GetAsync($"/api/v1/Manufacturers/{Manufacturer1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<ManufacturerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetManufacturerDetail_ResponseStructure_ShouldHaveCorrectSuccessFormat()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Manufacturers/{Manufacturer1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<ManufacturerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task GetManufacturerDetail_ResponseStructure_ShouldHaveCorrectErrorFormat()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Manufacturers/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<ManufacturerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task GetManufacturerDetail_WithLargeId_ShouldHandleGracefully()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Manufacturers/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<ManufacturerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetManufacturerDetail_TenantIsolation_ShouldNotReturnOtherTenantManufacturers()
    {
        await SeedManufacturerTestDataAsync();

        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync("/api/v1/Manufacturers/3");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response1.StatusCode);

        var response2 = await Client.GetAsync("/api/v1/Manufacturers/4");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response3 = await Client.GetAsync("/api/v1/Manufacturers/1");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response3.StatusCode);

        var response4 = await Client.GetAsync("/api/v1/Manufacturers/2");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response4.StatusCode);
    }

    [Fact]
    public async Task GetManufacturerDetail_WithCompleteManufacturerData_ShouldMapAllFields()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Manufacturers/{Manufacturer1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<ManufacturerDetailDto>>(response);
        var manufacturer = result.Data!;

        TestAssertions.AssertTrue(manufacturer.Id != Guid.Empty);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(manufacturer.Name));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(manufacturer.Street));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(manufacturer.City));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(manufacturer.State));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(manufacturer.Country));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(manufacturer.ZipCode));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(manufacturer.Phone));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(manufacturer.Email));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(manufacturer.Website));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(manufacturer.Logo));
    }

    [Fact]
    public async Task GetManufacturerDetail_WithCompleteGermanManufacturerData_ShouldMapAllFields()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var response = await Client.GetAsync($"/api/v1/Manufacturers/{Manufacturer3Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<ManufacturerDetailDto>>(response);
        var manufacturer = result.Data!;

        TestAssertions.AssertEqual<Guid>(Manufacturer3Id, manufacturer.Id);
        TestAssertions.AssertEqual("Gamma Systems", manufacturer.Name);
        TestAssertions.AssertEqual("789 Tech Avenue", manufacturer.Street);
        TestAssertions.AssertEqual("Berlin", manufacturer.City);
        TestAssertions.AssertEqual("Berlin", manufacturer.State);
        TestAssertions.AssertEqual("Germany", manufacturer.Country);
        TestAssertions.AssertEqual("10115", manufacturer.ZipCode);
        TestAssertions.AssertEqual("+49-30-12345678", manufacturer.Phone);
        TestAssertions.AssertEqual("hello@gamma.de", manufacturer.Email);
        TestAssertions.AssertEqual("https://gamma.de", manufacturer.Website);
        TestAssertions.AssertEqual("gamma-logo.png", manufacturer.Logo);
    }

    [Fact]
    public async Task GetManufacturerDetail_WithValidEmailFormat_ShouldReturnCorrectEmail()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Manufacturers/{Manufacturer1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<ManufacturerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var manufacturer = result.Data!;
        TestAssertions.AssertEqual("contact@alpha.com", manufacturer.Email);
        TestAssertions.AssertTrue(manufacturer.Email.Contains("@"));
    }

    [Fact]
    public async Task GetManufacturerDetail_WithValidWebsiteFormat_ShouldReturnCorrectWebsite()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Manufacturers/{Manufacturer1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<ManufacturerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var manufacturer = result.Data!;
        TestAssertions.AssertEqual("https://alpha.com", manufacturer.Website);
        TestAssertions.AssertTrue(manufacturer.Website.StartsWith("https://"));
    }

    [Fact]
    public async Task GetManufacturerDetail_WithValidPhoneFormat_ShouldReturnCorrectPhone()
    {
        await SeedManufacturerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Manufacturers/{Manufacturer1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<ManufacturerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var manufacturer = result.Data!;
        TestAssertions.AssertEqual("+1-555-0101", manufacturer.Phone);
        TestAssertions.AssertTrue(manufacturer.Phone.StartsWith("+"));
    }
}