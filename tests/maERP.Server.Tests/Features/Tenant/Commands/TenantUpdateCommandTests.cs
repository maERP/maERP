using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.Tenant.Commands;

public class TenantUpdateCommandTests : TenantIsolatedTestBase
{
    private const string AdminUserId = CurrentUserHelper.DefaultUserId;
    private const string RegularUserId = "22222222-2222-2222-2222-222222222222";
    private async Task SeedUserTenantsAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var hasUserTenants = await DbContext.UserTenant.IgnoreQueryFilters().AnyAsync();
            if (!hasUserTenants)
            {
                await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

                // Create a user with RoleManageTenant permission for TestTenant1
                var userTenantWithPermission = new maERP.Domain.Entities.UserTenant
                {
                    UserId = AdminUserId,
                    TenantId = TenantConstants.TestTenant1Id,
                    IsDefault = true,
                    RoleManageTenant = true,
                    RoleManageUser = true
                };

                // Create a user without RoleManageTenant permission for TestTenant1
                var userTenantWithoutPermission = new maERP.Domain.Entities.UserTenant
                {
                    UserId = RegularUserId,
                    TenantId = TenantConstants.TestTenant1Id,
                    IsDefault = false,
                    RoleManageTenant = false,
                    RoleManageUser = false
                };

                // Create a user with permission for TestTenant2
                var userTenant2WithPermission = new maERP.Domain.Entities.UserTenant
                {
                    UserId = AdminUserId,
                    TenantId = TenantConstants.TestTenant2Id,
                    IsDefault = false,
                    RoleManageTenant = true,
                    RoleManageUser = true
                };

                DbContext.UserTenant.AddRange(
                    userTenantWithPermission,
                    userTenantWithoutPermission,
                    userTenant2WithPermission);
                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private TenantInputDto CreateUpdateTenantInput()
    {
        return new TenantInputDto
        {
            Name = "Updated Tenant Name",
            Description = "Updated tenant description",
            IsActive = true,
            CompanyName = "Updated Corp",
            ContactEmail = "updated@tenant.com",
            Phone = "+49 123 456789",
            Website = "https://www.updated-tenant.com",
            Street = "Updated Street 123",
            Street2 = "Building B",
            PostalCode = "54321",
            City = "Updated City",
            State = "Updated State",
            Country = "Updated Country",
            Iban = "DE89370400440532013000"
        };
    }

    private void SimulateAuthenticatedRequest(string userId)
    {
        Client.DefaultRequestHeaders.Remove("X-Test-UserId");
        Client.DefaultRequestHeaders.Remove("X-Test-Unauthenticated");
        Client.DefaultRequestHeaders.Add("X-Test-UserId", userId);
        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Test");
    }

    [Fact]
    public async Task UpdateTenant_WithValidDataAndPermission_ShouldReturnOk()
    {
        await SeedUserTenantsAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        SimulateAuthenticatedRequest(AdminUserId);

        var updateInput = CreateUpdateTenantInput();
        var response = await PutAsJsonAsync($"/api/v1/tenants/{TenantConstants.TestTenant1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual<Guid>(TenantConstants.TestTenant1Id, result.Data);
    }

    [Fact]
    public async Task UpdateTenant_WithoutPermission_ShouldReturnBadRequest()
    {
        await SeedUserTenantsAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        SimulateAuthenticatedRequest(RegularUserId);

        var updateInput = CreateUpdateTenantInput();
        var response = await PutAsJsonAsync($"/api/v1/tenants/{TenantConstants.TestTenant1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var responseContent = await ReadResponseStringAsync(response);
        TestAssertions.AssertNotNull(responseContent);
        TestAssertions.AssertTrue(responseContent.Contains("do not have permission"));
    }

    [Fact]
    public async Task UpdateTenant_WithNonExistentId_ShouldReturnBadRequest()
    {
        await SeedUserTenantsAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        SimulateAuthenticatedRequest(AdminUserId);

        var updateInput = CreateUpdateTenantInput();
        var response = await PutAsJsonAsync($"/api/v1/tenants/{Guid.NewGuid()}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var responseContent = await ReadResponseStringAsync(response);
        TestAssertions.AssertTrue(responseContent.Contains("not found"));
    }

    [Fact]
    public async Task UpdateTenant_WithEmptyName_ShouldReturnBadRequest()
    {
        await SeedUserTenantsAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        SimulateAuthenticatedRequest(AdminUserId);

        var updateInput = CreateUpdateTenantInput();
        updateInput.Name = "";

        var response = await PutAsJsonAsync($"/api/v1/tenants/{TenantConstants.TestTenant1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_WithNullName_ShouldReturnBadRequest()
    {
        await SeedUserTenantsAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        SimulateAuthenticatedRequest(AdminUserId);

        var updateInput = CreateUpdateTenantInput();
        updateInput.Name = null!;

        var response = await PutAsJsonAsync($"/api/v1/tenants/{TenantConstants.TestTenant1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_WithDuplicateNameInSystem_ShouldReturnBadRequest()
    {
        await SeedUserTenantsAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        SimulateAuthenticatedRequest(AdminUserId);

        var updateInput = CreateUpdateTenantInput();
        updateInput.Name = "Test Tenant 2";

        var response = await PutAsJsonAsync($"/api/v1/tenants/{TenantConstants.TestTenant1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateTenant_WithSameName_ShouldReturnOk()
    {
        await SeedUserTenantsAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        SimulateAuthenticatedRequest(AdminUserId);

        var updateInput = CreateUpdateTenantInput();
        updateInput.Name = "Test Tenant 1";

        var response = await PutAsJsonAsync($"/api/v1/tenants/{TenantConstants.TestTenant1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task UpdateTenant_WithInvalidEmail_ShouldReturnBadRequest()
    {
        await SeedUserTenantsAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        SimulateAuthenticatedRequest(AdminUserId);

        var updateInput = CreateUpdateTenantInput();
        updateInput.ContactEmail = "invalid-email-format";

        var response = await PutAsJsonAsync($"/api/v1/tenants/{TenantConstants.TestTenant1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var responseContent = await ReadResponseStringAsync(response);
        TestAssertions.AssertTrue(responseContent.Contains("e-mail") || responseContent.Contains("email"));
    }

    [Fact]
    public async Task UpdateTenant_WithInvalidWebsite_ShouldReturnBadRequest()
    {
        await SeedUserTenantsAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        SimulateAuthenticatedRequest(AdminUserId);

        var updateInput = CreateUpdateTenantInput();
        updateInput.Website = "not-a-valid-url";

        var response = await PutAsJsonAsync($"/api/v1/tenants/{TenantConstants.TestTenant1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var responseContent = await ReadResponseStringAsync(response);
        TestAssertions.AssertTrue(responseContent.Contains("URL") || responseContent.Contains("url"));
    }

    [Fact]
    public async Task UpdateTenant_WithInvalidIban_ShouldReturnBadRequest()
    {
        await SeedUserTenantsAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        SimulateAuthenticatedRequest(AdminUserId);

        var updateInput = CreateUpdateTenantInput();
        updateInput.Iban = "INVALID-IBAN";

        var response = await PutAsJsonAsync($"/api/v1/tenants/{TenantConstants.TestTenant1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var responseContent = await ReadResponseStringAsync(response);
        TestAssertions.AssertTrue(responseContent.Contains("IBAN") || responseContent.Contains("iban"));
    }

    [Fact]
    public async Task UpdateTenant_WithLongName_ShouldReturnBadRequest()
    {
        await SeedUserTenantsAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        SimulateAuthenticatedRequest(AdminUserId);

        var updateInput = CreateUpdateTenantInput();
        updateInput.Name = new string('A', 101);

        var response = await PutAsJsonAsync($"/api/v1/tenants/{TenantConstants.TestTenant1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_WithCompleteValidData_ShouldUpdateAllFields()
    {
        await SeedUserTenantsAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        SimulateAuthenticatedRequest(AdminUserId);

        var updateInput = CreateUpdateTenantInput();
        var response = await PutAsJsonAsync($"/api/v1/tenants/{TenantConstants.TestTenant1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        DbContext.ChangeTracker.Clear();

        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        var updatedTenant = await DbContext.Tenant
            .IgnoreQueryFilters()
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == TenantConstants.TestTenant1Id);

        TenantContext.SetCurrentTenantId(currentTenant);

        TestAssertions.AssertNotNull(updatedTenant);
        TestAssertions.AssertEqual(updateInput.Name, updatedTenant!.Name);
        TestAssertions.AssertEqual(updateInput.Description, updatedTenant.Description);
        TestAssertions.AssertEqual(updateInput.CompanyName, updatedTenant.CompanyName);
        TestAssertions.AssertEqual(updateInput.ContactEmail, updatedTenant.ContactEmail);
        TestAssertions.AssertEqual(updateInput.Phone, updatedTenant.Phone);
        TestAssertions.AssertEqual(updateInput.Website, updatedTenant.Website);
        TestAssertions.AssertEqual(updateInput.Street, updatedTenant.Street);
        TestAssertions.AssertEqual(updateInput.Street2, updatedTenant.Street2);
        TestAssertions.AssertEqual(updateInput.PostalCode, updatedTenant.PostalCode);
        TestAssertions.AssertEqual(updateInput.City, updatedTenant.City);
        TestAssertions.AssertEqual(updateInput.State, updatedTenant.State);
        TestAssertions.AssertEqual(updateInput.Country, updatedTenant.Country);
        TestAssertions.AssertEqual(updateInput.Iban, updatedTenant.Iban);
    }

    [Fact]
    public async Task UpdateTenant_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        await SeedUserTenantsAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        SimulateUnauthenticatedRequest();

        var updateInput = CreateUpdateTenantInput();
        var response = await PutAsJsonAsync($"/api/v1/tenants/{TenantConstants.TestTenant1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_DifferentTenantWithPermission_ShouldUpdateCorrectly()
    {
        await SeedUserTenantsAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);
        SimulateAuthenticatedRequest(AdminUserId);

        var updateInput = new TenantInputDto
        {
            Name = "Updated Tenant 2 Name",
            Description = "Updated tenant 2 description",
            IsActive = true,
            CompanyName = "Tenant 2 Corp",
            ContactEmail = "tenant2@updated.com"
        };

        var response = await PutAsJsonAsync($"/api/v1/tenants/{TenantConstants.TestTenant2Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual<Guid>(TenantConstants.TestTenant2Id, result.Data);
    }

    [Fact]
    public async Task UpdateTenant_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedUserTenantsAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        SimulateAuthenticatedRequest(AdminUserId);

        var updateInput = CreateUpdateTenantInput();
        var response = await PutAsJsonAsync($"/api/v1/tenants/{TenantConstants.TestTenant1Id}", updateInput);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertEqual<Guid>(TenantConstants.TestTenant1Id, result.Data);
    }
}
