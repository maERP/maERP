using System.Text.Json.Serialization;

namespace maERP.Domain.Dtos.Tenant;

public class TenantStatusUpdateDto
{
    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }
}
