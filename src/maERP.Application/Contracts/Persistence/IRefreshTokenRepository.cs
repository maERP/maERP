using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

/// <summary>
/// Refresh-token store. The plaintext token never leaves the issue/refresh handlers — all lookups
/// go through the SHA-256 hash. Family-based revocation is the canary for replay attempts.
/// </summary>
public interface IRefreshTokenRepository
{
    Task<Guid> CreateAsync(RefreshToken entity);

    Task<RefreshToken?> GetByHashAsync(string tokenHash);

    Task UpdateAsync(RefreshToken entity);

    /// <summary>Marks every non-revoked token in the family as revoked. Used on replay detection and on logout.</summary>
    Task RevokeFamilyAsync(Guid family, DateTime revokedAt);

    /// <summary>Bulk-deletes tokens whose ExpiresAt is older than <paramref name="cutoff"/>. Returns rows removed.</summary>
    Task<int> DeleteExpiredAsync(DateTime cutoff);
}
