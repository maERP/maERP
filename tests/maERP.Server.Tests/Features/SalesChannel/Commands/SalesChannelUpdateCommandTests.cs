#nullable disable
using System.Net;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Enums;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.SalesChannel.Commands;

public class SalesChannelUpdateCommandTests : TenantIsolatedTestBase
{
    // Test Entity IDs
    private static readonly Guid TestWarehouse1Id = new("11111111-1111-1111-1111-111111111111");
    private static readonly Guid TestWarehouse2Id = new("22222222-2222-2222-2222-222222222222");
    private static readonly Guid TestWarehouse3Id = new("33333333-3333-3333-3333-333333333333");
    private static readonly Guid TestSalesChannel1Id = new("44444444-4444-4444-4444-444444444444");
    private static readonly Guid TestSalesChannel2Id = new("55555555-5555-5555-5555-555555555555");
    private static readonly Guid TestSalesChannel3Id = new("66666666-6666-6666-6666-666666666666");

    private async Task SeedTestDataAsync()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            // Check if test sales channels are already seeded to avoid duplicates
            var existingSalesChannel = await DbContext.SalesChannel.IgnoreQueryFilters()
                .FirstOrDefaultAsync(sc => sc.Id == TestSalesChannel1Id);

            if (existingSalesChannel == null)
            {

                // Create test warehouses only if they don't exist
                var existingWarehouse1 = await DbContext.Warehouse.IgnoreQueryFilters()
                    .FirstOrDefaultAsync(w => w.Id == TestWarehouse1Id);
                    
                var warehouse1 = new maERP.Domain.Entities.Warehouse();
                var warehouse2 = new maERP.Domain.Entities.Warehouse();
                var warehouse3 = new maERP.Domain.Entities.Warehouse();

                if (existingWarehouse1 == null)
                {
                    warehouse1 = new maERP.Domain.Entities.Warehouse
                    {
                        Id = TestWarehouse1Id,
                        Name = "Test Warehouse 1",
                        TenantId = TenantConstants.TestTenant1Id
                    };
                    
                    warehouse2 = new maERP.Domain.Entities.Warehouse
                    {
                        Id = TestWarehouse2Id,
                        Name = "Test Warehouse 2", 
                        TenantId = TenantConstants.TestTenant1Id
                    };
                    
                    warehouse3 = new maERP.Domain.Entities.Warehouse
                    {
                        Id = TestWarehouse3Id,
                        Name = "Test Warehouse 3",
                        TenantId = TenantConstants.TestTenant2Id
                    };
                    
                    DbContext.Warehouse.AddRange(warehouse1, warehouse2, warehouse3);
                    await DbContext.SaveChangesAsync();
                }
                else
                {
                    // If warehouses already exist, fetch them from database
                    warehouse1 = await DbContext.Warehouse.IgnoreQueryFilters()
                        .FirstAsync(w => w.Id == TestWarehouse1Id);
                    warehouse2 = await DbContext.Warehouse.IgnoreQueryFilters()
                        .FirstAsync(w => w.Id == TestWarehouse2Id);
                    warehouse3 = await DbContext.Warehouse.IgnoreQueryFilters()
                        .FirstAsync(w => w.Id == TestWarehouse3Id);
                }

                // Create existing sales channels for testing updates
                var salesChannel1 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = TestSalesChannel1Id,
                    Type = SalesChannelType.WooCommerce,
                    Name = "Original WooCommerce Store T1",
                    Url = "https://original.example.com",
                    Username = "originaluser",
                    Password = "originalpass",
                    ImportProducts = true,
                    ExportProducts = false,
                    ImportCustomers = true,
                    ExportCustomers = false,
                    ImportOrders = true,
                    ExportOrders = false,
                    TenantId = TenantConstants.TestTenant1Id,
                    Warehouses = new List<maERP.Domain.Entities.Warehouse> { warehouse1 }
                };

                var salesChannel2 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = TestSalesChannel2Id,
                    Type = SalesChannelType.Shopware6,
                    Name = "Original Shopware Store T1",
                    Url = "https://shopware.example.com",
                    Username = "shopwareuser",
                    Password = "shopwarepass",
                    ImportProducts = false,
                    ExportProducts = true,
                    ImportCustomers = false,
                    ExportCustomers = true,
                    ImportOrders = false,
                    ExportOrders = true,
                    TenantId = TenantConstants.TestTenant1Id
                };

                var salesChannel3 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = TestSalesChannel3Id,
                    Type = SalesChannelType.eBay,
                    Name = "Original eBay Store T2",
                    Url = "https://ebay.example.com",
                    Username = "ebayuser",
                    Password = "ebaypass",
                    ImportProducts = true,
                    ExportProducts = true,
                    ImportCustomers = true,
                    ExportCustomers = true,
                    ImportOrders = true,
                    ExportOrders = true,
                    TenantId = TenantConstants.TestTenant2Id,
                    Warehouses = new List<maERP.Domain.Entities.Warehouse> { warehouse3 }
                };

                DbContext.SalesChannel.AddRange(salesChannel1, salesChannel2, salesChannel3);
                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private SalesChannelInputDto CreateUpdateSalesChannelDto(Guid id)
    {
        return new SalesChannelInputDto
        {
            Id = id,
            SalesChannelType = SalesChannelType.Shopware5,
            Name = "Updated Sales Channel Name",
            Url = "https://updated.example.com",
            Username = "updateduser",
            Password = "updatedpassword123",
            ImportProducts = false,
            ExportProducts = true,
            ImportCustomers = false,
            ExportCustomers = true,
            ImportOrders = false,
            ExportOrders = true,
            WarehouseIds = new List<Guid> { TestWarehouse1Id, TestWarehouse2Id }
        };
    }



    [Fact]
    public async Task UpdateSalesChannel_WithValidData_ShouldReturnSuccess()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateSalesChannelDto(TestSalesChannel1Id);

        var response = await PutAsJsonAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(TestSalesChannel1Id, result.Data);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithValidData_ShouldPersistChanges()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateSalesChannelDto(TestSalesChannel1Id);

        var response = await PutAsJsonAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify through API that changes were persisted
        var getResponse = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesChannelDetail = await ReadResponseAsync<Result<SalesChannelDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesChannelDetail?.Data);
        TestAssertions.AssertEqual(updateDto.Name, salesChannelDetail.Data.Name);
        TestAssertions.AssertEqual(updateDto.Url, salesChannelDetail.Data.Url);
        TestAssertions.AssertEqual(updateDto.Username, salesChannelDetail.Data.Username);
        TestAssertions.AssertEqual(updateDto.Password, salesChannelDetail.Data.Password);
        TestAssertions.AssertEqual(SalesChannelType.Shopware5, salesChannelDetail.Data.SalesChannelType);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var nonExistentId = Guid.NewGuid();
        var updateDto = CreateUpdateSalesChannelDto(nonExistentId); // Use same ID as URL

        var response = await PutAsJsonAsync($"/api/v1/SalesChannels/{nonExistentId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithoutTenantHeader_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        RemoveTenantHeader();
        var updateDto = CreateUpdateSalesChannelDto(TestSalesChannel1Id);

        var response = await PutAsJsonAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithInvalidTenantHeaderFormat_ShouldReturnUnauthorized()
    {
        await SeedTestDataAsync();
        SetInvalidTenantHeaderValue("invalid-guid-format");
        var updateDto = CreateUpdateSalesChannelDto(TestSalesChannel1Id);

        var response = await PutAsJsonAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateSalesChannel_TenantIsolation_ShouldOnlyUpdateOwnTenantData()
    {
        await SeedTestDataAsync();

        var uniqueId1 = Guid.NewGuid().ToString("N")[..8];
        var uniqueId2 = Guid.NewGuid().ToString("N")[..8];

        // Test Tenant 1 updating its own data - should succeed
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto1 = CreateUpdateSalesChannelDto(TestSalesChannel1Id);
        updateDto1.Name = $"Updated-T1-{uniqueId1}";

        var updateOwnResponse = await PutAsJsonAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}", updateDto1);
        TestAssertions.AssertEqual(HttpStatusCode.OK, updateOwnResponse.StatusCode);
        var updateResult = await ReadResponseAsync<Result<Guid>>(updateOwnResponse);
        TestAssertions.AssertTrue(updateResult.Succeeded);

        // Test Tenant 1 trying to update Tenant 2's data - should fail with NotFound
        var updateDto2 = CreateUpdateSalesChannelDto(TestSalesChannel3Id);
        updateDto2.Name = "Should not work - cross tenant update";

        var updateCrossResponse = await PutAsJsonAsync($"/api/v1/SalesChannels/{TestSalesChannel3Id}", updateDto2);
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, updateCrossResponse.StatusCode);

        // Test Tenant 2 updating its own data - should succeed
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var updateDto3 = CreateUpdateSalesChannelDto(TestSalesChannel3Id);
        updateDto3.Name = $"Updated-T2-{uniqueId2}";
        updateDto3.WarehouseIds = new List<Guid> { TestWarehouse3Id };

        var updateOwn2Response = await PutAsJsonAsync($"/api/v1/SalesChannels/{TestSalesChannel3Id}", updateDto3);
        TestAssertions.AssertEqual(HttpStatusCode.OK, updateOwn2Response.StatusCode);
        var updateResult2 = await ReadResponseAsync<Result<Guid>>(updateOwn2Response);
        TestAssertions.AssertTrue(updateResult2.Succeeded);

        // Test Tenant 2 trying to update Tenant 1's data - should fail with NotFound
        var updateDto4 = CreateUpdateSalesChannelDto(TestSalesChannel1Id);
        updateDto4.Name = "Should not work - cross tenant update";

        var updateCross2Response = await PutAsJsonAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}", updateDto4);
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, updateCross2Response.StatusCode);

        // Verify tenant isolation - each tenant can only see their updated data
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var getResponse1 = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertHttpSuccess(getResponse1);
        var salesChannel1 = await ReadResponseAsync<Result<SalesChannelDetailDto>>(getResponse1);
        
        // Tenant 1 should not be able to access Tenant 2's sales channel
        var getTenant2Response = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel3Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, getTenant2Response.StatusCode);

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var getResponse3 = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel3Id}");
        TestAssertions.AssertHttpSuccess(getResponse3);
        var salesChannel3 = await ReadResponseAsync<Result<SalesChannelDetailDto>>(getResponse3);
        
        // Tenant 2 should not be able to access Tenant 1's sales channel
        var getTenant1Response = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, getTenant1Response.StatusCode);

        // Verify that the updates were successful
        TestAssertions.AssertTrue(salesChannel1.Data.Name.Contains($"Updated-T1-{uniqueId1}"), 
            $"Expected name to contain 'Updated-T1-{uniqueId1}', but got: {salesChannel1.Data.Name}");
        TestAssertions.AssertTrue(salesChannel3.Data.Name.Contains($"Updated-T2-{uniqueId2}"), 
            $"Expected name to contain 'Updated-T2-{uniqueId2}', but got: {salesChannel3.Data.Name}");
    }

    [Fact]
    public async Task UpdateSalesChannel_WithMissingRequiredFields_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = new SalesChannelInputDto
        {
            Id = TestSalesChannel1Id,
            // Missing required fields: Name, SalesChannelType
            Url = "https://incomplete.example.com"
        };

        var response = await PutAsJsonAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithInvalidWarehouseIds_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateSalesChannelDto(TestSalesChannel1Id);
        updateDto.WarehouseIds = new List<Guid> { Guid.NewGuid() }; // Non-existent warehouse

        var response = await PutAsJsonAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithCrossTenantWarehouseIds_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateSalesChannelDto(TestSalesChannel1Id);
        updateDto.WarehouseIds = new List<Guid> { TestWarehouse3Id }; // Warehouse belongs to tenant 2

        var response = await PutAsJsonAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithTooLongName_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateSalesChannelDto(TestSalesChannel1Id);
        updateDto.Name = new string('A', 256); // Exceeds 255 character limit

        var response = await PutAsJsonAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(responseContent.Contains("validation errors") || responseContent.Contains("Name"));
    }

    [Fact]
    public async Task UpdateSalesChannel_WithInvalidUrl_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateSalesChannelDto(TestSalesChannel1Id);
        updateDto.Url = "invalid-url";

        var response = await PutAsJsonAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithDuplicateName_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Try to update first sales channel with the name of the second
        var updateDto = CreateUpdateSalesChannelDto(TestSalesChannel1Id);
        updateDto.Name = "Original Shopware Store T1"; // Name of sales channel 2

        var response = await PutAsJsonAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithSameName_ShouldUpdateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Update with the same name should be allowed
        var updateDto = CreateUpdateSalesChannelDto(TestSalesChannel1Id);
        updateDto.Name = "Original WooCommerce Store T1"; // Same name as existing

        var response = await PutAsJsonAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithEmptyWarehouseIds_ShouldUpdateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateSalesChannelDto(TestSalesChannel1Id);
        updateDto.WarehouseIds = new List<Guid>(); // Remove all warehouse associations

        var response = await PutAsJsonAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify no warehouses are associated
        var getResponse = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesChannelDetail = await ReadResponseAsync<Result<SalesChannelDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesChannelDetail?.Data);
        TestAssertions.AssertEqual(0, salesChannelDetail.Data.Warehouses.Count);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithBooleanFlags_ShouldPersistCorrectly()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateSalesChannelDto(TestSalesChannel1Id);
        updateDto.ImportProducts = true;
        updateDto.ExportProducts = false;
        updateDto.ImportCustomers = true;
        updateDto.ExportCustomers = false;
        updateDto.ImportOrders = true;
        updateDto.ExportOrders = false;

        var response = await PutAsJsonAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify boolean flags are persisted correctly
        var getResponse = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesChannelDetail = await ReadResponseAsync<Result<SalesChannelDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesChannelDetail?.Data);

        TestAssertions.AssertTrue(salesChannelDetail.Data.ImportProducts);
        TestAssertions.AssertFalse(salesChannelDetail.Data.ExportProducts);
        TestAssertions.AssertTrue(salesChannelDetail.Data.ImportCustomers);
        TestAssertions.AssertFalse(salesChannelDetail.Data.ExportCustomers);
        TestAssertions.AssertTrue(salesChannelDetail.Data.ImportOrders);
        TestAssertions.AssertFalse(salesChannelDetail.Data.ExportOrders);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithDifferentSalesChannelType_ShouldUpdateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateSalesChannelDto(TestSalesChannel1Id);
        updateDto.SalesChannelType = SalesChannelType.eBay; // Change from WooCommerce to eBay

        var response = await PutAsJsonAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify type change was persisted
        var getResponse = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesChannelDetail = await ReadResponseAsync<Result<SalesChannelDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesChannelDetail?.Data);
        TestAssertions.AssertEqual(SalesChannelType.eBay, salesChannelDetail.Data.SalesChannelType);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithInvalidJson_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var invalidJson = "{ invalid json }";
        var content = new StringContent(invalidJson, System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PutAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithEmptyBody_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var content = new StringContent("", System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PutAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithIdMismatch_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateSalesChannelDto(TestSalesChannel2Id); // DTO has different ID than URL

        var response = await PutAsJsonAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateSalesChannel_WithNonExistentTenant_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetInvalidTenantHeader();
        var updateDto = CreateUpdateSalesChannelDto(TestSalesChannel1Id);

        var response = await PutAsJsonAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateSalesChannel_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateSalesChannelDto(TestSalesChannel1Id);

        var response = await PutAsJsonAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(TestSalesChannel1Id, result.Data);
        TestAssertions.AssertNotNull(result.Messages);
    }
}