using System.ComponentModel.DataAnnotations;

namespace maERP.Domain.Dtos.Manufacturer;

public class ManufacturerDetailDto
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;

    [StringLength(255)]
    public string? Street { get; set; }

    [StringLength(255)]
    public string City { get; set; } = string.Empty;

    [StringLength(255)]
    public string? State { get; set; }

    [StringLength(255)]
    public string Country { get; set; } = string.Empty;

    [StringLength(20)]
    public string? ZipCode { get; set; }

    [StringLength(50)]
    public string? Phone { get; set; }

    [StringLength(255)]
    [EmailAddress]
    public string? Email { get; set; }

    [StringLength(500)]
    public string? Website { get; set; }

    [StringLength(500)]
    public string? Logo { get; set; }
}