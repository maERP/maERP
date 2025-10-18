using System.ComponentModel.DataAnnotations;
using maERP.Domain.Entities.Common;

namespace maERP.Domain.Entities;

public class Tenant : BaseEntityWithoutTenant, IBaseEntityWithoutTenant
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public bool IsActive { get; set; } = true;

    [MaxLength(200)]
    public string? ContactEmail { get; set; }

    // Users with this tenant as their default tenant
    public ICollection<ApplicationUser>? DefaultForUsers { get; set; }

    // Collection of user-tenant associations
    public ICollection<UserTenant>? UserTenants { get; set; }
}