using maERP.Domain.Entities;
using maERP.Domain.Enums;
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
        // Use context hash code to track seeded contexts
        var contextId = context.GetHashCode();

        if (_seededContexts.ContainsKey(contextId))
        {
            return; // Already seeded
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
                          await context.Warehouse.IgnoreQueryFilters().AnyAsync();

            if (!hasData)
            {
                SeedTenants(context, tenantContext);
                SeedTaxClasses(context, tenantContext);
                SeedWarehouses(context, tenantContext);
                SeedAiModels(context, tenantContext);
                SeedAiPrompts(context, tenantContext);
                await context.SaveChangesAsync();

                _seededContexts.TryAdd(contextId, true);
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
            Id = 1,
            Name = "Test Tenant 1",
            TenantCode = "TEST1",
            IsActive = true,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var tenant2 = new Tenant
        {
            Id = 2,
            Name = "Test Tenant 2",
            TenantCode = "TEST2",
            IsActive = true,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        context.Tenant.AddRange(tenant1, tenant2);
    }

    private static void SeedTaxClasses(ApplicationDbContext context, ITenantContext? tenantContext)
    {
        var taxClass1Tenant1 = new TaxClass
        {
            Id = 1,
            TaxRate = 19.0, // 19% VAT for tenant 1
            TenantId = 1,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var taxClass2Tenant1 = new TaxClass
        {
            Id = 2,
            TaxRate = 7.0, // 7% reduced VAT for tenant 1
            TenantId = 1,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var taxClass1Tenant2 = new TaxClass
        {
            Id = 3,
            TaxRate = 20.0, // 20% VAT for tenant 2
            TenantId = 2,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var taxClass2Tenant2 = new TaxClass
        {
            Id = 4,
            TaxRate = 10.0, // 10% reduced VAT for tenant 2
            TenantId = 2,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        context.TaxClass.AddRange(taxClass1Tenant1, taxClass2Tenant1, taxClass1Tenant2, taxClass2Tenant2);
    }

    private static void SeedWarehouses(ApplicationDbContext context, ITenantContext? tenantContext)
    {
        var warehouse1Tenant1 = new Warehouse
        {
            Id = 1,
            Name = "Main Warehouse Tenant 1",
            TenantId = 1,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var warehouse2Tenant1 = new Warehouse
        {
            Id = 2,
            Name = "Secondary Warehouse Tenant 1",
            TenantId = 1,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var warehouse1Tenant2 = new Warehouse
        {
            Id = 3,
            Name = "Main Warehouse Tenant 2",
            TenantId = 2,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var warehouse2Tenant2 = new Warehouse
        {
            Id = 4,
            Name = "Secondary Warehouse Tenant 2",
            TenantId = 2,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        context.Warehouse.AddRange(warehouse1Tenant1, warehouse2Tenant1, warehouse1Tenant2, warehouse2Tenant2);
    }

    private static void SeedAiModels(ApplicationDbContext context, ITenantContext? tenantContext)
    {
        var aiModel1Tenant1 = new AiModel
        {
            Id = 1,
            Name = "ChatGPT-4O Tenant 1",
            AiModelType = AiModelType.ChatGpt4O,
            ApiUrl = "https://api.openai.com",
            ApiKey = "test-key-1",
            NCtx = 4096,
            TenantId = 1, // Explicitly set TenantId
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var aiModel2Tenant1 = new AiModel
        {
            Id = 2,
            Name = "Claude 3.5 Tenant 1",
            AiModelType = AiModelType.Claude35,
            ApiUrl = "https://api.anthropic.com",
            ApiKey = "test-key-2",
            NCtx = 8192,
            TenantId = 1, // Explicitly set TenantId
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var aiModel1Tenant2 = new AiModel
        {
            Id = 3,
            Name = "ChatGPT-4O Tenant 2",
            AiModelType = AiModelType.ChatGpt4O,
            ApiUrl = "https://api.openai.com",
            ApiKey = "test-key-3",
            NCtx = 4096,
            TenantId = 2, // Explicitly set TenantId
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        context.AiModel.AddRange(aiModel1Tenant1, aiModel2Tenant1, aiModel1Tenant2);
    }

    private static void SeedAiPrompts(ApplicationDbContext context, ITenantContext? tenantContext)
    {
        var aiPrompt1Tenant1 = new AiPrompt
        {
            Id = 1,
            AiModelId = 1,
            Identifier = "Test Prompt 1 Tenant 1",
            PromptText = "This is a test prompt for tenant 1",
            TenantId = 1,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var aiPrompt2Tenant1 = new AiPrompt
        {
            Id = 2,
            AiModelId = 2,
            Identifier = "Test Prompt 2 Tenant 1",
            PromptText = "This is another test prompt for tenant 1",
            TenantId = 1,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var aiPrompt1Tenant2 = new AiPrompt
        {
            Id = 3,
            AiModelId = 3,
            Identifier = "Test Prompt 1 Tenant 2",
            PromptText = "This is a test prompt for tenant 2",
            TenantId = 2,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        context.AiPrompt.AddRange(aiPrompt1Tenant1, aiPrompt2Tenant1, aiPrompt1Tenant2);
    }
}