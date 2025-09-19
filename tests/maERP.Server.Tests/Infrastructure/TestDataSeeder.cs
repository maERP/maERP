using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.Domain.Constants;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace maERP.Server.Tests.Infrastructure;

public static class TestDataSeeder
{
    private static readonly ConcurrentDictionary<int, bool> _seededContexts = new();

    public static async Task SeedTestDataAsync(ApplicationDbContext context, ITenantContext? tenantContext = null)
    {
        // For InMemory databases in tests, don't rely on static caching as it can cause issues
        // with database state consistency between different HTTP client instances
        var isInMemoryDatabase = context.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";

        // Use database name + context hash for better uniqueness in InMemory scenarios
        var databaseName = isInMemoryDatabase
            ? Environment.GetEnvironmentVariable("TEST_DB_NAME") ?? "InMemoryTest"
            : "unknown";
        var contextKey = $"{databaseName}_{context.GetHashCode()}";
        var contextId = contextKey.GetHashCode();

        if (!isInMemoryDatabase && _seededContexts.ContainsKey(contextId))
        {
            return; // Already seeded (only for non-InMemory databases)
        }

        // Check if data is already seeded to avoid duplicate operations
        var currentTenant = tenantContext?.GetCurrentTenantId();
        tenantContext?.SetCurrentTenantId(null);

        try
        {
            var hasData = await context.Tenant.IgnoreQueryFilters().AnyAsync() ||
                          await context.AiModel.IgnoreQueryFilters().AnyAsync() ||
                          await context.AiPrompt.IgnoreQueryFilters().AnyAsync() ||
                          await context.TaxClass.IgnoreQueryFilters().AnyAsync() ||
                          await context.Warehouse.IgnoreQueryFilters().AnyAsync() ||
                          await context.SalesChannel.IgnoreQueryFilters().AnyAsync();

            if (!hasData)
            {
                SeedTenants(context, tenantContext);
                SeedTaxClasses(context, tenantContext);
                SeedWarehouses(context, tenantContext);
                SeedSalesChannels(context, tenantContext);
                SeedAiModels(context, tenantContext);
                SeedAiPrompts(context, tenantContext);
                await context.SaveChangesAsync();

                // Seed relationships after main entities are saved
                await SeedSalesChannelWarehouseRelationshipsAsync(context, tenantContext);

                // Only cache for non-InMemory databases to avoid state inconsistencies
                if (!isInMemoryDatabase)
                {
                    _seededContexts.TryAdd(contextId, true);
                }
            }
        }
        finally
        {
            tenantContext?.SetCurrentTenantId(currentTenant);
        }
    }

    private static void SeedTenants(ApplicationDbContext context, ITenantContext? tenantContext)
    {
        var tenant1 = new Tenant
        {
            Id = TenantConstants.TestTenant1Id,
            Name = "Test Tenant 1",
            TenantCode = "TEST1",
            IsActive = true,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var tenant2 = new Tenant
        {
            Id = TenantConstants.TestTenant2Id,
            Name = "Test Tenant 2",
            TenantCode = "TEST2",
            IsActive = true,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var tenant3 = new Tenant
        {
            Id = TenantConstants.TestTenant3Id,
            Name = "Test Tenant 3",
            TenantCode = "TEST3",
            IsActive = true,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        context.Tenant.AddRange(tenant1, tenant2, tenant3);
    }

    private static void SeedTaxClasses(ApplicationDbContext context, ITenantContext? tenantContext)
    {
        var taxClass1Tenant1 = new TaxClass
        {
            Id = Guid.Parse("00000001-0001-0001-0001-000000000001"),
            TaxRate = 19.0, // 19% VAT for tenant 1
            TenantId = TenantConstants.TestTenant1Id,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var taxClass2Tenant1 = new TaxClass
        {
            Id = Guid.Parse("00000002-0002-0002-0002-000000000002"),
            TaxRate = 7.0, // 7% reduced VAT for tenant 1
            TenantId = TenantConstants.TestTenant1Id,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var taxClass1Tenant2 = new TaxClass
        {
            Id = Guid.Parse("00000003-0003-0003-0003-000000000003"),
            TaxRate = 20.0, // 20% VAT for tenant 2
            TenantId = TenantConstants.TestTenant2Id,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var taxClass2Tenant2 = new TaxClass
        {
            Id = Guid.Parse("00000004-0004-0004-0004-000000000004"),
            TaxRate = 10.0, // 10% reduced VAT for tenant 2
            TenantId = TenantConstants.TestTenant2Id,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        context.TaxClass.AddRange(taxClass1Tenant1, taxClass2Tenant1, taxClass1Tenant2, taxClass2Tenant2);
    }

    private static void SeedWarehouses(ApplicationDbContext context, ITenantContext? tenantContext)
    {
        var warehouse1Tenant1 = new Warehouse
        {
            Id = Guid.Parse("10000001-0001-0001-0001-000000000001"),
            Name = "Main Warehouse Tenant 1",
            TenantId = TenantConstants.TestTenant1Id,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var warehouse2Tenant1 = new Warehouse
        {
            Id = Guid.Parse("10000002-0002-0002-0002-000000000002"),
            Name = "Secondary Warehouse Tenant 1",
            TenantId = TenantConstants.TestTenant1Id,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var warehouse1Tenant2 = new Warehouse
        {
            Id = Guid.Parse("10000003-0003-0003-0003-000000000003"),
            Name = "Main Warehouse Tenant 2",
            TenantId = TenantConstants.TestTenant2Id,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var warehouse2Tenant2 = new Warehouse
        {
            Id = Guid.Parse("10000004-0004-0004-0004-000000000004"),
            Name = "Secondary Warehouse Tenant 2",
            TenantId = TenantConstants.TestTenant2Id,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        context.Warehouse.AddRange(warehouse1Tenant1, warehouse2Tenant1, warehouse1Tenant2, warehouse2Tenant2);
    }

    private static void SeedSalesChannels(ApplicationDbContext context, ITenantContext? tenantContext)
    {
        var salesChannel1Tenant1 = new maERP.Domain.Entities.SalesChannel
        {
            Id = Guid.Parse("d1d1d1d1-d1d1-d1d1-d1d1-d1d1d1d1d1d1"),
            Name = "WooCommerce Tenant 1",
            Type = SalesChannelType.WooCommerce,
            Url = "https://tenant1.woocommerce.com",
            Username = "tenant1_user",
            Password = "tenant1_pass",
            ImportProducts = true,
            ExportProducts = true,
            ImportCustomers = true,
            ExportCustomers = false,
            ImportOrders = true,
            ExportOrders = false,
            TenantId = TenantConstants.TestTenant1Id,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var salesChannel2Tenant1 = new maERP.Domain.Entities.SalesChannel
        {
            Id = Guid.Parse("d2d2d2d2-d2d2-d2d2-d2d2-d2d2d2d2d2d2"),
            Name = "Shopware6 Tenant 1",
            Type = SalesChannelType.Shopware6,
            Url = "https://tenant1.shopware6.com",
            Username = "tenant1_shopware6",
            Password = "tenant1_shopware6_pass",
            ImportProducts = false,
            ExportProducts = true,
            ImportCustomers = false,
            ExportCustomers = true,
            ImportOrders = false,
            ExportOrders = true,
            TenantId = TenantConstants.TestTenant1Id,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var salesChannel3Tenant2 = new maERP.Domain.Entities.SalesChannel
        {
            Id = Guid.Parse("d3d3d3d3-d3d3-d3d3-d3d3-d3d3d3d3d3d3"),
            Name = "eBay Tenant 2",
            Type = SalesChannelType.eBay,
            Url = "https://tenant2.ebay.com",
            Username = "tenant2_ebay",
            Password = "tenant2_ebay_pass",
            ImportProducts = true,
            ExportProducts = false,
            ImportCustomers = true,
            ExportCustomers = true,
            ImportOrders = true,
            ExportOrders = true,
            TenantId = TenantConstants.TestTenant2Id,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var salesChannel4Tenant2 = new maERP.Domain.Entities.SalesChannel
        {
            Id = Guid.Parse("d4d4d4d4-d4d4-d4d4-d4d4-d4d4d4d4d4d4"),
            Name = "PointOfSale Tenant 2",
            Type = SalesChannelType.PointOfSale,
            Url = "https://tenant2.pos.com",
            Username = "tenant2_pos",
            Password = "tenant2_pos_pass",
            ImportProducts = false,
            ExportProducts = false,
            ImportCustomers = false,
            ExportCustomers = false,
            ImportOrders = true,
            ExportOrders = false,
            TenantId = TenantConstants.TestTenant2Id,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        context.SalesChannel.AddRange(salesChannel1Tenant1, salesChannel2Tenant1, salesChannel3Tenant2, salesChannel4Tenant2);

        // Set up warehouse relationships - must be done after SaveChanges to ensure entities exist
        // These will be set up in a separate call after initial seeding
    }

    public static async Task SeedSalesChannelWarehouseRelationshipsAsync(ApplicationDbContext context, ITenantContext? tenantContext = null)
    {
        var currentTenant = tenantContext?.GetCurrentTenantId();
        tenantContext?.SetCurrentTenantId(null);

        try
        {
            // Check if relationships already exist
            var hasRelationships = await context.SalesChannel
                .IgnoreQueryFilters()
                .Include(sc => sc.Warehouses)
                .AnyAsync(sc => sc.Warehouses.Any());

            if (!hasRelationships)
            {
                var salesChannel1 = await context.SalesChannel
                    .IgnoreQueryFilters()
                    .Include(sc => sc.Warehouses)
                    .FirstAsync(sc => sc.Id == Guid.Parse("d1d1d1d1-d1d1-d1d1-d1d1-d1d1d1d1d1d1"));

                var salesChannel2 = await context.SalesChannel
                    .IgnoreQueryFilters()
                    .Include(sc => sc.Warehouses)
                    .FirstAsync(sc => sc.Id == Guid.Parse("d2d2d2d2-d2d2-d2d2-d2d2-d2d2d2d2d2d2"));

                var salesChannel3 = await context.SalesChannel
                    .IgnoreQueryFilters()
                    .Include(sc => sc.Warehouses)
                    .FirstAsync(sc => sc.Id == Guid.Parse("d3d3d3d3-d3d3-d3d3-d3d3-d3d3d3d3d3d3"));

                // Get warehouses
                var warehouse1Tenant1 = await context.Warehouse
                    .IgnoreQueryFilters()
                    .FirstAsync(w => w.Id == Guid.Parse("10000001-0001-0001-0001-000000000001"));

                var warehouse2Tenant1 = await context.Warehouse
                    .IgnoreQueryFilters()
                    .FirstAsync(w => w.Id == Guid.Parse("10000002-0002-0002-0002-000000000002"));

                var warehouse1Tenant2 = await context.Warehouse
                    .IgnoreQueryFilters()
                    .FirstAsync(w => w.Id == Guid.Parse("10000003-0003-0003-0003-000000000003"));

                // Set up relationships
                salesChannel1.Warehouses.Add(warehouse1Tenant1);
                salesChannel1.Warehouses.Add(warehouse2Tenant1);

                salesChannel2.Warehouses.Add(warehouse1Tenant1);

                salesChannel3.Warehouses.Add(warehouse1Tenant2);

                await context.SaveChangesAsync();
            }
        }
        finally
        {
            tenantContext?.SetCurrentTenantId(currentTenant);
        }
    }

    private static void SeedAiModels(ApplicationDbContext context, ITenantContext? tenantContext)
    {
        var aiModel1Tenant1 = new AiModel
        {
            Id = Guid.Parse("20000001-0001-0001-0001-000000000001"),
            Name = "ChatGPT-4O Tenant 1",
            AiModelType = AiModelType.ChatGpt4O,
            ApiUrl = "https://api.openai.com",
            ApiKey = "test-key-1",
            NCtx = 4096,
            TenantId = TenantConstants.TestTenant1Id, // Explicitly set TenantId
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var aiModel2Tenant1 = new AiModel
        {
            Id = Guid.Parse("20000002-0002-0002-0002-000000000002"),
            Name = "Claude 3.5 Tenant 1",
            AiModelType = AiModelType.Claude35,
            ApiUrl = "https://api.anthropic.com",
            ApiKey = "test-key-2",
            NCtx = 8192,
            TenantId = TenantConstants.TestTenant1Id, // Explicitly set TenantId
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var aiModel1Tenant2 = new AiModel
        {
            Id = Guid.Parse("20000003-0003-0003-0003-000000000003"),
            Name = "ChatGPT-4O Tenant 2",
            AiModelType = AiModelType.ChatGpt4O,
            ApiUrl = "https://api.openai.com",
            ApiKey = "test-key-3",
            NCtx = 4096,
            TenantId = TenantConstants.TestTenant2Id, // Explicitly set TenantId
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        context.AiModel.AddRange(aiModel1Tenant1, aiModel2Tenant1, aiModel1Tenant2);
    }

    private static void SeedAiPrompts(ApplicationDbContext context, ITenantContext? tenantContext)
    {
        var aiPrompt1Tenant1 = new AiPrompt
        {
            Id = Guid.Parse("30000001-0001-0001-0001-000000000001"),
            AiModelId = Guid.Parse("20000001-0001-0001-0001-000000000001"),
            Identifier = "Test Prompt 1 Tenant 1",
            PromptText = "This is a test prompt for tenant 1",
            TenantId = TenantConstants.TestTenant1Id,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var aiPrompt2Tenant1 = new AiPrompt
        {
            Id = Guid.Parse("30000002-0002-0002-0002-000000000002"),
            AiModelId = Guid.Parse("20000002-0002-0002-0002-000000000002"),
            Identifier = "Test Prompt 2 Tenant 1",
            PromptText = "This is another test prompt for tenant 1",
            TenantId = TenantConstants.TestTenant1Id,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var aiPrompt1Tenant2 = new AiPrompt
        {
            Id = Guid.Parse("30000003-0003-0003-0003-000000000003"),
            AiModelId = Guid.Parse("20000003-0003-0003-0003-000000000003"),
            Identifier = "Test Prompt 1 Tenant 2",
            PromptText = "This is a test prompt for tenant 2",
            TenantId = TenantConstants.TestTenant2Id,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        context.AiPrompt.AddRange(aiPrompt1Tenant1, aiPrompt2Tenant1, aiPrompt1Tenant2);
    }
}