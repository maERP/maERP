using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace maERP.Server.Tests.Features.TaxClass.Commands;

public class TaxClassCreateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public TaxClassCreateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_TaxClassCreateCommandTests_{uniqueId}";
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

    protected void SetInvalidTenantHeader()
    {
        SetTenantHeader(999); // Non-existent tenant ID for testing tenant isolation
    }

    protected async Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value);
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
        var hasData = await DbContext.Tenant.IgnoreQueryFilters().AnyAsync();
        if (!hasData)
        {
            await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        }
    }

    private TaxClassInputDto CreateValidTaxClassDto()
    {
        return new TaxClassInputDto
        {
            TaxRate = 19.0
        };
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task CreateTaxClass_WithValidData_ShouldReturnCreated()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var taxClassDto = CreateValidTaxClassDto();

        // Act
        var response = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data > 0);
    }

    [Fact]
    public async Task CreateTaxClass_WithoutTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassDto = CreateValidTaxClassDto();

        // Act
        var response = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateTaxClass_WithInvalidTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        SetInvalidTenantHeader();
        var taxClassDto = CreateValidTaxClassDto();

        // Act
        var response = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateTaxClass_WithNegativeTaxRate_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var taxClassDto = new TaxClassInputDto
        {
            TaxRate = -5.0
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var content = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(content.Contains("TaxRate"));
    }

    [Fact]
    public async Task CreateTaxClass_WithTaxRateOver100_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var taxClassDto = new TaxClassInputDto
        {
            TaxRate = 150.0
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var content = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(content.Contains("TaxRate"));
    }

    [Fact]
    public async Task CreateTaxClass_WithZeroTaxRate_ShouldSucceed()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var taxClassDto = new TaxClassInputDto
        {
            TaxRate = 0.0
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task CreateTaxClass_TenantIsolation_ShouldCreateSeparatelyForEachTenant()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassDto = CreateValidTaxClassDto();

        // Act - Create for tenant 1
        SetTenantHeader(1);
        var response1 = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);
        var result1 = await ReadResponseAsync<Result<int>>(response1);

        // Act - Create for tenant 2
        SetTenantHeader(2);
        var response2 = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);
        var result2 = await ReadResponseAsync<Result<int>>(response2);

        // Assert
        TestAssertions.AssertHttpStatusCode(response1, HttpStatusCode.Created);
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.Created);
        TestAssertions.AssertNotEqual(result1.Data, result2.Data);

        // Verify tenant isolation - tenant 1 can't access tenant 2's tax class
        SetTenantHeader(1);
        var getResponse = await Client.GetAsync($"/api/v1/TaxClasses/{result2.Data}");
        TestAssertions.AssertHttpStatusCode(getResponse, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateTaxClass_WithDecimalPrecision_ShouldHandleCorrectly()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var taxClassDto = new TaxClassInputDto
        {
            TaxRate = 19.75 // Test decimal precision
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<int>>(response);
        
        // Verify the created tax class has correct tax rate
        var getResponse = await Client.GetAsync($"/api/v1/TaxClasses/{result.Data}");
        var getResult = await ReadResponseAsync<Result<TaxClassDetailDto>>(getResponse);
        TestAssertions.AssertEqual(19.75, getResult.Data!.TaxRate);
    }

    [Fact]
    public async Task CreateTaxClass_MultipleConcurrentRequests_ShouldHandleCorrectly()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var tasks = new List<Task<HttpResponseMessage>>();

        // Act - Create multiple tax classes concurrently
        for (int i = 0; i < 5; i++)
        {
            var taxClassDto = new TaxClassInputDto
            {
                TaxRate = 10.0 + i // Different tax rates
            };
            tasks.Add(PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto));
        }

        var responses = await Task.WhenAll(tasks);

        // Assert
        foreach (var response in responses)
        {
            TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
            var result = await ReadResponseAsync<Result<int>>(response);
            TestAssertions.AssertTrue(result.Succeeded);
            TestAssertions.AssertTrue(result.Data > 0);
        }

        // Verify all were created with unique IDs
        var ids = new HashSet<Guid>();
        foreach (var response in responses)
        {
            var result = await ReadResponseAsync<Result<int>>(response);
            TestAssertions.AssertTrue(ids.Add(result.Data)); // Should return true if unique
        }
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("abc")]
    [InlineData("")]
    public async Task CreateTaxClass_WithInvalidTenantHeaderValue_ShouldReturnNotFound(string invalidTenantId)
    {
        // Arrange
        await SeedTestDataAsync();
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", invalidTenantId);
        var taxClassDto = CreateValidTaxClassDto();

        // Act
        var response = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateTaxClass_WithNullDto_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);

        // Act
        var response = await PostAsJsonAsync<TaxClassInputDto?>("/api/v1/TaxClasses", null);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task CreateTaxClass_ResponseTime_ShouldBeReasonable()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var taxClassDto = CreateValidTaxClassDto();
        var startTime = DateTime.UtcNow;

        // Act
        var response = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);
        var endTime = DateTime.UtcNow;

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var responseTime = endTime - startTime;
        TestAssertions.AssertTrue(responseTime.TotalSeconds < 5); // Should respond within 5 seconds
    }

    [Fact]
    public async Task CreateTaxClass_WithMaxTaxRate_ShouldSucceed()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var taxClassDto = new TaxClassInputDto
        {
            TaxRate = 100.0 // Maximum valid tax rate
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task CreateTaxClass_AfterTenantSwitch_ShouldCreateInCorrectTenant()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassDto1 = new TaxClassInputDto { TaxRate = 15.0 };
        var taxClassDto2 = new TaxClassInputDto { TaxRate = 25.0 };

        // Act - Create in tenant 1
        SetTenantHeader(1);
        var response1 = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto1);
        var result1 = await ReadResponseAsync<Result<int>>(response1);

        // Switch to tenant 2 and create
        SetTenantHeader(2);
        var response2 = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto2);
        var result2 = await ReadResponseAsync<Result<int>>(response2);

        // Assert
        TestAssertions.AssertHttpStatusCode(response1, HttpStatusCode.Created);
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.Created);

        // Verify each tenant can only access their own tax class
        SetTenantHeader(1);
        var get1 = await Client.GetAsync($"/api/v1/TaxClasses/{result1.Data}");
        TestAssertions.AssertHttpSuccess(get1);
        var get2 = await Client.GetAsync($"/api/v1/TaxClasses/{result2.Data}");
        TestAssertions.AssertHttpStatusCode(get2, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateTaxClass_VerifyResultStructure_ShouldHaveCorrectFormat()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var taxClassDto = CreateValidTaxClassDto();

        // Act
        var response = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<int>>(response);
        
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(ResultStatusCode.Created, result.StatusCode);
        TestAssertions.AssertTrue(result.Data > 0);
        TestAssertions.AssertNotNull(result.Messages);
    }
}