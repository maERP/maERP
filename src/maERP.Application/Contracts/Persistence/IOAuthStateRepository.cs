using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

/// <summary>
/// Repository for the short-lived OAuth state-token store. Operations are intentionally minimal
/// — the controller never enumerates and never paginates; it only inserts on Start, fetches by
/// token on Callback, and the cleanup background service deletes expired rows.
/// </summary>
public interface IOAuthStateRepository
{
    Task<Guid> CreateAsync(OAuthState entity);

    Task<OAuthState?> GetByTokenAsync(string stateToken);

    Task UpdateAsync(OAuthState entity);

    /// <summary>Deletes states older than <paramref name="cutoff"/> (UTC). Returns rows removed.</summary>
    Task<int> DeleteExpiredAsync(DateTime cutoff);
}
