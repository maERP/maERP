using System.Net;
using maERP.Domain.Constants;
using maERP.Domain.Dtos.Product;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace maERP.Server.Tests.Features.Product.Queries;

public class ProductListQueryTests : TenantIsolatedTestBase
{
    private static readonly Guid Manufacturer1Id = Guid.Parse("50000001-0001-0001-0001-000000000001");
    private static readonly Guid Manufacturer2Id = Guid.Parse("50000002-0002-0002-0002-000000000002");
    private static readonly Guid Product1Id = Guid.NewGuid();
    private static readonly Guid Product2Id = Guid.NewGuid();
    private static readonly Guid Product3Id = Guid.NewGuid();
    private static readonly Guid TaxClass1Id = Guid.NewGuid();

    private async Task SeedProductTestDataAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var hasData = await DbContext.Product.AnyAsync();
            if (!hasData)
            {
                await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

                var manufacturer1 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = Manufacturer1Id,
                    Name = "Test Manufacturer 1",
                    City = "Test City 1",
                    Country = "Test Country 1",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var manufacturer2 = new maERP.Domain.Entities.Manufacturer
                {
                    Id = Manufacturer2Id,
                    Name = "Test Manufacturer 2",
                    City = "Test City 2",
                    Country = "Test Country 2",
                    TenantId = TenantConstants.TestTenant2Id
                };

                var taxClass1 = new maERP.Domain.Entities.TaxClass
                {
                    Id = TaxClass1Id,
                    TaxRate = 19.0,
                    TenantId = TenantConstants.TestTenant1Id
                };

                DbContext.TaxClass.Add(taxClass1);
                DbContext.Manufacturer.AddRange(manufacturer1, manufacturer2);

                var product1Tenant1 = new maERP.Domain.Entities.Product
                {
                    Id = Product1Id,
                    Sku = "TEST-001",
                    Name = "Test Product 1",
                    Description = "Description for product 1",
                    Ean = "1234567890123",
                    Price = 99.99m,
                    Msrp = 119.99m,
                    ManufacturerId = Manufacturer1Id,
                    TaxClassId = TaxClass1Id,
                    TenantId = TenantConstants.TestTenant1Id
                };

                var product2Tenant1 = new maERP.Domain.Entities.Product
                {
                    Id = Product2Id,
                    Sku = "TEST-002",
                    Name = "Another Test Product",
                    Description = "Description for product 2",
                    Ean = "2345678901234",
                    Price = 149.99m,
                    Msrp = 179.99m,
                    ManufacturerId = Manufacturer1Id,
                    TaxClassId = TaxClass1Id,
                    TenantId = TenantConstants.TestTenant1Id
                };

                var product3Tenant2 = new maERP.Domain.Entities.Product
                {
                    Id = Product3Id,
                    Sku = "TEST-003",
                    Name = "Tenant 2 Product",
                    Description = "Product for tenant 2",
                    Ean = "3456789012345",
                    Price = 79.99m,
                    Msrp = 99.99m,
                    ManufacturerId = Manufacturer2Id,
                    TaxClassId = TaxClass1Id,
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Product.AddRange(product1Tenant1, product2Tenant1, product3Tenant2);
                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }


    [Fact]
    public async Task GetProducts_WithValidTenant_ShouldReturnTenantData()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Products");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetProducts_WithDifferentTenant_ShouldReturnOnlyThatTenantData()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var response = await Client.GetAsync("/api/v1/Products");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual("Tenant 2 Product", result.Data?.First().Name);
    }

    [Fact]
    public async Task GetProducts_WithoutTenantHeader_ShouldReturnEmptyResult()
    {
        await SeedProductTestDataAsync();
        RemoveTenantHeader();

        var response = await Client.GetAsync("/api/v1/Products");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetProducts_WithPagination_ShouldRespectPageSize()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Products?pageNumber=0&pageSize=1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual(2, result.TotalCount);
        TestAssertions.AssertEqual(2, result.TotalPages);
    }

    [Fact]
    public async Task GetProducts_WithPaginationSecondPage_ShouldReturnSecondPageData()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Products?pageNumber=1&pageSize=1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual(2, result.TotalCount);
        TestAssertions.AssertEqual(2, result.TotalPages);
    }

    [Fact]
    public async Task GetProducts_WithSearchString_ShouldFilterResults()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Products?searchString=Another");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertTrue(result.Data?.First().Name.Contains("Another") ?? false);
    }

    [Fact]
    public async Task GetProducts_WithSearchStringNoMatch_ShouldReturnEmpty()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Products?searchString=NonexistentProduct");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetProducts_WithOrderByName_ShouldReturnOrderedResults()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Products?orderBy=Name");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);

        var names = result.Data?.Select(x => x.Name).ToList();
        TestAssertions.AssertEqual("Another Test Product", names?[0]);
        TestAssertions.AssertEqual("Test Product 1", names?[1]);
    }

    [Fact]
    public async Task GetProducts_WithOrderByNameDescending_ShouldReturnDescOrderedResults()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Products?orderBy=Name desc");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);

        var names = result.Data?.Select(x => x.Name).ToList();
        TestAssertions.AssertEqual("Test Product 1", names?[0]);
        TestAssertions.AssertEqual("Another Test Product", names?[1]);
    }

    [Fact]
    public async Task GetProducts_WithOrderByPrice_ShouldReturnPriceOrderedResults()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Products?orderBy=Price");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);

        var prices = result.Data?.Select(x => x.Price).ToList();
        TestAssertions.AssertEqual(99.99m, prices?[0]);
        TestAssertions.AssertEqual(149.99m, prices?[1]);
    }

    [Fact]
    public async Task GetProducts_WithMultipleOrderBy_ShouldRespectMultipleSorting()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Products?orderBy=Price,Name");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetProducts_WithInvalidPageNumber_ShouldReturnEmptyResults()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Products?pageNumber=10&pageSize=10");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(2, result.TotalCount);
    }

    [Fact]
    public async Task GetProducts_WithZeroPageSize_ShouldUseDefaultPageSize()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Products?pageSize=0");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetProducts_WithNegativePageNumber_ShouldHandleGracefully()
    {
        SetInvalidTenantHeader();

        var response = await Client.GetAsync("/api/v1/Products?pageNumber=-1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetProducts_WithNonExistentTenant_ShouldReturnEmptyPaginatedResult()
    {
        await SeedProductTestDataAsync();
        SetInvalidTenantHeader();

        var response = await Client.GetAsync("/api/v1/Products");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(0, result.TotalCount);
        TestAssertions.AssertEqual(0, result.TotalPages);
    }

    [Fact]
    public async Task GetProducts_ResponseStructure_ShouldContainRequiredFields()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Products");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);

        var firstProduct = result.Data?.First();
        TestAssertions.AssertNotNull(firstProduct);
        TestAssertions.AssertTrue(firstProduct!.Id != Guid.Empty);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstProduct.Name));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstProduct.Sku));
        TestAssertions.AssertTrue(firstProduct.Price > 0);
    }

    [Fact]
    public async Task GetProducts_WithManufacturerData_ShouldIncludeManufacturerInfo()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Products");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);

        var firstProduct = result.Data?.First();
        TestAssertions.AssertNotNull(firstProduct?.Manufacturer);
        TestAssertions.AssertEqual("Test Manufacturer 1", firstProduct?.Manufacturer?.Name);
        TestAssertions.AssertEqual("Test City 1", firstProduct?.Manufacturer?.City);
    }

    [Fact]
    public async Task GetProducts_WithSpecificSkuSearch_ShouldFilterBySku()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Products?searchString=TEST-001");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual("TEST-001", result.Data?.First().Sku);
    }

    [Fact]
    public async Task GetProducts_WithSkuSearch_ShouldFilterBySku()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Products?searchString=TEST-002");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual("Another Test Product", result.Data?.First().Name);
    }

    [Fact]
    public async Task GetProducts_WithNameSearch_ShouldFilterByName()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Products?searchString=Another");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual("Another Test Product", result.Data?.First().Name);
    }

    [Fact]
    public async Task GetProducts_WithCaseInsensitiveSearch_ShouldReturnResults()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Products?searchString=test");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetProducts_WithPartialSearch_ShouldReturnMatchingResults()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Products?searchString=Test");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetProducts_WithEmptySearchString_ShouldReturnAllResults()
    {
        await SeedProductTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Products?searchString=");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetProducts_WithInvalidTenantHeaderFormat_ShouldReturnUnauthorized()
    {
        await SeedProductTestDataAsync();
        SetInvalidTenantHeaderValue("invalid-guid-format");

        var response = await Client.GetAsync("/api/v1/Products");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetProducts_TenantIsolation_ShouldOnlyReturnOwnTenantData()
    {
        await SeedProductTestDataAsync();

        // Test tenant 1 sees only its products
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync("/api/v1/Products");
        TestAssertions.AssertHttpSuccess(response1);
        var result1 = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response1);
        TestAssertions.AssertNotNull(result1);
        TestAssertions.AssertNotNull(result1.Data);
        TestAssertions.AssertEqual(2, result1.Data?.Count ?? 0);

        // Verify all products belong to tenant 1
        var tenant1Products = result1.Data?.Where(p => p.Name.Contains("Test Product") || p.Name.Contains("Another")).ToList();
        TestAssertions.AssertEqual(2, tenant1Products?.Count ?? 0);

        // Test tenant 2 sees only its products
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await Client.GetAsync("/api/v1/Products");
        TestAssertions.AssertHttpSuccess(response2);
        var result2 = await ReadResponseAsync<PaginatedResult<ProductListDto>>(response2);
        TestAssertions.AssertNotNull(result2);
        TestAssertions.AssertNotNull(result2.Data);
        TestAssertions.AssertEqual(1, result2.Data?.Count ?? 0);
        TestAssertions.AssertEqual("Tenant 2 Product", result2.Data?.First().Name);

        // Verify tenant 2 doesn't see tenant 1 products
        var hasTestProduct1 = result2.Data?.Any(p => p.Name.Contains("Test Product 1")) ?? false;
        var hasAnotherProduct = result2.Data?.Any(p => p.Name.Contains("Another")) ?? false;
        TestAssertions.AssertFalse(hasTestProduct1, "Tenant 2 should not see Tenant 1 products");
        TestAssertions.AssertFalse(hasAnotherProduct, "Tenant 2 should not see Tenant 1 products");
    }
}