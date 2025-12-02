using System.Text.Json.Serialization;

namespace maERP.Domain.Dtos.Superadmin;

/// <summary>
/// DTO representing detailed tenant information for superadmin context,
/// including a list of users assigned to the tenant.
/// </summary>
public class SuperadminTenantDetailDto : SuperadminTenantDtoBase
{
    [JsonPropertyName("userCount")]
    public int UserCount { get; set; }

    [JsonPropertyName("users")]
    public List<SuperadminTenantUserDto> Users { get; set; } = new();
}
