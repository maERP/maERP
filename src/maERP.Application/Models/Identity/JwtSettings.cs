namespace maERP.Application.Models.Identity;

public class JwtSettings
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;

    /// <summary>Lifetime of the access token (JWT). Should be short — 15–60 min.</summary>
    public double DurationInMinutes { get; set; }

    /// <summary>Refresh-token lifetime when the user opted into "Remember me".</summary>
    public int RefreshTokenExpireDays { get; set; }

    /// <summary>Refresh-token lifetime when the user did NOT opt into "Remember me" (session-ish).</summary>
    public int RefreshTokenExpireDaysShort { get; set; } = 1;
}
