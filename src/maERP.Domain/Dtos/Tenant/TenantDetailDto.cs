using System.Text.Json.Serialization;

namespace maERP.Domain.Dtos.Tenant;

public class TenantDetailDto : TenantDtoBase
{
    [JsonPropertyName("userCount")]
    public int UserCount { get; set; }
}
