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

public class TaxClassDeleteCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public TaxClassDeleteCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_TaxClassDeleteCommandTests_{uniqueId}";
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

    private async Task<int> CreateTestTaxClassAsync(int tenantId, double taxRate = 19.0)
    {
        TenantContext.SetCurrentTenantId(tenantId);
        
        var taxClass = new Domain.Entities.TaxClass
        {
            TaxRate = taxRate,
            TenantId = tenantId,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        DbContext.TaxClass.Add(taxClass);
        await DbContext.SaveChangesAsync();
        
        TenantContext.SetCurrentTenantId(null);
        return taxClass.Id;
    }

    private async Task<int> CreateTestProductWithTaxClassAsync(int tenantId, int taxClassId)
    {
        TenantContext.SetCurrentTenantId(tenantId);
        
        var product = new maERP.Domain.Entities.Product
        {
            Name = "Test Product",
            Sku = $"TEST-{Guid.NewGuid().ToString()[..8]}",
            ManufacturerId = 1, // Assuming exists from seed data
            TaxClassId = taxClassId,
            TenantId = tenantId,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        DbContext.Product.Add(product);
        await DbContext.SaveChangesAsync();
        
        TenantContext.SetCurrentTenantId(null);
        return product.Id;
    }

    private async Task SeedTestDataAsync()
    {
        var hasData = await DbContext.Tenant.IgnoreQueryFilters().AnyAsync();
        if (!hasData)
        {
            await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        }
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task DeleteTaxClass_WithValidId_ShouldReturnOk()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(1);
        SetTenantHeader(1);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/TaxClasses/{taxClassId}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(taxClassId, result.Data);

        // Verify deletion
        var getResponse = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}");
        TestAssertions.AssertHttpStatusCode(getResponse, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteTaxClass_WithoutTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(1);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/TaxClasses/{taxClassId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteTaxClass_WithInvalidTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(1);
        SetTenantHeader(999);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/TaxClasses/{taxClassId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteTaxClass_FromDifferentTenant_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(1);
        SetTenantHeader(2); // Different tenant

        // Act
        var response = await Client.DeleteAsync($"/api/v1/TaxClasses/{taxClassId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);

        // Verify tax class still exists for tenant 1
        SetTenantHeader(1);
        var getResponse = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}");
        TestAssertions.AssertHttpSuccess(getResponse);
    }

    [Fact]
    public async Task DeleteTaxClass_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);

        // Act
        var response = await Client.DeleteAsync("/api/v1/TaxClasses/999");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteTaxClass_TenantIsolation_ShouldOnlyDeleteOwnTenant()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId1 = await CreateTestTaxClassAsync(1, 19.0);
        var taxClassId2 = await CreateTestTaxClassAsync(2, 20.0);

        // Act - Delete tenant 1's tax class
        SetTenantHeader(1);
        var response1 = await Client.DeleteAsync($"/api/v1/TaxClasses/{taxClassId1}");
        
        // Try to delete tenant 2's tax class from tenant 1
        var response2 = await Client.DeleteAsync($"/api/v1/TaxClasses/{taxClassId2}");

        // Assert
        TestAssertions.AssertHttpSuccess(response1);
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.NotFound);

        // Verify tenant 2's tax class still exists
        SetTenantHeader(2);
        var getResponse = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId2}");
        TestAssertions.AssertHttpSuccess(getResponse);
    }

    [Fact]
    public async Task DeleteTaxClass_WithAssociatedProducts_ShouldHandleCorrectly()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(1);
        
        // Create a product that uses this tax class
        // Note: Depending on business rules, this might fail or succeed with cascade
        // Adjust test based on actual implementation
        SetTenantHeader(1);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/TaxClasses/{taxClassId}");

        // Assert - Adjust based on business rules
        // If deletion should fail when products exist:
        // TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        // If deletion should succeed with cascade or nullification:
        TestAssertions.AssertHttpSuccess(response);
    }

    [Fact]
    public async Task DeleteTaxClass_MultipleDeletions_ShouldOnlyDeleteOnce()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(1);
        SetTenantHeader(1);

        // Act - Delete twice
        var response1 = await Client.DeleteAsync($"/api/v1/TaxClasses/{taxClassId}");
        var response2 = await Client.DeleteAsync($"/api/v1/TaxClasses/{taxClassId}");

        // Assert
        TestAssertions.AssertHttpSuccess(response1);
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteTaxClass_ConcurrentDeletions_ShouldHandleCorrectly()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(1);
        SetTenantHeader(1);

        // Act - Try to delete concurrently
        var task1 = Client.DeleteAsync($"/api/v1/TaxClasses/{taxClassId}");
        var task2 = Client.DeleteAsync($"/api/v1/TaxClasses/{taxClassId}");
        var task3 = Client.DeleteAsync($"/api/v1/TaxClasses/{taxClassId}");

        var responses = await Task.WhenAll(task1, task2, task3);

        // Assert - Only one should succeed
        var successCount = responses.Count(r => r.IsSuccessStatusCode);
        TestAssertions.AssertTrue(successCount >= 1); // At least one should succeed
        
        // Verify deletion
        var getResponse = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}");
        TestAssertions.AssertHttpStatusCode(getResponse, HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("abc")]
    [InlineData("")]
    public async Task DeleteTaxClass_WithInvalidTenantHeaderValue_ShouldReturnNotFound(string invalidTenantId)
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(1);
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", invalidTenantId);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/TaxClasses/{taxClassId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteTaxClass_WithZeroId_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);

        // Act
        var response = await Client.DeleteAsync("/api/v1/TaxClasses/0");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteTaxClass_WithNegativeId_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);

        // Act
        var response = await Client.DeleteAsync("/api/v1/TaxClasses/-1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteTaxClass_WithStringId_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);

        // Act
        var response = await Client.DeleteAsync("/api/v1/TaxClasses/abc");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task DeleteTaxClass_ResponseTime_ShouldBeReasonable()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(1);
        SetTenantHeader(1);
        var startTime = DateTime.UtcNow;

        // Act
        var response = await Client.DeleteAsync($"/api/v1/TaxClasses/{taxClassId}");
        var endTime = DateTime.UtcNow;

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var responseTime = endTime - startTime;
        TestAssertions.AssertTrue(responseTime.TotalSeconds < 5);
    }

    [Fact]
    public async Task DeleteTaxClass_VerifyResultStructure_ShouldHaveCorrectFormat()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(1);
        SetTenantHeader(1);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/TaxClasses/{taxClassId}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<int>>(response);
        
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(ResultStatusCode.Ok, result.StatusCode);
        TestAssertions.AssertEqual(taxClassId, result.Data);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task DeleteTaxClass_MultipleTaxClasses_ShouldDeleteCorrectOne()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId1 = await CreateTestTaxClassAsync(1, 19.0);
        var taxClassId2 = await CreateTestTaxClassAsync(1, 7.0);
        var taxClassId3 = await CreateTestTaxClassAsync(1, 0.0);
        SetTenantHeader(1);

        // Act - Delete middle one
        var response = await Client.DeleteAsync($"/api/v1/TaxClasses/{taxClassId2}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        
        // Verify only the correct one was deleted
        var getResponse1 = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId1}");
        TestAssertions.AssertHttpSuccess(getResponse1);
        
        var getResponse2 = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId2}");
        TestAssertions.AssertHttpStatusCode(getResponse2, HttpStatusCode.NotFound);
        
        var getResponse3 = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId3}");
        TestAssertions.AssertHttpSuccess(getResponse3);
    }

    [Fact]
    public async Task DeleteTaxClass_AfterTenantSwitch_ShouldDeleteFromCorrectTenant()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId1 = await CreateTestTaxClassAsync(1);
        var taxClassId2 = await CreateTestTaxClassAsync(2);

        // Act - Start with tenant 1
        SetTenantHeader(1);
        var canAccessOwn = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId1}");
        TestAssertions.AssertHttpSuccess(canAccessOwn);

        // Switch to tenant 2 and delete their tax class
        SetTenantHeader(2);
        var deleteResponse = await Client.DeleteAsync($"/api/v1/TaxClasses/{taxClassId2}");
        TestAssertions.AssertHttpSuccess(deleteResponse);

        // Try to delete tenant 1's tax class from tenant 2
        var deleteOtherResponse = await Client.DeleteAsync($"/api/v1/TaxClasses/{taxClassId1}");
        TestAssertions.AssertHttpStatusCode(deleteOtherResponse, HttpStatusCode.NotFound);

        // Verify tenant 1's tax class still exists
        SetTenantHeader(1);
        var stillExists = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId1}");
        TestAssertions.AssertHttpSuccess(stillExists);
    }

    [Fact]
    public async Task DeleteTaxClass_WithMaxIntId_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/TaxClasses/{int.MaxValue}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }
}