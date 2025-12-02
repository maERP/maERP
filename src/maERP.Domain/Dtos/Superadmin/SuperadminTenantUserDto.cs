using System.Text.Json.Serialization;

namespace maERP.Domain.Dtos.Superadmin;

/// <summary>
/// DTO representing a user assigned to a tenant in the superadmin context.
/// </summary>
public class SuperadminTenantUserDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("firstname")]
    public string Firstname { get; set; } = string.Empty;

    [JsonPropertyName("lastname")]
    public string Lastname { get; set; } = string.Empty;

    [JsonPropertyName("isDefault")]
    public bool IsDefault { get; set; }

    [JsonPropertyName("roleManageTenant")]
    public bool RoleManageTenant { get; set; }

    [JsonPropertyName("roleManageUser")]
    public bool RoleManageUser { get; set; }

    [JsonPropertyName("dateCreated")]
    public DateTime DateCreated { get; set; }
}
