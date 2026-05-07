using System.ComponentModel.DataAnnotations;
using maERP.Domain.Entities.Common;

namespace maERP.Domain.Entities;

/// <summary>
/// Long-lived opaque refresh token, paired with a short-lived JWT access token. The plaintext
/// token is only ever returned to the client at issue time; the database stores its SHA-256 hash
/// so a leaked dump cannot be used to mint access tokens.
///
/// Rotation: every successful refresh marks the current row as revoked and inserts a successor
/// in the same <see cref="Family"/>. If a revoked token is ever presented again (replay), the
/// whole family is invalidated — that's the canary for a stolen token.
/// </summary>
public class RefreshToken : BaseEntityWithoutTenant, IBaseEntityWithoutTenant
{
    [Required]
    [MaxLength(64)]
    public string UserId { get; set; } = string.Empty;

    /// <summary>SHA-256(token) base64. Lookup key.</summary>
    [Required]
    [MaxLength(128)]
    public string TokenHash { get; set; } = string.Empty;

    /// <summary>Rotation chain id — all successors share this value.</summary>
    [Required]
    public Guid Family { get; set; }

    [Required]
    public DateTime ExpiresAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    public Guid? ReplacedByTokenId { get; set; }

    /// <summary>True when the user opted into "remember me"; controls lifetime + roaming.</summary>
    public bool IsPersistent { get; set; }
}
