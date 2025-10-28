using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// API client for demo data operations (DEBUG only)
/// </summary>
public interface IDemoDataApiClient
{
    /// <summary>
    /// Create all demo data
    /// </summary>
    Task<Result<string>?> CreateAllDemoDataAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Create AI demo data
    /// </summary>
    Task<Result<string>?> CreateAiDemoDataAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Create tenant demo data
    /// </summary>
    Task<Result<string>?> CreateTenantDemoDataAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Clear all data
    /// </summary>
    Task<Result<string>?> ClearAllDataAsync(CancellationToken cancellationToken = default);
}
