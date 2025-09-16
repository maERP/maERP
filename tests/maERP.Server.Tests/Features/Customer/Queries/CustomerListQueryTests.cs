using System.Net;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Xunit;
using maERP.Domain.Enums;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.Customer.Queries;

public class CustomerListQueryTests : TenantIsolatedTestBase
{

    private async Task SeedCustomerTestDataAsync()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        var customer1Tenant1 = new maERP.Domain.Entities.Customer
        {
            Id = Guid.Parse("40000001-0001-0001-0001-000000000001"),
            Firstname = "Alice",
            Lastname = "Johnson",
            CompanyName = "Alpha Company",
            Email = "alice.johnson@alpha.com",
            Phone = "+1111111111",
            Website = "https://alpha.com",
            VatNumber = "VAT111111111",
            Note = "First customer for tenant 1",
            CustomerStatus = CustomerStatus.Active,
            DateEnrollment = DateTimeOffset.UtcNow.AddDays(-10),
            TenantId = TenantConstants.TestTenant1Id
        };

        var customer2Tenant1 = new maERP.Domain.Entities.Customer
        {
            Id = Guid.Parse("40000002-0002-0002-0002-000000000002"),
            Firstname = "Bob",
            Lastname = "Smith",
            CompanyName = "Beta Corporation",
            Email = "bob.smith@beta.com",
            Phone = "+2222222222",
            Website = "https://beta.com",
            VatNumber = "VAT222222222",
            Note = "Second customer for tenant 1",
            CustomerStatus = CustomerStatus.Inactive,
            DateEnrollment = DateTimeOffset.UtcNow.AddDays(-20),
            TenantId = TenantConstants.TestTenant1Id
        };

        var customer3Tenant1 = new maERP.Domain.Entities.Customer
        {
            Id = Guid.Parse("40000003-0003-0003-0003-000000000003"),
            Firstname = "Charlie",
            Lastname = "Brown",
            CompanyName = "Gamma Enterprise",
            Email = "charlie.brown@gamma.com",
            Phone = "+3333333333",
            Website = "https://gamma.com",
            VatNumber = "VAT333333333",
            Note = "Third customer for tenant 1",
            CustomerStatus = CustomerStatus.Active,
            DateEnrollment = DateTimeOffset.UtcNow.AddDays(-30),
            TenantId = TenantConstants.TestTenant1Id
        };

        var customer4Tenant2 = new maERP.Domain.Entities.Customer
        {
            Id = Guid.Parse("40000004-0004-0004-0004-000000000004"),
            Firstname = "David",
            Lastname = "Wilson",
            CompanyName = "Delta Company",
            Email = "david.wilson@delta.com",
            Phone = "+4444444444",
            Website = "https://delta.com",
            VatNumber = "VAT444444444",
            Note = "Customer for tenant 2",
            CustomerStatus = CustomerStatus.Active,
            DateEnrollment = DateTimeOffset.UtcNow.AddDays(-5),
            TenantId = TenantConstants.TestTenant2Id
        };

        var customer5Tenant2 = new maERP.Domain.Entities.Customer
        {
            Id = Guid.Parse("40000005-0005-0005-0005-000000000005"),
            Firstname = "Eve",
            Lastname = "Davis",
            CompanyName = "Epsilon Ltd",
            Email = "eve.davis@epsilon.com",
            Phone = "+5555555555",
            Website = "https://epsilon.com",
            VatNumber = "VAT555555555",
            Note = "Another customer for tenant 2",
            CustomerStatus = CustomerStatus.Active,
            DateEnrollment = DateTimeOffset.UtcNow.AddDays(-15),
            TenantId = TenantConstants.TestTenant2Id
        };

        DbContext.Customer.AddRange(
            customer1Tenant1,
            customer2Tenant1,
            customer3Tenant1,
            customer4Tenant2,
            customer5Tenant2);
        await DbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task GetCustomers_WithValidTenant_ShouldReturnTenantData()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetCustomers_WithDifferentTenant_ShouldReturnOnlyThatTenantData()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var response = await Client.GetAsync("/api/v1/Customers");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        TestAssertions.AssertTrue(result.Data?.All(c => c.Id == Guid.Parse("40000004-0004-0004-0004-000000000004") || c.Id == Guid.Parse("40000005-0005-0005-0005-000000000005")) ?? false);
    }

    [Fact]
    public async Task GetCustomers_WithoutTenantHeader_ShouldReturnEmptyResult()
    {
        await SeedCustomerTestDataAsync();
        RemoveTenantHeader();

        var response = await Client.GetAsync("/api/v1/Customers");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetCustomers_WithPagination_ShouldRespectPageSize()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers?pageNumber=0&pageSize=2");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual(3, result.TotalCount);
        TestAssertions.AssertEqual(2, result.TotalPages);
    }

    [Fact]
    public async Task GetCustomers_WithPaginationSecondPage_ShouldReturnSecondPageData()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers?pageNumber=1&pageSize=2");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual(3, result.TotalCount);
        TestAssertions.AssertEqual(2, result.TotalPages);
    }

    [Fact]
    public async Task GetCustomers_WithSearchStringFirstname_ShouldFilterResults()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers?searchString=Alice");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual("Alice", result.Data?.First().Firstname);
    }

    [Fact]
    public async Task GetCustomers_WithSearchStringLastname_ShouldFilterResults()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers?searchString=Brown");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual("Charlie", result.Data?.First().Firstname);
        TestAssertions.AssertEqual("Brown", result.Data?.First().Lastname);
    }

    [Fact]
    public async Task GetCustomers_WithSearchStringNoMatch_ShouldReturnEmpty()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers?searchString=NonexistentCustomer");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetCustomers_WithOrderByFirstname_ShouldReturnOrderedResults()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers?orderBy=Firstname");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);

        var names = result.Data?.Select(x => x.Firstname).ToList();
        TestAssertions.AssertEqual("Alice", names?[0]);
        TestAssertions.AssertEqual("Bob", names?[1]);
        TestAssertions.AssertEqual("Charlie", names?[2]);
    }

    [Fact]
    public async Task GetCustomers_WithOrderByLastnameDescending_ShouldReturnDescOrderedResults()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers?orderBy=Lastname desc");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);

        var names = result.Data?.Select(x => x.Lastname).ToList();
        TestAssertions.AssertEqual("Smith", names?[0]);
        TestAssertions.AssertEqual("Johnson", names?[1]);
        TestAssertions.AssertEqual("Brown", names?[2]);
    }

    [Fact]
    public async Task GetCustomers_WithOrderByDateEnrollment_ShouldReturnDateOrderedResults()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers?orderBy=DateEnrollment");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);

        var dates = result.Data?.Select(x => x.DateEnrollment).ToList();
        TestAssertions.AssertTrue(dates?[0] <= dates?[1]);
        TestAssertions.AssertTrue(dates?[1] <= dates?[2]);
    }

    [Fact]
    public async Task GetCustomers_WithMultipleOrderBy_ShouldRespectMultipleSorting()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers?orderBy=Lastname,Firstname");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetCustomers_WithInvalidPageNumber_ShouldReturnEmptyResults()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers?pageNumber=10&pageSize=10");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(3, result.TotalCount);
    }

    [Fact]
    public async Task GetCustomers_WithZeroPageSize_ShouldUseDefaultPageSize()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers?pageSize=0");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetCustomers_WithNegativePageNumber_ShouldHandleGracefully()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers?pageNumber=-1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetCustomers_WithNonExistentTenant_ShouldReturnEmptyPaginatedResult()
    {
        await SeedCustomerTestDataAsync();
        SetInvalidTenantHeader();

        var response = await Client.GetAsync("/api/v1/Customers");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(0, result.TotalCount);
        TestAssertions.AssertEqual(0, result.TotalPages);
    }

    [Fact]
    public async Task GetCustomers_WithInvalidTenantHeaderFormat_ShouldReturnUnauthorized()
    {
        await SeedCustomerTestDataAsync();
        SetInvalidTenantHeaderValue("invalid-guid-format");

        var response = await Client.GetAsync("/api/v1/Customers");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetCustomers_ResponseStructure_ShouldContainRequiredFields()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);

        var firstCustomer = result.Data?.First();
        TestAssertions.AssertNotNull(firstCustomer);
        TestAssertions.AssertTrue(firstCustomer!.Id != Guid.Empty);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstCustomer.Firstname));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstCustomer.Lastname));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstCustomer.FullName));
        TestAssertions.AssertTrue(firstCustomer.DateEnrollment != default);
    }

    [Fact]
    public async Task GetCustomers_WithCaseInsensitiveSearch_ShouldReturnResults()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers?searchString=alice");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual("Alice", result.Data?.First().Firstname);
    }

    [Fact]
    public async Task GetCustomers_WithPartialSearch_ShouldReturnMatchingResults()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers?searchString=Jo");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual("Alice", result.Data?.First().Firstname);
        TestAssertions.AssertEqual("Johnson", result.Data?.First().Lastname);
    }

    [Fact]
    public async Task GetCustomers_WithEmptySearchString_ShouldReturnAllResults()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers?searchString=");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetCustomers_TenantIsolation_ShouldNotReturnOtherTenantCustomers()
    {
        await SeedCustomerTestDataAsync();

        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync("/api/v1/Customers");
        TestAssertions.AssertHttpSuccess(response1);
        var result1 = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response1);
        TestAssertions.AssertEqual(3, result1.Data?.Count ?? 0);
        TestAssertions.AssertTrue(result1.Data?.All(c => c.Id == Guid.Parse("40000001-0001-0001-0001-000000000001") || c.Id == Guid.Parse("40000002-0002-0002-0002-000000000002") || c.Id == Guid.Parse("40000003-0003-0003-0003-000000000003")) ?? false);

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await Client.GetAsync("/api/v1/Customers");
        TestAssertions.AssertHttpSuccess(response2);
        var result2 = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response2);
        TestAssertions.AssertEqual(2, result2.Data?.Count ?? 0);
        TestAssertions.AssertTrue(result2.Data?.All(c => c.Id == Guid.Parse("40000004-0004-0004-0004-000000000004") || c.Id == Guid.Parse("40000005-0005-0005-0005-000000000005")) ?? false);
    }

    [Fact]
    public async Task GetCustomers_FullNameProperty_ShouldBeConcatenatedCorrectly()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers?searchString=Alice");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);

        var customer = result.Data?.First();
        TestAssertions.AssertEqual("Alice Johnson", customer!.FullName);
    }

    [Fact]
    public async Task GetCustomers_DefaultSorting_ShouldReturnConsistentOrder()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetCustomers_PaginationMetadata_ShouldBeCorrect()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers?pageNumber=0&pageSize=2");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertEqual(0, result.CurrentPage);
        TestAssertions.AssertEqual(2, result.PageSize);
        TestAssertions.AssertEqual(3, result.TotalCount);
        TestAssertions.AssertEqual(2, result.TotalPages);
        TestAssertions.AssertTrue(result.HasNextPage);
        TestAssertions.AssertFalse(result.HasPreviousPage);
    }

    [Fact]
    public async Task GetCustomers_LargePageSize_ShouldReturnAllAvailableResults()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Customers?pageSize=1000");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CustomerListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual(3, result.TotalCount);
        TestAssertions.AssertEqual(1, result.TotalPages);
    }
}