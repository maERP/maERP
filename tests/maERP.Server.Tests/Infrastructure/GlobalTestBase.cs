using System.Text.Json;
using maERP.Persistence.DatabaseContext;
using Microsoft.Extensions.DependencyInjection;

namespace maERP.Server.Tests.Infrastructure;

public abstract class GlobalTestBase : IDisposable
{
    protected TestWebApplicationFactory<Program> Factory { get; }
    protected HttpClient Client { get; }
    protected ApplicationDbContext DbContext { get; }
    protected IServiceScope Scope { get; }

    protected GlobalTestBase()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_{GetType().Name}_{uniqueId}";
        Environment.SetEnvironmentVariable("TEST_DB_NAME", testDbName);

        Factory = new TestWebApplicationFactory<Program>();
        Client = Factory.CreateClient();
        Scope = Factory.Services.CreateScope();
        DbContext = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        DbContext.Database.EnsureCreated();
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

    protected async Task<T?> ReadResponseAsync<T>(HttpResponseMessage response) where T : class
    {
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    protected async Task<string> ReadResponseStringAsync(HttpResponseMessage response)
    {
        return await response.Content.ReadAsStringAsync();
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
        GC.SuppressFinalize(this);
    }
}