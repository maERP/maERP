using System.Net;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Xunit;
using maERP.Domain.Enums;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.Customer.Queries;

public class CustomerDetailQueryTests : TenantIsolatedTestBase
{
    // Customer ID constants for tests
    private static readonly Guid Customer1Id = new("11111111-1111-1111-1111-000000000001");
    private static readonly Guid Customer2Id = new("11111111-1111-1111-1111-000000000002");
    private static readonly Guid Customer3Id = new("22222222-2222-2222-2222-000000000003");
    private static readonly Guid CustomerAddress1Id = new("11111111-1111-1111-1111-0000000000a1");
    private static readonly Guid CustomerAddress2Id = new("11111111-1111-1111-1111-0000000000a2");

    private async Task SeedCustomerTestDataAsync()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        var customer1Tenant1 = new maERP.Domain.Entities.Customer
        {
            Id = Customer1Id,
            CustomerId = 1,
            Firstname = "John",
            Lastname = "Doe",
            CompanyName = "Test Company 1",
            Email = "john.doe@testcompany1.com",
            Phone = "+1234567890",
            Website = "https://testcompany1.com",
            VatNumber = "VAT123456789",
            Note = "Test customer for tenant 1",
            CustomerStatus = CustomerStatus.Active,
            DateEnrollment = DateTimeOffset.UtcNow.AddDays(-30),
            TenantId = TenantConstants.TestTenant1Id
        };

        var customer2Tenant1 = new maERP.Domain.Entities.Customer
        {
            Id = Customer2Id,
            CustomerId = 2,
            Firstname = "Jane",
            Lastname = "Smith",
            CompanyName = "Test Company 2",
            Email = "jane.smith@testcompany2.com",
            Phone = "+0987654321",
            Website = "https://testcompany2.com",
            VatNumber = "VAT987654321",
            Note = "Another test customer for tenant 1",
            CustomerStatus = CustomerStatus.Inactive,
            DateEnrollment = DateTimeOffset.UtcNow.AddDays(-60),
            TenantId = TenantConstants.TestTenant1Id
        };

        var customer3Tenant2 = new maERP.Domain.Entities.Customer
        {
            Id = Customer3Id,
            CustomerId = 3,
            Firstname = "Bob",
            Lastname = "Wilson",
            CompanyName = "Tenant 2 Company",
            Email = "bob.wilson@tenant2company.com",
            Phone = "+1122334455",
            Website = "https://tenant2company.com",
            VatNumber = "VAT112233445",
            Note = "Test customer for tenant 2",
            CustomerStatus = CustomerStatus.Active,
            DateEnrollment = DateTimeOffset.UtcNow.AddDays(-15),
            TenantId = TenantConstants.TestTenant2Id
        };

        var customerAddress1 = new maERP.Domain.Entities.CustomerAddress
        {
            Id = CustomerAddress1Id,
            CustomerId = Customer1Id,
            Firstname = "John",
            Lastname = "Doe",
            CompanyName = "Test Company 1",
            Street = "123 Main St",
            HouseNr = "A",
            Zip = "12345",
            City = "Test City",
            DefaultDeliveryAddress = true,
            DefaultInvoiceAddress = true,
            TenantId = TenantConstants.TestTenant1Id
        };

        var customerAddress2 = new maERP.Domain.Entities.CustomerAddress
        {
            Id = CustomerAddress2Id,
            CustomerId = Customer2Id,
            Firstname = "Jane",
            Lastname = "Smith",
            CompanyName = "Test Company 2",
            Street = "456 Oak Ave",
            HouseNr = "B",
            Zip = "67890",
            City = "Another City",
            DefaultDeliveryAddress = true,
            DefaultInvoiceAddress = false,
            TenantId = TenantConstants.TestTenant1Id
        };

        DbContext.Customer.AddRange(customer1Tenant1, customer2Tenant1, customer3Tenant2);
        DbContext.CustomerAddress.AddRange(customerAddress1, customerAddress2);
        await DbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task GetCustomerDetail_WithValidIdAndTenant_ShouldReturnCustomerDetail()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Customers/{Customer1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<CustomerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(Customer1Id, result.Data!.Id);
        TestAssertions.AssertEqual("John", result.Data.Firstname);
        TestAssertions.AssertEqual("Doe", result.Data.Lastname);
        TestAssertions.AssertEqual("Test Company 1", result.Data.CompanyName);
    }

    [Fact]
    public async Task GetCustomerDetail_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers/99999999-9999-9999-9999-999999999999");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<CustomerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetCustomerDetail_WithWrongTenant_ShouldReturnNotFound()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var response = await Client.GetAsync($"/api/v1/Customers/{Customer1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<CustomerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetCustomerDetail_WithoutTenantHeader_ShouldReturnNotFound()
    {
        await SeedCustomerTestDataAsync();
        RemoveTenantHeader();

        var response = await Client.GetAsync($"/api/v1/Customers/{Customer1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<CustomerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetCustomerDetail_WithValidId_ShouldIncludeAllCustomerFields()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Customers/{Customer1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<CustomerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var customer = result.Data!;
        TestAssertions.AssertEqual(Customer1Id, customer.Id);
        TestAssertions.AssertEqual("John", customer.Firstname);
        TestAssertions.AssertEqual("Doe", customer.Lastname);
        TestAssertions.AssertEqual("Test Company 1", customer.CompanyName);
        TestAssertions.AssertEqual("john.doe@testcompany1.com", customer.Email);
        TestAssertions.AssertEqual("+1234567890", customer.Phone);
        TestAssertions.AssertEqual("https://testcompany1.com", customer.Website);
        TestAssertions.AssertEqual("VAT123456789", customer.VatNumber);
        TestAssertions.AssertEqual("Test customer for tenant 1", customer.Note);
        TestAssertions.AssertEqual(CustomerStatus.Active, customer.CustomerStatus);
        TestAssertions.AssertEqual("John Doe", customer.FullName);
    }

    [Fact]
    public async Task GetCustomerDetail_WithCustomerAddresses_ShouldIncludeAddressDetails()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Customers/{Customer1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<CustomerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var customer = result.Data!;
        TestAssertions.AssertNotNull(customer.CustomerAddresses);
        TestAssertions.AssertEqual(1, customer.CustomerAddresses.Count);

        var address = customer.CustomerAddresses.First();
        TestAssertions.AssertEqual(CustomerAddress1Id, address.Id);
        TestAssertions.AssertEqual("John", address.Firstname);
        TestAssertions.AssertEqual("Doe", address.Lastname);
        TestAssertions.AssertEqual("123 Main St", address.Street);
        TestAssertions.AssertEqual("A", address.HouseNr);
        TestAssertions.AssertEqual("12345", address.Zip);
        TestAssertions.AssertEqual("Test City", address.City);
        TestAssertions.AssertTrue(address.DefaultDeliveryAddress);
        TestAssertions.AssertTrue(address.DefaultInvoiceAddress);
    }

    [Fact]
    public async Task GetCustomerDetail_WithTenant2Customer_ShouldReturnCorrectCustomer()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var response = await Client.GetAsync($"/api/v1/Customers/{Customer3Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<CustomerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(Customer3Id, result.Data!.Id);
        TestAssertions.AssertEqual("Bob", result.Data.Firstname);
        TestAssertions.AssertEqual("Wilson", result.Data.Lastname);
        TestAssertions.AssertEqual("Tenant 2 Company", result.Data.CompanyName);
    }

    [Fact]
    public async Task GetCustomerDetail_WithInvalidId_ShouldReturnNotFound()
    {
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers/invalid");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetCustomerDetail_WithZeroId_ShouldReturnBadRequest()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers/00000000-0000-0000-0000-000000000000");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<CustomerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetCustomerDetail_WithNegativeId_ShouldReturnNotFound()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers/invalid");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetCustomerDetail_WithInactiveCustomer_ShouldReturnCorrectly()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Customers/{Customer2Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<CustomerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var customer = result.Data!;
        TestAssertions.AssertEqual(Customer2Id, customer.Id);
        TestAssertions.AssertEqual("Jane", customer.Firstname);
        TestAssertions.AssertEqual("Smith", customer.Lastname);
        TestAssertions.AssertEqual(CustomerStatus.Inactive, customer.CustomerStatus);
    }

    [Fact]
    public async Task GetCustomerDetail_WithNonExistentTenant_ShouldReturnNotFound()
    {
        await SeedCustomerTestDataAsync();
        SetInvalidTenantHeader();

        var response = await Client.GetAsync($"/api/v1/Customers/{Customer1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<CustomerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetCustomerDetail_WithInvalidTenantHeaderFormat_ShouldReturnUnauthorized()
    {
        await SeedCustomerTestDataAsync();
        SetInvalidTenantHeaderValue("invalid-guid-format");

        var response = await Client.GetAsync($"/api/v1/Customers/{Customer1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetCustomerDetail_ResponseStructure_ShouldHaveCorrectSuccessFormat()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Customers/{Customer1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<CustomerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task GetCustomerDetail_ResponseStructure_ShouldHaveCorrectErrorFormat()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers/99999999-9999-9999-9999-999999999999");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<CustomerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task GetCustomerDetail_WithLargeId_ShouldHandleGracefully()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers/99999999-9999-9999-9999-999999999998");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<CustomerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetCustomerDetail_TenantIsolation_ShouldNotReturnOtherTenantCustomers()
    {
        await SeedCustomerTestDataAsync();

        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync($"/api/v1/Customers/{Customer3Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response1.StatusCode);

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await Client.GetAsync($"/api/v1/Customers/{Customer1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);

        var response3 = await Client.GetAsync($"/api/v1/Customers/{Customer2Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response3.StatusCode);
    }

    [Fact]
    public async Task GetCustomerDetail_WithNullCustomerAddresses_ShouldReturnEmptyList()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var response = await Client.GetAsync($"/api/v1/Customers/{Customer3Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<CustomerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data!.CustomerAddresses);
        TestAssertions.AssertEmpty(result.Data.CustomerAddresses);
    }

    [Fact]
    public async Task GetCustomerDetail_WithCompleteCustomerData_ShouldMapAllFields()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Customers/{Customer1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<CustomerDetailDto>>(response);
        var customer = result.Data!;

        TestAssertions.AssertTrue(customer.Id != Guid.Empty);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(customer.Firstname));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(customer.Lastname));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(customer.CompanyName));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(customer.Email));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(customer.Phone));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(customer.Website));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(customer.VatNumber));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(customer.Note));
        TestAssertions.AssertTrue(customer.DateEnrollment != default);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(customer.FullName));
    }

    [Fact]
    public async Task GetCustomerDetail_MultipleAddresses_ShouldReturnAllAddresses()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Customers/{Customer2Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<CustomerDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var customer = result.Data!;
        TestAssertions.AssertNotNull(customer.CustomerAddresses);
        TestAssertions.AssertEqual(1, customer.CustomerAddresses.Count);
    }

    [Fact]
    public async Task GetCustomerDetail_ValidDateEnrollment_ShouldHaveCorrectDateFormat()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Customers/{Customer1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<CustomerDetailDto>>(response);
        var customer = result.Data!;

        TestAssertions.AssertTrue(customer.DateEnrollment != default(DateTimeOffset));
        TestAssertions.AssertTrue(customer.DateEnrollment < DateTimeOffset.UtcNow);
    }

    [Fact]
    public async Task GetCustomerDetail_CustomerStatus_ShouldReturnCorrectEnum()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response1 = await Client.GetAsync($"/api/v1/Customers/{Customer1Id}");
        TestAssertions.AssertHttpSuccess(response1);
        var result1 = await ReadResponseAsync<Result<CustomerDetailDto>>(response1);
        TestAssertions.AssertEqual(CustomerStatus.Active, result1.Data!.CustomerStatus);

        var response2 = await Client.GetAsync($"/api/v1/Customers/{Customer2Id}");
        TestAssertions.AssertHttpSuccess(response2);
        var result2 = await ReadResponseAsync<Result<CustomerDetailDto>>(response2);
        TestAssertions.AssertEqual(CustomerStatus.Inactive, result2.Data!.CustomerStatus);
    }
}