using System.ComponentModel.DataAnnotations;

namespace maERP.Domain.Dtos.User;

public class UserTenantAssignmentDto
{
    [Required]
    public int TenantId { get; set; }
    
    public string TenantName { get; set; } = string.Empty;
    
    public string TenantCode { get; set; } = string.Empty;
    
    public bool IsDefault { get; set; }
}
