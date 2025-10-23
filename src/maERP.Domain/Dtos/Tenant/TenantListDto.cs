using System.Text.Json.Serialization;

namespace maERP.Domain.Dtos.Tenant;

public class TenantListDto : TenantDtoBase
{
    [JsonPropertyName("canManageTenant")]
    public bool CanManageTenant { get; set; }
}
