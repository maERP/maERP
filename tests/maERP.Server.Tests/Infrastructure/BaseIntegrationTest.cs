using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using maERP.Persistence.DatabaseContext;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using maERP.Application.Contracts.Services;

namespace maERP.Server.Tests.Infrastructure;

public abstract class BaseIntegrationTest : IClassFixture<TestWebApplicationFactory<Program>>, IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    protected BaseIntegrationTest(TestWebApplicationFactory<Program> factory)
    {
        // Set a unique database name per test INSTANCE to ensure complete isolation
        var testClassName = GetType().Name;
        var testMethodName = System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType?.Name ?? "Unknown";
        var uniqueId = Guid.NewGuid().ToString("N")[..8]; // Short unique ID
        var testDbName = $"TestDb_{testClassName}_{uniqueId}_{Thread.CurrentThread.ManagedThreadId}";
        Environment.SetEnvironmentVariable("TEST_DB_NAME", testDbName);

        Factory = factory;
        Client = factory.CreateClient();

        Scope = factory.Services.CreateScope();
        DbContext = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        TenantContext = Scope.ServiceProvider.GetRequiredService<ITenantContext>();

        // Ensure database is created for this test
        DbContext.Database.EnsureCreated();

        // Initialize tenant context with default tenants and reset current tenant
        TenantContext.SetAssignedTenantIds(new[] { 1, 2 });
        TenantContext.SetCurrentTenantId(null); // Start with no tenant set
    }

    protected void SetTenantHeader(int tenantId)
    {
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", tenantId.ToString());
    }

    protected void ClearTenantHeader()
    {
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
    }

    protected void ClearAllHeaders()
    {
        Client.DefaultRequestHeaders.Clear();
    }

    protected void SetAuthorizationHeader(string token)
    {
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    protected async Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        return await Client.PostAsync(requestUri, content);
    }

    protected async Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        return await Client.PutAsync(requestUri, content);
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

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
    }
}