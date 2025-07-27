using System.ComponentModel.DataAnnotations;

namespace maERP.Domain.Dtos.Manufacturer;

public class ManufacturerDetailDto
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(255)]
    public string Street { get; set; } = string.Empty;
    
    [StringLength(255)]
    public string City { get; set; } = string.Empty;
    
    [StringLength(255)]
    public string State { get; set; } = string.Empty;
    
    [StringLength(255)]
    public string Country { get; set; } = string.Empty;
    
    [StringLength(20)]
    public string ZipCode { get; set; } = string.Empty;
    
    [StringLength(50)]
    public string Phone { get; set; } = string.Empty;
    
    [StringLength(255)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string Website { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string Logo { get; set; } = string.Empty;
}