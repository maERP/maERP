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

namespace maERP.Server.Tests.Features.Manufacturer.Commands;

public class ManufacturerCreateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;
    private static readonly Guid Manufacturer1Id = Guid.NewGuid();

    public ManufacturerCreateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_ManufacturerCreateCommandTests_{uniqueId}";
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

    protected async Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
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
            var hasData = await DbContext.Manufacturer.AnyAsync();
            if (!hasData)
            {
                await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

                var existingManufacturer = new maERP.Domain.Entities.Manufacturer
                {
                    Id = Manufacturer1Id,
                    Name = "Existing Manufacturer",
                    Street = "123 Existing St",
                    City = "Existing City",
                    Country = "Existing Country",
                    Email = "existing@manufacturer.com",
                    Website = "https://existing.com",
                    TenantId = TenantConstants.TestTenant1Id
                };

                DbContext.Manufacturer.Add(existingManufacturer);
                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private ManufacturerInputDto CreateValidManufacturerInput()
    {
        return new ManufacturerInputDto
        {
            Name = "New Test Manufacturer",
            Street = "456 Test Ave",
            City = "Test City",
            State = "Test State",
            Country = "Test Country",
            ZipCode = "12345",
            Phone = "+1-555-1234",
            Email = "test@newmanufacturer.com",
            Website = "https://newmanufacturer.com",
            Logo = "logo.png"
        };
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task CreateManufacturer_WithValidData_ShouldReturnCreated()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var manufacturerInput = CreateValidManufacturerInput();
        var response = await PostAsJsonAsync("/api/v1/Manufacturers", manufacturerInput);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
    }

    [Fact]
    public async Task CreateManufacturer_WithoutTenantHeader_ShouldReturnUnauthorized()
    {
        await SeedTestDataAsync();

        var manufacturerInput = CreateValidManufacturerInput();
        var response = await PostAsJsonAsync("/api/v1/Manufacturers", manufacturerInput);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateManufacturer_WithEmptyName_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var manufacturerInput = CreateValidManufacturerInput();
        manufacturerInput.Name = "";

        var response = await PostAsJsonAsync("/api/v1/Manufacturers", manufacturerInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateManufacturer_WithNullName_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var manufacturerInput = CreateValidManufacturerInput();
        manufacturerInput.Name = null!;

        var response = await PostAsJsonAsync("/api/v1/Manufacturers", manufacturerInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateManufacturer_WithDuplicateName_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var manufacturerInput = CreateValidManufacturerInput();
        manufacturerInput.Name = "Existing Manufacturer";

        var response = await PostAsJsonAsync("/api/v1/Manufacturers", manufacturerInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateManufacturer_WithInvalidEmail_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var manufacturerInput = CreateValidManufacturerInput();
        manufacturerInput.Email = "invalid-email-format";

        var response = await PostAsJsonAsync("/api/v1/Manufacturers", manufacturerInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateManufacturer_WithInvalidWebsite_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var manufacturerInput = CreateValidManufacturerInput();
        manufacturerInput.Website = "not-a-valid-url";

        var response = await PostAsJsonAsync("/api/v1/Manufacturers", manufacturerInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateManufacturer_WithMinimalData_ShouldReturnCreated()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var manufacturerInput = new ManufacturerInputDto
        {
            Name = "Minimal Manufacturer",
            City = "Test City",
            Country = "Test Country"
        };

        var response = await PostAsJsonAsync("/api/v1/Manufacturers", manufacturerInput);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
    }

    [Fact]
    public async Task CreateManufacturer_WithLongName_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var manufacturerInput = CreateValidManufacturerInput();
        manufacturerInput.Name = new string('A', 256);

        var response = await PostAsJsonAsync("/api/v1/Manufacturers", manufacturerInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateManufacturer_WithTenant1_ShouldBeIsolatedFromTenant2()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var manufacturerInput = CreateValidManufacturerInput();
        manufacturerInput.Name = "Tenant 1 Manufacturer";

        var response = await PostAsJsonAsync("/api/v1/Manufacturers", manufacturerInput);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);
        var manufacturerId = result.Data;

        var manufacturer = await DbContext.Manufacturer.FindAsync(manufacturerId);
        TestAssertions.AssertNotNull(manufacturer);
        TestAssertions.AssertEqual<Guid>(TenantConstants.TestTenant1Id, manufacturer!.TenantId!.Value);
    }

    [Fact]
    public async Task CreateManufacturer_WithTenant2_ShouldBeIsolatedFromTenant1()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var manufacturerInput = CreateValidManufacturerInput();
        manufacturerInput.Name = "Tenant 2 Manufacturer";

        var response = await PostAsJsonAsync("/api/v1/Manufacturers", manufacturerInput);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);
        var manufacturerId = result.Data;

        var manufacturer = await DbContext.Manufacturer.FindAsync(manufacturerId);
        TestAssertions.AssertNotNull(manufacturer);
        TestAssertions.AssertEqual<Guid>(TenantConstants.TestTenant2Id, manufacturer!.TenantId!.Value);
    }

    [Fact]
    public async Task CreateManufacturer_SameNameDifferentTenants_ShouldAllowBoth()
    {
        await SeedTestDataAsync();

        var manufacturerInput = CreateValidManufacturerInput();
        manufacturerInput.Name = "Cross Tenant Manufacturer";

        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await PostAsJsonAsync("/api/v1/Manufacturers", manufacturerInput);
        TestAssertions.AssertEqual(HttpStatusCode.Created, response1.StatusCode);

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await PostAsJsonAsync("/api/v1/Manufacturers", manufacturerInput);
        TestAssertions.AssertEqual(HttpStatusCode.Created, response2.StatusCode);

        var result1 = await ReadResponseAsync<Result<Guid>>(response1);
        var result2 = await ReadResponseAsync<Result<Guid>>(response2);

        TestAssertions.AssertNotEqual(result1.Data, result2.Data);
    }

    [Fact]
    public async Task CreateManufacturer_WithValidGermanData_ShouldReturnCreated()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var manufacturerInput = new ManufacturerInputDto
        {
            Name = "Deutsche Fertigungsgesellschaft",
            Street = "Industriestraße 42",
            City = "München",
            State = "Bayern",
            Country = "Deutschland",
            ZipCode = "80331",
            Phone = "+49-89-123456789",
            Email = "kontakt@deutsche-fertigung.de",
            Website = "https://deutsche-fertigung.de"
        };

        var response = await PostAsJsonAsync("/api/v1/Manufacturers", manufacturerInput);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
    }

    [Fact]
    public async Task CreateManufacturer_WithNonExistentTenant_ShouldReturnUnauthorized()
    {
        await SeedTestDataAsync();
        SetTenantHeader(Guid.Parse("99999999-9999-9999-9999-999999999999"));

        var manufacturerInput = CreateValidManufacturerInput();
        var response = await PostAsJsonAsync("/api/v1/Manufacturers", manufacturerInput);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateManufacturer_WithSpecialCharactersInName_ShouldReturnCreated()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var manufacturerInput = CreateValidManufacturerInput();
        manufacturerInput.Name = "Manufacturer & Co. - Special Chars™";

        var response = await PostAsJsonAsync("/api/v1/Manufacturers", manufacturerInput);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
    }

    [Fact]
    public async Task CreateManufacturer_WithValidPhoneFormats_ShouldReturnCreated()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var manufacturerInput = CreateValidManufacturerInput();
        manufacturerInput.Name = "Phone Test Manufacturer";
        manufacturerInput.Phone = "+49-30-12345678";

        var response = await PostAsJsonAsync("/api/v1/Manufacturers", manufacturerInput);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
    }

    [Fact]
    public async Task CreateManufacturer_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var manufacturerInput = CreateValidManufacturerInput();
        var response = await PostAsJsonAsync("/api/v1/Manufacturers", manufacturerInput);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
    }

    [Fact]
    public async Task CreateManufacturer_WithEmptyOptionalFields_ShouldReturnCreated()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var manufacturerInput = new ManufacturerInputDto
        {
            Name = "Optional Fields Test",
            City = "Test City",
            Country = "Test Country",
            Street = "",
            State = "",
            ZipCode = "",
            Phone = "",
            Email = "",
            Website = "",
            Logo = ""
        };

        var response = await PostAsJsonAsync("/api/v1/Manufacturers", manufacturerInput);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
    }

    [Fact]
    public async Task CreateManufacturer_TenantIsolation_ShouldNotSeeOtherTenantData()
    {
        await SeedTestDataAsync();

        var manufacturerInput = CreateValidManufacturerInput();
        manufacturerInput.Name = "Tenant Isolation Test";

        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await PostAsJsonAsync("/api/v1/Manufacturers", manufacturerInput);
        var result1 = await ReadResponseAsync<Result<Guid>>(response1);
        var manufacturer1Id = result1.Data;

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var manufacturer1FromTenant2 = await DbContext.Manufacturer
            .Where(m => m.Id == manufacturer1Id)
            .FirstOrDefaultAsync();

        Assert.Null(manufacturer1FromTenant2);
    }

    [Fact]
    public async Task CreateManufacturer_WithCompleteValidData_ShouldCreateWithAllFields()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var manufacturerInput = CreateValidManufacturerInput();
        var response = await PostAsJsonAsync("/api/v1/Manufacturers", manufacturerInput);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        var manufacturerId = result.Data;

        var createdManufacturer = await DbContext.Manufacturer.FindAsync(manufacturerId);
        TestAssertions.AssertNotNull(createdManufacturer);
        TestAssertions.AssertEqual(manufacturerInput.Name, createdManufacturer!.Name);
        TestAssertions.AssertEqual(manufacturerInput.Street, createdManufacturer.Street);
        TestAssertions.AssertEqual(manufacturerInput.City, createdManufacturer.City);
        TestAssertions.AssertEqual(manufacturerInput.State, createdManufacturer.State);
        TestAssertions.AssertEqual(manufacturerInput.Country, createdManufacturer.Country);
        TestAssertions.AssertEqual(manufacturerInput.ZipCode, createdManufacturer.ZipCode);
        TestAssertions.AssertEqual(manufacturerInput.Phone, createdManufacturer.Phone);
        TestAssertions.AssertEqual(manufacturerInput.Email, createdManufacturer.Email);
        TestAssertions.AssertEqual(manufacturerInput.Website, createdManufacturer.Website);
        TestAssertions.AssertEqual(manufacturerInput.Logo, createdManufacturer.Logo);
        TestAssertions.AssertEqual<Guid>(TenantConstants.TestTenant1Id, createdManufacturer.TenantId!.Value);
    }
}