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
                          await context.AiModel.IgnoreQueryFilters().AnyAsync();

            if (!hasData)
            {
                SeedTenants(context, tenantContext);
                SeedAiModels(context, tenantContext);
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
}