using System.ComponentModel.DataAnnotations;

namespace maERP.Domain.Dtos.User;

public class UserTenantAssignmentDto
{
    [Required]
    public Guid TenantId { get; set; }

    public string TenantName { get; set; } = string.Empty;

    public bool IsDefault { get; set; }

    public bool RoleManageUser { get; set; }
}
