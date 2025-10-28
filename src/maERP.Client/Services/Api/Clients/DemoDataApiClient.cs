using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// Implementation of demo data API client (DEBUG only)
/// </summary>
public class DemoDataApiClient : ApiClientBase, IDemoDataApiClient
{
    private const string BaseEndpoint = "api/v1/DemoData";

    public DemoDataApiClient(HttpClient httpClient, ILogger<DemoDataApiClient> logger)
        : base(httpClient, logger)
    {
    }

    public async Task<Result<string>?> CreateAllDemoDataAsync(CancellationToken cancellationToken = default)
    {
        return await PostAsync<object, Result<string>>($"{BaseEndpoint}/all", new { }, cancellationToken);
    }

    public async Task<Result<string>?> CreateAiDemoDataAsync(CancellationToken cancellationToken = default)
    {
        return await PostAsync<object, Result<string>>($"{BaseEndpoint}/ai", new { }, cancellationToken);
    }

    public async Task<Result<string>?> CreateTenantDemoDataAsync(CancellationToken cancellationToken = default)
    {
        return await PostAsync<object, Result<string>>($"{BaseEndpoint}/tenants", new { }, cancellationToken);
    }

    public async Task<Result<string>?> ClearAllDataAsync(CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.DeleteAsync($"{BaseEndpoint}/clear", cancellationToken);
        return await EnsureSuccessAndReadAsync<Result<string>>(response, cancellationToken);
    }
}
