using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace maERP.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;

    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime DateModified { get; set; } = DateTime.UtcNow;

    // Default tenant for backward compatibility and ease of migration
    public int? DefaultTenantId { get; set; }

    // Navigation property for the default tenant
    public Tenant? DefaultTenant { get; set; }

    // Collection of user-tenant associations
    public ICollection<UserTenant>? UserTenants { get; set; }
}