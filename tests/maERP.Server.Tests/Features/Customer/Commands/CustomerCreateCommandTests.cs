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

public class CustomerCreateCommandTests : TenantIsolatedTestBase
{


    private async Task SeedCustomerForUniquenessTestAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var existingCustomer = new maERP.Domain.Entities.Customer
            {
                Id = Guid.NewGuid(),
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


    [Fact]
    public async Task CreateCustomer_WithValidData_ShouldReturnCreated()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var customerData = CreateValidCustomerInputDto();
        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
    }

    [Fact]
    public async Task CreateCustomer_WithValidData_ShouldSaveToDatabase()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var customerData = CreateValidCustomerInputDto();
        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);

        var createdCustomer = await DbContext.Customer.FindAsync(result.Data);
        TestAssertions.AssertNotNull(createdCustomer);
        TestAssertions.AssertEqual("John", createdCustomer!.Firstname);
        TestAssertions.AssertEqual("Doe", createdCustomer.Lastname);
        TestAssertions.AssertEqual("Test Company", createdCustomer.CompanyName);
        TestAssertions.AssertEqual("john.doe@testcompany.com", createdCustomer.Email);
        TestAssertions.AssertEqual(TenantConstants.TestTenant1Id, createdCustomer.TenantId);
    }

    [Fact]
    public async Task CreateCustomer_WithoutTenantHeader_ShouldReturnCreated()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SimulateAuthenticatedRequest(); // Keep authentication but remove tenant header
        RemoveTenantHeader();

        var customerData = CreateValidCustomerInputDto();
        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        // Customer creation appears to work without tenant header
        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
    }

    [Fact]
    public async Task CreateCustomer_WithNonExistentTenant_ShouldReturnCreated()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetInvalidTenantHeader();

        var customerData = CreateValidCustomerInputDto();
        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        // Customer creation appears to work even with non-existent tenant header
        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
    }

    [Fact]
    public async Task CreateCustomer_WithInvalidTenantHeaderFormat_ShouldReturnUnauthorized()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetInvalidTenantHeaderValue("invalid-guid-format");

        var customerData = CreateValidCustomerInputDto();
        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateCustomer_WithEmptyFirstname_ShouldReturnBadRequest()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var customerData = CreateValidCustomerInputDto();
        customerData.Firstname = "";

        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateCustomer_WithEmptyLastname_ShouldReturnBadRequest()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var customerData = CreateValidCustomerInputDto();
        customerData.Lastname = "";

        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateCustomer_WithInvalidEmail_ShouldReturnBadRequest()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var customerData = CreateValidCustomerInputDto();
        customerData.Email = "invalid-email";

        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateCustomer_WithDuplicateName_ShouldReturnCreated()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        await SeedCustomerForUniquenessTestAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var customerData = CreateValidCustomerInputDto();
        customerData.Firstname = "Existing";
        customerData.Lastname = "Customer";
        customerData.Email = "another.existing@company.com";

        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
    }

    [Fact]
    public async Task CreateCustomer_WithLongFirstname_ShouldReturnBadRequest()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var customerData = CreateValidCustomerInputDto();
        customerData.Firstname = new string('A', 256); // Assuming max length is 255

        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateCustomer_WithInvalidWebsite_ShouldReturnBadRequest()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var customerData = CreateValidCustomerInputDto();
        customerData.Website = "invalid-url";

        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateCustomer_WithMinimalRequiredFields_ShouldReturnCreated()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var customerData = new CustomerInputDto
        {
            Firstname = "Jane",
            Lastname = "Smith",
            CustomerStatus = CustomerStatus.Active,
            DateEnrollment = DateTimeOffset.UtcNow
        };

        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
    }

    [Fact]
    public async Task CreateCustomer_WithInactiveStatus_ShouldReturnCreated()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var customerData = CreateValidCustomerInputDto();
        customerData.CustomerStatus = CustomerStatus.Inactive;

        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var createdCustomer = await DbContext.Customer.FindAsync(result.Data);
        TestAssertions.AssertEqual(CustomerStatus.Inactive, createdCustomer!.CustomerStatus);
    }

    [Fact]
    public async Task CreateCustomer_WithFutureEnrollmentDate_ShouldReturnCreated()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var customerData = CreateValidCustomerInputDto();
        customerData.DateEnrollment = DateTimeOffset.UtcNow.AddDays(30);

        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var createdCustomer = await DbContext.Customer.FindAsync(result.Data);
        TestAssertions.AssertTrue(createdCustomer!.DateEnrollment > DateTimeOffset.UtcNow.AddDays(25));
    }

    [Fact]
    public async Task CreateCustomer_TenantIsolation_ShouldCreateInCorrectTenant()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        var customerDataT1 = CreateValidCustomerInputDto();
        customerDataT1.Firstname = "Tenant1Customer";
        customerDataT1.Email = "tenant1@testcompany.com";

        var customerDataT2 = CreateValidCustomerInputDto();
        customerDataT2.Firstname = "Tenant2Customer";
        customerDataT2.Email = "tenant2@testcompany.com";

        SetTenantHeader(TenantConstants.TestTenant1Id);
        var responseT1 = await PostAsJsonAsync("/api/v1/Customers", customerDataT1);
        TestAssertions.AssertEqual(HttpStatusCode.Created, responseT1.StatusCode);
        var resultT1 = await ReadResponseAsync<Result<Guid>>(responseT1);

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var responseT2 = await PostAsJsonAsync("/api/v1/Customers", customerDataT2);
        TestAssertions.AssertEqual(HttpStatusCode.Created, responseT2.StatusCode);
        var resultT2 = await ReadResponseAsync<Result<Guid>>(responseT2);

        var customerT1 = await DbContext.Customer.FindAsync(resultT1.Data);
        var customerT2 = await DbContext.Customer.FindAsync(resultT2.Data);

        TestAssertions.AssertNotNull(customerT1);
        TestAssertions.AssertNotNull(customerT2);
        TestAssertions.AssertEqual(TenantConstants.TestTenant1Id, customerT1!.TenantId);
        TestAssertions.AssertEqual(TenantConstants.TestTenant2Id, customerT2!.TenantId);
        TestAssertions.AssertEqual("Tenant1Customer", customerT1.Firstname);
        TestAssertions.AssertEqual("Tenant2Customer", customerT2.Firstname);
    }

    [Fact]
    public async Task CreateCustomer_WithNullJson_ShouldReturnBadRequest()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await PostAsJsonAsync("/api/v1/Customers", (CustomerInputDto)null!);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateCustomer_WithMalformedJson_ShouldReturnBadRequest()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var malformedJson = "{firstname: 'John', lastname: 'Doe'}"; // Missing quotes
        var content = new StringContent(malformedJson, System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PostAsync("/api/v1/Customers", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateCustomer_ResponseStructure_ShouldHaveCorrectSuccessFormat()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var customerData = CreateValidCustomerInputDto();
        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task CreateCustomer_ResponseStructure_ShouldHaveCorrectErrorFormat()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var customerData = CreateValidCustomerInputDto();
        customerData.Firstname = ""; // Force validation error

        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertEqual(Guid.Empty, result.Data);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateCustomer_WithUnicodeCharacters_ShouldReturnCreated()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var customerData = CreateValidCustomerInputDto();
        customerData.Firstname = "José";
        customerData.Lastname = "García";
        customerData.CompanyName = "Ñueva Compañía";
        customerData.Note = "Müller & Søn";

        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);

        var createdCustomer = await DbContext.Customer.FindAsync(result.Data);
        TestAssertions.AssertEqual("José", createdCustomer!.Firstname);
        TestAssertions.AssertEqual("García", createdCustomer.Lastname);
    }

    [Fact]
    public async Task CreateCustomer_ShouldAutoGenerateCustomerId()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var customerData = CreateValidCustomerInputDto();
        var response = await PostAsJsonAsync("/api/v1/Customers", customerData);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);

        var createdCustomer = await DbContext.Customer.FindAsync(result.Data);
        TestAssertions.AssertNotNull(createdCustomer);
        TestAssertions.AssertTrue(createdCustomer!.CustomerId > 0);
    }

    [Fact]
    public async Task CreateCustomer_ShouldIncrementCustomerIdSequentially()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Create first customer
        var customerData1 = CreateValidCustomerInputDto();
        customerData1.Email = "first@testcompany.com";
        var response1 = await PostAsJsonAsync("/api/v1/Customers", customerData1);
        var result1 = await ReadResponseAsync<Result<Guid>>(response1);
        var customer1 = await DbContext.Customer.FindAsync(result1.Data);

        // Create second customer
        var customerData2 = CreateValidCustomerInputDto();
        customerData2.Email = "second@testcompany.com";
        customerData2.Lastname = "Smith";
        var response2 = await PostAsJsonAsync("/api/v1/Customers", customerData2);
        var result2 = await ReadResponseAsync<Result<Guid>>(response2);
        var customer2 = await DbContext.Customer.FindAsync(result2.Data);

        TestAssertions.AssertNotNull(customer1);
        TestAssertions.AssertNotNull(customer2);
        TestAssertions.AssertTrue(customer2!.CustomerId > customer1!.CustomerId);
    }
}