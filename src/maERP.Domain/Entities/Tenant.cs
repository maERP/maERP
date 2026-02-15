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

    // Company Information
    [MaxLength(200)]
    public string? CompanyName { get; set; }

    // Contact Information
    [MaxLength(200)]
    public string? ContactEmail { get; set; }

    [MaxLength(50)]
    public string? Phone { get; set; }

    [MaxLength(200)]
    public string? Website { get; set; }

    // Address Information
    [MaxLength(200)]
    public string? Street { get; set; }

    [MaxLength(200)]
    public string? Street2 { get; set; }

    [MaxLength(20)]
    public string? PostalCode { get; set; }

    [MaxLength(100)]
    public string? City { get; set; }

    [MaxLength(100)]
    public string? State { get; set; }

    [MaxLength(100)]
    public string? Country { get; set; }

    // Banking Information
    [MaxLength(34)]
    public string? Iban { get; set; }

    // Users with this tenant as their default tenant
    public ICollection<ApplicationUser>? DefaultForUsers { get; set; }

    // Collection of user-tenant associations
    public ICollection<UserTenant>? UserTenants { get; set; }
}