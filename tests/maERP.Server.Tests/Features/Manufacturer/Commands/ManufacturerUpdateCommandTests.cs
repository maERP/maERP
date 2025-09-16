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

public class ManufacturerUpdateCommandTests : IDisposable
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

    public ManufacturerUpdateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_ManufacturerUpdateCommandTests_{uniqueId}";
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

    protected async Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        return await Client.PutAsync(requestUri, content);
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

                var manufacturer1 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = Manufacturer1Id,
                    Name = "Original Manufacturer 1",
                    Street = "123 Original St",
                    City = "Original City",
                    State = "Original State",
                    Country = "Original Country",
                    ZipCode = "12345",
                    Phone = "+1-555-0001",
                    Email = "original1@manufacturer.com",
                    Website = "https://original1.com",
                    Logo = "original1-logo.png",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var manufacturer2 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = Manufacturer2Id,
                    Name = "Original Manufacturer 2",
                    Street = "456 Original Ave",
                    City = "Different City",
                    Country = "Original Country",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var manufacturer3Tenant2 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = Manufacturer3Id,
                    Name = "Tenant 2 Manufacturer",
                    City = "Tenant 2 City",
                    Country = "Tenant 2 Country",
                    TenantId = TenantConstants.TestTenant2Id
                };

                var manufacturer4Tenant2 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = Manufacturer4Id,
                    Name = "Another Tenant 2 Manufacturer",
                    City = "Another City T2",
                    Country = "Tenant 2 Country",
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Manufacturer.AddRange(manufacturer1, manufacturer2, manufacturer3Tenant2, manufacturer4Tenant2);
                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private ManufacturerInputDto CreateUpdateManufacturerInput()
    {
        return new ManufacturerInputDto
        {
            Id = Manufacturer1Id,
            Name = "Updated Manufacturer Name",
            Street = "789 Updated Blvd",
            City = "Updated City",
            State = "Updated State",
            Country = "Updated Country",
            ZipCode = "54321",
            Phone = "+1-555-9999",
            Email = "updated@manufacturer.com",
            Website = "https://updated-manufacturer.com",
            Logo = "updated-logo.png"
        };
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task UpdateManufacturer_WithValidData_ShouldReturnOk()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateInput = CreateUpdateManufacturerInput();
        var response = await PutAsJsonAsync($"/api/v1/Manufacturers/{Manufacturer1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual<Guid>(Manufacturer1Id, result.Data);
    }

    [Fact]
    public async Task UpdateManufacturer_WithoutTenantHeader_ShouldReturnUnauthorized()
    {
        await SeedTestDataAsync();

        var updateInput = CreateUpdateManufacturerInput();
        var response = await PutAsJsonAsync($"/api/v1/Manufacturers/{Manufacturer1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateManufacturer_WithNonExistentId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateInput = CreateUpdateManufacturerInput();
        var response = await PutAsJsonAsync($"/api/v1/Manufacturers/{Guid.NewGuid()}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateManufacturer_WithWrongTenant_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var updateInput = CreateUpdateManufacturerInput();
        updateInput.Id = Manufacturer1Id;

        var response = await PutAsJsonAsync($"/api/v1/Manufacturers/{Manufacturer1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateManufacturer_WithEmptyName_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateInput = CreateUpdateManufacturerInput();
        updateInput.Name = "";

        var response = await PutAsJsonAsync($"/api/v1/Manufacturers/{Manufacturer1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateManufacturer_WithNullName_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateInput = CreateUpdateManufacturerInput();
        updateInput.Name = null!;

        var response = await PutAsJsonAsync($"/api/v1/Manufacturers/{Manufacturer1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateManufacturer_WithDuplicateNameInSameTenant_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateInput = CreateUpdateManufacturerInput();
        updateInput.Name = "Original Manufacturer 2";

        var response = await PutAsJsonAsync($"/api/v1/Manufacturers/{Manufacturer1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateManufacturer_WithSameName_ShouldReturnOk()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateInput = CreateUpdateManufacturerInput();
        updateInput.Name = "Original Manufacturer 1";

        var response = await PutAsJsonAsync($"/api/v1/Manufacturers/{Manufacturer1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task UpdateManufacturer_WithInvalidEmail_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateInput = CreateUpdateManufacturerInput();
        updateInput.Email = "invalid-email-format";

        var response = await PutAsJsonAsync($"/api/v1/Manufacturers/{Manufacturer1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateManufacturer_WithInvalidWebsite_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateInput = CreateUpdateManufacturerInput();
        updateInput.Website = "not-a-valid-url";

        var response = await PutAsJsonAsync($"/api/v1/Manufacturers/{Manufacturer1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateManufacturer_WithLongName_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateInput = CreateUpdateManufacturerInput();
        updateInput.Name = new string('A', 256);

        var response = await PutAsJsonAsync($"/api/v1/Manufacturers/{Manufacturer1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateManufacturer_TenantIsolation_ShouldUpdateOnlyOwnTenantData()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateInput = CreateUpdateManufacturerInput();
        var response = await PutAsJsonAsync($"/api/v1/Manufacturers/{Manufacturer1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        var updatedManufacturer = await DbContext.Manufacturer.FindAsync(Manufacturer1Id);
        TestAssertions.AssertNotNull(updatedManufacturer);
        TestAssertions.AssertEqual("Updated Manufacturer Name", updatedManufacturer!.Name);
        TestAssertions.AssertEqual<Guid>(TenantConstants.TestTenant1Id, updatedManufacturer.TenantId!.Value);

        var tenant2Manufacturer = await DbContext.Manufacturer.FindAsync(Manufacturer3Id);
        TestAssertions.AssertNotNull(tenant2Manufacturer);
        TestAssertions.AssertEqual("Tenant 2 Manufacturer", tenant2Manufacturer!.Name);
        TestAssertions.AssertEqual<Guid>(TenantConstants.TestTenant2Id, tenant2Manufacturer.TenantId!.Value);
    }

    [Fact]
    public async Task UpdateManufacturer_WithTenant2Data_ShouldUpdateCorrectly()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var updateInput = new ManufacturerInputDto
        {
            Id = Manufacturer3Id,
            Name = "Updated Tenant 2 Manufacturer",
            City = "Updated T2 City",
            Country = "Updated T2 Country"
        };

        var response = await PutAsJsonAsync($"/api/v1/Manufacturers/{Manufacturer3Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual<Guid>(Manufacturer3Id, result.Data);
    }

    [Fact]
    public async Task UpdateManufacturer_WithNonExistentTenant_ShouldReturnUnauthorized()
    {
        await SeedTestDataAsync();
        SetTenantHeader(Guid.Parse("99999999-9999-9999-9999-999999999999"));

        var updateInput = CreateUpdateManufacturerInput();
        var response = await PutAsJsonAsync($"/api/v1/Manufacturers/{Manufacturer1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateManufacturer_WithPartialUpdate_ShouldUpdateOnlyProvidedFields()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateInput = new ManufacturerInputDto
        {
            Id = Manufacturer1Id,
            Name = "Partially Updated Name",
            Street = "123 Original St",
            City = "Original City",
            State = "Original State",
            Country = "Original Country",
            ZipCode = "12345",
            Phone = "+1234567890",
            Email = "original@example.com",
            Website = "https://original.example.com",
            Logo = "original-logo.png"
        };

        var response = await PutAsJsonAsync($"/api/v1/Manufacturers/{Manufacturer1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Clear the change tracker to ensure we get fresh data from the database
        DbContext.ChangeTracker.Clear();

        var updatedManufacturer = await DbContext.Manufacturer
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == Manufacturer1Id);
        TestAssertions.AssertNotNull(updatedManufacturer);
        TestAssertions.AssertEqual("Partially Updated Name", updatedManufacturer!.Name);
        TestAssertions.AssertEqual("123 Original St", updatedManufacturer.Street);
    }

    [Fact]
    public async Task UpdateManufacturer_WithClearingOptionalFields_ShouldUpdateCorrectly()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateInput = new ManufacturerInputDto
        {
            Id = Manufacturer1Id,
            Name = "Updated Name Only",
            City = "Original City",
            Country = "Original Country",
            Street = "",
            State = "",
            ZipCode = "",
            Phone = "",
            Email = "",
            Website = "",
            Logo = ""
        };

        var response = await PutAsJsonAsync($"/api/v1/Manufacturers/{Manufacturer1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Clear the change tracker to ensure we get fresh data from the database
        DbContext.ChangeTracker.Clear();

        var updatedManufacturer = await DbContext.Manufacturer
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == Manufacturer1Id);
        TestAssertions.AssertNotNull(updatedManufacturer);
        TestAssertions.AssertEqual("Updated Name Only", updatedManufacturer!.Name);
        TestAssertions.AssertEqual("", updatedManufacturer.Street);
        TestAssertions.AssertEqual("", updatedManufacturer.Email);
    }

    [Fact]
    public async Task UpdateManufacturer_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateInput = CreateUpdateManufacturerInput();
        var response = await PutAsJsonAsync($"/api/v1/Manufacturers/{Manufacturer1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertEqual<Guid>(Manufacturer1Id, result.Data);
    }

    [Fact]
    public async Task UpdateManufacturer_WithGermanData_ShouldUpdateCorrectly()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateInput = new ManufacturerInputDto
        {
            Id = Manufacturer1Id,
            Name = "Deutsche Aktualisierte Fertigung",
            Street = "Neue Industriestraße 99",
            City = "Hamburg",
            State = "Hamburg",
            Country = "Deutschland",
            ZipCode = "20095",
            Phone = "+49-40-987654321",
            Email = "kontakt@aktualisiert.de",
            Website = "https://aktualisiert-fertigung.de"
        };

        var response = await PutAsJsonAsync($"/api/v1/Manufacturers/{Manufacturer1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);

        // Clear the change tracker to ensure we get fresh data from the database
        DbContext.ChangeTracker.Clear();

        var updatedManufacturer = await DbContext.Manufacturer
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == Manufacturer1Id);
        TestAssertions.AssertEqual("Deutsche Aktualisierte Fertigung", updatedManufacturer!.Name);
        TestAssertions.AssertEqual("Hamburg", updatedManufacturer.City);
        TestAssertions.AssertEqual("Deutschland", updatedManufacturer.Country);
    }

    [Fact]
    public async Task UpdateManufacturer_TenantIsolation_ShouldNotAccessOtherTenantManufacturers()
    {
        await SeedTestDataAsync();

        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateInput1 = CreateUpdateManufacturerInput();
        var response1 = await PutAsJsonAsync("/api/v1/Manufacturers/3", updateInput1);
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response1.StatusCode);

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var updateInput2 = CreateUpdateManufacturerInput();
        updateInput2.Id = Manufacturer1Id;
        var response2 = await PutAsJsonAsync("/api/v1/Manufacturers/1", updateInput2);
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);
    }

    [Fact]
    public async Task UpdateManufacturer_WithCompleteValidData_ShouldUpdateAllFields()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateInput = CreateUpdateManufacturerInput();
        var response = await PutAsJsonAsync($"/api/v1/Manufacturers/{Manufacturer1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Clear the change tracker to ensure we get fresh data from the database
        DbContext.ChangeTracker.Clear();

        var updatedManufacturer = await DbContext.Manufacturer
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == Manufacturer1Id);
        TestAssertions.AssertNotNull(updatedManufacturer);
        TestAssertions.AssertEqual(updateInput.Name, updatedManufacturer!.Name);
        TestAssertions.AssertEqual(updateInput.Street, updatedManufacturer.Street);
        TestAssertions.AssertEqual(updateInput.City, updatedManufacturer.City);
        TestAssertions.AssertEqual(updateInput.State, updatedManufacturer.State);
        TestAssertions.AssertEqual(updateInput.Country, updatedManufacturer.Country);
        TestAssertions.AssertEqual(updateInput.ZipCode, updatedManufacturer.ZipCode);
        TestAssertions.AssertEqual(updateInput.Phone, updatedManufacturer.Phone);
        TestAssertions.AssertEqual(updateInput.Email, updatedManufacturer.Email);
        TestAssertions.AssertEqual(updateInput.Website, updatedManufacturer.Website);
        TestAssertions.AssertEqual(updateInput.Logo, updatedManufacturer.Logo);
        TestAssertions.AssertEqual<Guid>(TenantConstants.TestTenant1Id, updatedManufacturer.TenantId!.Value);
    }

    [Fact]
    public async Task UpdateManufacturer_WithZeroId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateInput = CreateUpdateManufacturerInput();
        var response = await PutAsJsonAsync("/api/v1/Manufacturers/00000000-0000-0000-0000-000000000000", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateManufacturer_WithNegativeId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateInput = CreateUpdateManufacturerInput();
        var response = await PutAsJsonAsync("/api/v1/Manufacturers/invalid-guid", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateManufacturer_SameNameDifferentTenants_ShouldAllowUpdate()
    {
        await SeedTestDataAsync();

        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateInput1 = new ManufacturerInputDto
        {
            Id = Manufacturer1Id,
            Name = "Cross Tenant Name",
            City = "City 1",
            Country = "Country 1"
        };
        var response1 = await PutAsJsonAsync($"/api/v1/Manufacturers/{Manufacturer1Id}", updateInput1);
        TestAssertions.AssertEqual(HttpStatusCode.OK, response1.StatusCode);

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var updateInput2 = new ManufacturerInputDto
        {
            Id = Manufacturer3Id,
            Name = "Cross Tenant Name",
            City = "City 2",
            Country = "Country 2"
        };
        var response2 = await PutAsJsonAsync($"/api/v1/Manufacturers/{Manufacturer3Id}", updateInput2);
        TestAssertions.AssertEqual(HttpStatusCode.OK, response2.StatusCode);

        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);
        DbContext.ChangeTracker.Clear();

        var manufacturer1 = await DbContext.Manufacturer.AsNoTracking().FirstOrDefaultAsync(m => m.Id == Manufacturer1Id);
        var manufacturer3 = await DbContext.Manufacturer.AsNoTracking().FirstOrDefaultAsync(m => m.Id == Manufacturer3Id);

        TenantContext.SetCurrentTenantId(currentTenant);

        TestAssertions.AssertNotNull(manufacturer1);
        TestAssertions.AssertNotNull(manufacturer3);
        TestAssertions.AssertEqual("Cross Tenant Name", manufacturer1!.Name);
        TestAssertions.AssertEqual("Cross Tenant Name", manufacturer3!.Name);
        TestAssertions.AssertNotNull(manufacturer1.TenantId);
        TestAssertions.AssertNotNull(manufacturer3.TenantId);
        TestAssertions.AssertEqual(TenantConstants.TestTenant1Id, manufacturer1.TenantId!.Value);
        TestAssertions.AssertEqual(TenantConstants.TestTenant2Id, manufacturer3.TenantId!.Value);
    }
}