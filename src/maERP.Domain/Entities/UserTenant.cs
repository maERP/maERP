using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using maERP.Domain.Entities.Common;

namespace maERP.Domain.Entities;

public class UserTenant : BaseEntityWithoutTenant
{
    [Required]
    public string UserId { get; set; } = string.Empty;

    [ForeignKey("UserId")]
    public ApplicationUser? User { get; set; }

    [Required]
    public Guid TenantId { get; set; }

    [ForeignKey("TenantId")]
    public Tenant? Tenant { get; set; }

    // Flag to indicate if this is the default tenant for the user
    public bool IsDefault { get; set; }

    public bool RoleManageTenant { get; set; }
    public bool RoleManageUser { get; set; }
    
}
