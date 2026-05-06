using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Enums;

namespace maERP.Domain.Dtos.SalesChannel;

public class SalesChannelDetailDto
{
    public Guid Id { get; set; }
    public SalesChannelType SalesChannelType { get; set; }
    public string Name { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public bool ImportProducts { get; set; }
    public bool ExportProducts { get; set; }
    public bool ImportCustomers { get; set; }
    public bool ExportCustomers { get; set; }
    public bool ImportSaless { get; set; }
    public bool ExportSaless { get; set; }

    /// <summary>
    /// True if the channel has a stored refresh token (OAuth flow has been completed).
    /// The token itself is never exposed in the DTO.
    /// </summary>
    public bool HasRefreshToken { get; set; }

    /// <summary>UTC expiry of the current access token; null if not connected or never used.</summary>
    public DateTime? TokenExpiresAt { get; set; }

    public List<WarehouseDetailDto> Warehouses { get; set; } = new();
}

