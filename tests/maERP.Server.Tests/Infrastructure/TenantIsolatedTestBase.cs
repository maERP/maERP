using System.Text.Json;
using maERP.Application.Contracts.Services;
using maERP.Domain.Constants;
using maERP.Persistence.DatabaseContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace maERP.Server.Tests.Infrastructure;

public abstract class TenantIsolatedTestBase : IDisposable
{
    protected TestWebApplicationFactory<Program> Factory { get; }
    protected HttpClient Client { get; }
    protected ApplicationDbContext DbContext { get; }
    protected ITenantContext TenantContext { get; }
    protected IServiceScope Scope { get; }

    protected TenantIsolatedTestBase()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_{GetType().Name}_{uniqueId}";
        Environment.SetEnvironmentVariable("TEST_DB_NAME", testDbName);

        Factory = new TestWebApplicationFactory<Program>();
        Client = Factory.CreateClient();
        Scope = Factory.Services.CreateScope();
        DbContext = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        TenantContext = Scope.ServiceProvider.GetRequiredService<ITenantContext>();

        DbContext.Database.EnsureCreated();
        InitializeTenantContext();
    }


    protected virtual void InitializeTenantContext()
    {
        TenantContext.SetAssignedTenantIds(new[] {
            TenantConstants.TestTenant1Id,
            TenantConstants.TestTenant2Id
        });
        TenantContext.SetCurrentTenantId(null);
    }

    protected void SetTenantHeader(Guid tenantId)
    {
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", tenantId.ToString());
    }

    protected void SetInvalidTenantHeader()
    {
        SetTenantHeader(Guid.Parse("99999999-9999-9999-9999-999999999999"));
    }

    protected void SetInvalidTenantHeaderValue(string invalidValue)
    {
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", invalidValue);
    }

    protected void RemoveTenantHeader()
    {
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
    }

    protected void SimulateUnauthenticatedRequest()
    {
        Client.DefaultRequestHeaders.Add("X-Test-Unauthenticated", "true");
    }

    protected void SimulateAuthenticatedRequest()
    {
        Client.DefaultRequestHeaders.Remove("X-Test-Unauthenticated");
    }

    protected async Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        return await Client.PostAsync(requestUri, content);
    }

    protected async Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        return await Client.PutAsync(requestUri, content);
    }

    protected async Task<T> ReadResponseAsync<T>(HttpResponseMessage response) where T : class
    {
        var content = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(content))
            throw new InvalidOperationException("Response content is empty");

        try
        {
            var result = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return result ?? throw new InvalidOperationException("Failed to deserialize response");
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException($"Failed to deserialize response: {ex.Message}. Content: {content}");
        }
    }

    protected async Task<string> ReadResponseStringAsync(HttpResponseMessage response)
    {
        return await response.Content.ReadAsStringAsync();
    }

    public virtual void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }
}