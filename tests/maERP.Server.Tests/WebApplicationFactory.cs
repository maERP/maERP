using maERP.Application.Contracts.Services;
using maERP.Identity.Services;
using maERP.Persistence.DatabaseContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace maERP.Server.Tests;

public class MaErpWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(
            // ReSharper disable once AsyncVoidLambda
            async services =>
            {
                // Remove all DbContext related services
                var descriptors = services.Where(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>)
                        || d.ServiceType == typeof(ApplicationDbContext)).ToList();

                foreach (var descriptor in descriptors)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ApplicationDbContext>(
                    options =>
                    {
                        options.UseInMemoryDatabase("MemoryDB");
                        options.ConfigureWarnings(configurationBuilder => configurationBuilder.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                    }
                );

                // Replace TenantContext with a test version
                var tenantContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(ITenantContext));
                if (tenantContextDescriptor != null)
                {
                    services.Remove(tenantContextDescriptor);
                }
                services.AddScoped<ITenantContext, TestTenantContext>();

                await Task.CompletedTask;
            }
        );
    }

    public async Task InitializeDbForTests<T>(List<T>? seedData = null) where T : class
    {
        using var scope = Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        await db.Database.EnsureDeletedAsync();
        await db.Database.EnsureCreatedAsync();

        if (seedData != null)
        {
            await db.Set<T>().AddRangeAsync(seedData);
            await db.SaveChangesAsync();
        }
    }

    public async Task InitializeDbForTests()
    {
        using var scope = Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        await db.Database.EnsureDeletedAsync();
        await db.Database.EnsureCreatedAsync();
    }
}

public class TestTenantContext : ITenantContext
{
    private int? _currentTenantId = 1; // Default to tenant 1 for tests
    private readonly HashSet<int> _assignedTenantIds = new() { 1, 2 }; // Allow access to tenant 1 and 2

    public int? GetCurrentTenantId() => _currentTenantId;

    public void SetCurrentTenantId(int? tenantId)
    {
        if (tenantId == null || _assignedTenantIds.Contains(tenantId.Value))
        {
            _currentTenantId = tenantId;
        }
    }

    public bool HasTenant() => _currentTenantId.HasValue;

    public IReadOnlyCollection<int> GetAssignedTenantIds() => _assignedTenantIds;

    public void SetAssignedTenantIds(IEnumerable<int> tenantIds)
    {
        _assignedTenantIds.Clear();
        foreach (var id in tenantIds)
        {
            _assignedTenantIds.Add(id);
        }

        if (_currentTenantId.HasValue && !_assignedTenantIds.Contains(_currentTenantId.Value))
        {
            _currentTenantId = _assignedTenantIds.FirstOrDefault();
        }
    }

    public bool IsAssignedToTenant(int tenantId) => _assignedTenantIds.Contains(tenantId);

    public void SetTestTenant(int? tenantId)
    {
        _currentTenantId = tenantId;
        if (tenantId.HasValue && !_assignedTenantIds.Contains(tenantId.Value))
        {
            _assignedTenantIds.Add(tenantId.Value);
        }
    }
}