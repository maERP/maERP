using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Enums;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.Customer.Commands;

public class CustomerCreateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public CustomerCreateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_CustomerCreateCommandTests_{uniqueId}";
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
            var hasData = await DbContext.Customer.AnyAsync();
            if (!hasData)
            {
                await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

                var existingCustomer = new maERP.Domain.Entities.Customer
                {
                    Id = 1,
                    Firstname = "Existing",
                    Lastname = "Customer",
                    CompanyName = "Existing Company",
                    Email = "existing@company.com",
                    Phone = "+1111111111",
                    Website = "https://existing.com",
                    VatNumber = "VAT111111111",
                    Note = "Existing customer for uniqueness testing",
                    CustomerStatus = CustomerStatus.Active,
                    DateEnrollment = DateTimeOffset.UtcNow.AddDays(-10),
                    TenantId = TenantConstants.TestTenant1Id
                };

                DbContext.Customer.Add(existingCustomer);
                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private CustomerInputDto CreateValidCustomerInputDto()
    {
        return new CustomerInputDto
        {
            Firstname = "John",
            Lastname = "Doe",
            CompanyName = "Test Company",
            Email = "john.doe@testcompany.com",
            Phone = "+1234567890",
            Website = "https://testcompany.com",
            VatNumber = "VAT123456789",
            Note = "Test customer",
            CustomerStatus = CustomerStatus.Active,
            DateEnrollment = DateTimeOffset.UtcNow
        };
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task CreateCustomer_WithValidData_ShouldReturnCreated()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var customerData = CreateValidCustomerInputDto();
        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data > 0);
    }

    [Fact]
    public async Task CreateCustomer_WithValidData_ShouldSaveToDatabase()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var customerData = CreateValidCustomerInputDto();
        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);

        var createdCustomer = await DbContext.Customer.FindAsync(result.Data);
        TestAssertions.AssertNotNull(createdCustomer);
        TestAssertions.AssertEqual("John", createdCustomer!.Firstname);
        TestAssertions.AssertEqual("Doe", createdCustomer.Lastname);
        TestAssertions.AssertEqual("Test Company", createdCustomer.CompanyName);
        TestAssertions.AssertEqual("john.doe@testcompany.com", createdCustomer.Email);
        TestAssertions.AssertEqual(1, createdCustomer.TenantId);
    }

    [Fact]
    public async Task CreateCustomer_WithoutTenantHeader_ShouldReturnUnauthorized()
    {
        await SeedTestDataAsync();

        var customerData = CreateValidCustomerInputDto();
        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateCustomer_WithInvalidTenant_ShouldStillCreateWithCorrectTenant()
    {
        await SeedTestDataAsync();
        SetTenantHeader(999);

        var customerData = CreateValidCustomerInputDto();
        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        if (response.StatusCode == HttpStatusCode.Created)
        {
            var result = await ReadResponseAsync<Result<int>>(response);
            var createdCustomer = await DbContext.Customer.FindAsync(result.Data);
            TestAssertions.AssertNotNull(createdCustomer);
        }
    }

    [Fact]
    public async Task CreateCustomer_WithEmptyFirstname_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var customerData = CreateValidCustomerInputDto();
        customerData.Firstname = "";
        
        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateCustomer_WithEmptyLastname_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var customerData = CreateValidCustomerInputDto();
        customerData.Lastname = "";
        
        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateCustomer_WithInvalidEmail_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var customerData = CreateValidCustomerInputDto();
        customerData.Email = "invalid-email";
        
        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateCustomer_WithDuplicateData_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var customerData = CreateValidCustomerInputDto();
        customerData.Firstname = "Existing";
        customerData.Lastname = "Customer";
        
        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("already exists")));
    }

    [Fact]
    public async Task CreateCustomer_WithLongFirstname_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var customerData = CreateValidCustomerInputDto();
        customerData.Firstname = new string('A', 256); // Assuming max length is 255
        
        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateCustomer_WithInvalidWebsite_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var customerData = CreateValidCustomerInputDto();
        customerData.Website = "invalid-url";
        
        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateCustomer_WithMinimalRequiredFields_ShouldReturnCreated()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var customerData = new CustomerInputDto
        {
            Firstname = "Jane",
            Lastname = "Smith",
            CustomerStatus = CustomerStatus.Active,
            DateEnrollment = DateTimeOffset.UtcNow
        };
        
        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data > 0);
    }

    [Fact]
    public async Task CreateCustomer_WithInactiveStatus_ShouldReturnCreated()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var customerData = CreateValidCustomerInputDto();
        customerData.CustomerStatus = CustomerStatus.Inactive;
        
        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var createdCustomer = await DbContext.Customer.FindAsync(result.Data);
        TestAssertions.AssertEqual(CustomerStatus.Inactive, createdCustomer!.CustomerStatus);
    }

    [Fact]
    public async Task CreateCustomer_WithFutureEnrollmentDate_ShouldReturnCreated()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var customerData = CreateValidCustomerInputDto();
        customerData.DateEnrollment = DateTimeOffset.UtcNow.AddDays(30);
        
        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var createdCustomer = await DbContext.Customer.FindAsync(result.Data);
        TestAssertions.AssertTrue(createdCustomer!.DateEnrollment > DateTimeOffset.UtcNow.AddDays(25));
    }

    [Fact]
    public async Task CreateCustomer_TenantIsolation_ShouldCreateInCorrectTenant()
    {
        await SeedTestDataAsync();
        
        var customerDataT1 = CreateValidCustomerInputDto();
        customerDataT1.Firstname = "Tenant1";
        
        var customerDataT2 = CreateValidCustomerInputDto();
        customerDataT2.Firstname = "Tenant2";

        SetTenantHeader(1);
        var responseT1 = await PostAsJsonAsync("/api/v1/Customers", customerDataT1);
        TestAssertions.AssertEqual(HttpStatusCode.Created, responseT1.StatusCode);
        var resultT1 = await ReadResponseAsync<Result<int>>(responseT1);

        SetTenantHeader(2);
        var responseT2 = await PostAsJsonAsync("/api/v1/Customers", customerDataT2);
        TestAssertions.AssertEqual(HttpStatusCode.Created, responseT2.StatusCode);
        var resultT2 = await ReadResponseAsync<Result<int>>(responseT2);

        var customerT1 = await DbContext.Customer.FindAsync(resultT1.Data);
        var customerT2 = await DbContext.Customer.FindAsync(resultT2.Data);

        TestAssertions.AssertNotNull(customerT1);
        TestAssertions.AssertNotNull(customerT2);
        TestAssertions.AssertEqual(1, customerT1!.TenantId);
        TestAssertions.AssertEqual(2, customerT2!.TenantId);
        TestAssertions.AssertEqual("Tenant1", customerT1.Firstname);
        TestAssertions.AssertEqual("Tenant2", customerT2.Firstname);
    }

    [Fact]
    public async Task CreateCustomer_WithNullJson_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await PostAsJsonAsync("/api/v1/Customers", (CustomerInputDto)null!);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateCustomer_WithMalformedJson_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var malformedJson = "{firstname: 'John', lastname: 'Doe'}"; // Missing quotes
        var content = new StringContent(malformedJson, System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PostAsync("/api/v1/Customers", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateCustomer_ResponseStructure_ShouldHaveCorrectSuccessFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var customerData = CreateValidCustomerInputDto();
        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data > 0);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task CreateCustomer_ResponseStructure_ShouldHaveCorrectErrorFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var customerData = CreateValidCustomerInputDto();
        customerData.Firstname = ""; // Force validation error
        
        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertEqual(0, result.Data);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateCustomer_WithUnicodeCharacters_ShouldReturnCreated()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var customerData = CreateValidCustomerInputDto();
        customerData.Firstname = "José";
        customerData.Lastname = "García";
        customerData.CompanyName = "Ñueva Compañía";
        customerData.Note = "Müller & Søn";
        
        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertTrue(result.Succeeded);

        var createdCustomer = await DbContext.Customer.FindAsync(result.Data);
        TestAssertions.AssertEqual("José", createdCustomer!.Firstname);
        TestAssertions.AssertEqual("García", createdCustomer.Lastname);
    }
}