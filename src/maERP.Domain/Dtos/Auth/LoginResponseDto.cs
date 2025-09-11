#nullable disable

using maERP.Domain.Dtos.Tenant;

namespace maERP.Domain.Dtos.Auth;

public class LoginResponseDto
{
    public string UserId { get; set; }
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public string Token { get; set; }
    public List<TenantListDto> AvailableTenants { get; set; } = new();
    public Guid? CurrentTenantId { get; set; }
}