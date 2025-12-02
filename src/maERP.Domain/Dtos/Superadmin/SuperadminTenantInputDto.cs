using System.ComponentModel.DataAnnotations;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Dtos.Superadmin;

public class SuperadminTenantInputDto : ITenantInputModel
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    [MaxLength(200)]
    public string? CompanyName { get; set; }

    [EmailAddress]
    [MaxLength(200)]
    public string? ContactEmail { get; set; }

    [MaxLength(50)]
    public string? Phone { get; set; }

    [MaxLength(200)]
    [Url]
    public string? Website { get; set; }

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

    [MaxLength(34)]
    [RegularExpression(@"^[A-Z]{2}[0-9]{2}[A-Z0-9]{1,30}$", ErrorMessage = "Invalid IBAN format")]
    public string? Iban { get; set; }
}
