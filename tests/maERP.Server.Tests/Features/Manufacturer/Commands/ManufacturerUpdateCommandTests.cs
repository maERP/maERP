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

namespace maERP.Server.Tests.Features.Manufacturer.Commands;

public class ManufacturerUpdateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

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
                    Id = 1,
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
                    TenantId = 1
                };

                var manufacturer2 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = 2,
                    Name = "Original Manufacturer 2",
                    Street = "456 Original Ave",
                    City = "Different City",
                    Country = "Original Country",
                    TenantId = 1
                };

                var manufacturer3Tenant2 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = 3,
                    Name = "Tenant 2 Manufacturer",
                    City = "Tenant 2 City",
                    Country = "Tenant 2 Country",
                    TenantId = 2
                };

                var manufacturer4Tenant2 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = 4,
                    Name = "Another Tenant 2 Manufacturer",
                    City = "Another City T2",
                    Country = "Tenant 2 Country",
                    TenantId = 2
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
            Id = 1,
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
        SetTenantHeader(1);

        var updateInput = CreateUpdateManufacturerInput();
        var response = await PutAsJsonAsync("/api/v1/Manufacturers/1", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(1, result.Data);
    }

    [Fact]
    public async Task UpdateManufacturer_WithoutTenantHeader_ShouldReturnUnauthorized()
    {
        await SeedTestDataAsync();

        var updateInput = CreateUpdateManufacturerInput();
        var response = await PutAsJsonAsync("/api/v1/Manufacturers/1", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateManufacturer_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateInput = CreateUpdateManufacturerInput();
        var response = await PutAsJsonAsync("/api/v1/Manufacturers/999", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateManufacturer_WithWrongTenant_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(2);

        var updateInput = CreateUpdateManufacturerInput();
        updateInput.Id = 1;

        var response = await PutAsJsonAsync("/api/v1/Manufacturers/1", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateManufacturer_WithEmptyName_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateInput = CreateUpdateManufacturerInput();
        updateInput.Name = "";

        var response = await PutAsJsonAsync("/api/v1/Manufacturers/1", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateManufacturer_WithNullName_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateInput = CreateUpdateManufacturerInput();
        updateInput.Name = null!;

        var response = await PutAsJsonAsync("/api/v1/Manufacturers/1", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateManufacturer_WithDuplicateNameInSameTenant_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateInput = CreateUpdateManufacturerInput();
        updateInput.Name = "Original Manufacturer 2";

        var response = await PutAsJsonAsync("/api/v1/Manufacturers/1", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateManufacturer_WithSameName_ShouldReturnOk()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateInput = CreateUpdateManufacturerInput();
        updateInput.Name = "Original Manufacturer 1";

        var response = await PutAsJsonAsync("/api/v1/Manufacturers/1", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task UpdateManufacturer_WithInvalidEmail_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateInput = CreateUpdateManufacturerInput();
        updateInput.Email = "invalid-email-format";

        var response = await PutAsJsonAsync("/api/v1/Manufacturers/1", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateManufacturer_WithInvalidWebsite_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateInput = CreateUpdateManufacturerInput();
        updateInput.Website = "not-a-valid-url";

        var response = await PutAsJsonAsync("/api/v1/Manufacturers/1", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateManufacturer_WithLongName_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateInput = CreateUpdateManufacturerInput();
        updateInput.Name = new string('A', 256);

        var response = await PutAsJsonAsync("/api/v1/Manufacturers/1", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateManufacturer_TenantIsolation_ShouldUpdateOnlyOwnTenantData()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateInput = CreateUpdateManufacturerInput();
        var response = await PutAsJsonAsync("/api/v1/Manufacturers/1", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        var updatedManufacturer = await DbContext.Manufacturer.FindAsync(1);
        TestAssertions.AssertNotNull(updatedManufacturer);
        TestAssertions.AssertEqual("Updated Manufacturer Name", updatedManufacturer!.Name);
        TestAssertions.AssertEqual(1, updatedManufacturer.TenantId);

        var tenant2Manufacturer = await DbContext.Manufacturer.FindAsync(3);
        TestAssertions.AssertNotNull(tenant2Manufacturer);
        TestAssertions.AssertEqual("Tenant 2 Manufacturer", tenant2Manufacturer!.Name);
        TestAssertions.AssertEqual(2, tenant2Manufacturer.TenantId);
    }

    [Fact]
    public async Task UpdateManufacturer_WithTenant2Data_ShouldUpdateCorrectly()
    {
        await SeedTestDataAsync();
        SetTenantHeader(2);

        var updateInput = new ManufacturerInputDto
        {
            Id = 3,
            Name = "Updated Tenant 2 Manufacturer",
            City = "Updated T2 City",
            Country = "Updated T2 Country"
        };

        var response = await PutAsJsonAsync("/api/v1/Manufacturers/3", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(3, result.Data);
    }

    [Fact]
    public async Task UpdateManufacturer_WithNonExistentTenant_ShouldReturnUnauthorized()
    {
        await SeedTestDataAsync();
        SetTenantHeader(999);

        var updateInput = CreateUpdateManufacturerInput();
        var response = await PutAsJsonAsync("/api/v1/Manufacturers/1", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateManufacturer_WithPartialUpdate_ShouldUpdateOnlyProvidedFields()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateInput = new ManufacturerInputDto
        {
            Id = 1,
            Name = "Partially Updated Name",
            City = "Original City",
            Country = "Original Country"
        };

        var response = await PutAsJsonAsync("/api/v1/Manufacturers/1", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        var updatedManufacturer = await DbContext.Manufacturer.FindAsync(1);
        TestAssertions.AssertNotNull(updatedManufacturer);
        TestAssertions.AssertEqual("Partially Updated Name", updatedManufacturer!.Name);
        TestAssertions.AssertEqual("123 Original St", updatedManufacturer.Street);
    }

    [Fact]
    public async Task UpdateManufacturer_WithClearingOptionalFields_ShouldUpdateCorrectly()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateInput = new ManufacturerInputDto
        {
            Id = 1,
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

        var response = await PutAsJsonAsync("/api/v1/Manufacturers/1", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        var updatedManufacturer = await DbContext.Manufacturer.FindAsync(1);
        TestAssertions.AssertNotNull(updatedManufacturer);
        TestAssertions.AssertEqual("Updated Name Only", updatedManufacturer!.Name);
        TestAssertions.AssertEqual("", updatedManufacturer.Street);
        TestAssertions.AssertEqual("", updatedManufacturer.Email);
    }

    [Fact]
    public async Task UpdateManufacturer_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateInput = CreateUpdateManufacturerInput();
        var response = await PutAsJsonAsync("/api/v1/Manufacturers/1", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertEqual(1, result.Data);
    }

    [Fact]
    public async Task UpdateManufacturer_WithGermanData_ShouldUpdateCorrectly()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateInput = new ManufacturerInputDto
        {
            Id = 1,
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

        var response = await PutAsJsonAsync("/api/v1/Manufacturers/1", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertTrue(result.Succeeded);

        var updatedManufacturer = await DbContext.Manufacturer.FindAsync(1);
        TestAssertions.AssertEqual("Deutsche Aktualisierte Fertigung", updatedManufacturer!.Name);
        TestAssertions.AssertEqual("Hamburg", updatedManufacturer.City);
        TestAssertions.AssertEqual("Deutschland", updatedManufacturer.Country);
    }

    [Fact]
    public async Task UpdateManufacturer_TenantIsolation_ShouldNotAccessOtherTenantManufacturers()
    {
        await SeedTestDataAsync();

        SetTenantHeader(1);
        var updateInput1 = CreateUpdateManufacturerInput();
        var response1 = await PutAsJsonAsync("/api/v1/Manufacturers/3", updateInput1);
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response1.StatusCode);

        SetTenantHeader(2);
        var updateInput2 = CreateUpdateManufacturerInput();
        updateInput2.Id = 1;
        var response2 = await PutAsJsonAsync("/api/v1/Manufacturers/1", updateInput2);
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);
    }

    [Fact]
    public async Task UpdateManufacturer_WithCompleteValidData_ShouldUpdateAllFields()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateInput = CreateUpdateManufacturerInput();
        var response = await PutAsJsonAsync("/api/v1/Manufacturers/1", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        var updatedManufacturer = await DbContext.Manufacturer.FindAsync(1);
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
        TestAssertions.AssertEqual(1, updatedManufacturer.TenantId);
    }

    [Fact]
    public async Task UpdateManufacturer_WithZeroId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateInput = CreateUpdateManufacturerInput();
        var response = await PutAsJsonAsync("/api/v1/Manufacturers/0", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task UpdateManufacturer_WithNegativeId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateInput = CreateUpdateManufacturerInput();
        var response = await PutAsJsonAsync("/api/v1/Manufacturers/-1", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task UpdateManufacturer_SameNameDifferentTenants_ShouldAllowUpdate()
    {
        await SeedTestDataAsync();

        SetTenantHeader(1);
        var updateInput1 = new ManufacturerInputDto
        {
            Id = 1,
            Name = "Cross Tenant Name",
            City = "City 1",
            Country = "Country 1"
        };
        var response1 = await PutAsJsonAsync("/api/v1/Manufacturers/1", updateInput1);
        TestAssertions.AssertEqual(HttpStatusCode.OK, response1.StatusCode);

        SetTenantHeader(2);
        var updateInput2 = new ManufacturerInputDto
        {
            Id = 3,
            Name = "Cross Tenant Name",
            City = "City 2",
            Country = "Country 2"
        };
        var response2 = await PutAsJsonAsync("/api/v1/Manufacturers/3", updateInput2);
        TestAssertions.AssertEqual(HttpStatusCode.OK, response2.StatusCode);

        var manufacturer1 = await DbContext.Manufacturer.FindAsync(1);
        var manufacturer3 = await DbContext.Manufacturer.FindAsync(3);

        TestAssertions.AssertEqual("Cross Tenant Name", manufacturer1!.Name);
        TestAssertions.AssertEqual("Cross Tenant Name", manufacturer3!.Name);
        TestAssertions.AssertEqual(1, manufacturer1.TenantId);
        TestAssertions.AssertEqual(2, manufacturer3.TenantId);
    }
}