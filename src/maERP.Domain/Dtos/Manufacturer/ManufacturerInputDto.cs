using System.ComponentModel.DataAnnotations;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Dtos.Manufacturer;

public class ManufacturerInputDto : IManufacturerInputModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [Display(Name = "Name")]
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Street")]
    [StringLength(255)]
    public string Street { get; set; } = string.Empty;

    [Display(Name = "City")]
    [StringLength(255)]
    public string City { get; set; } = string.Empty;

    [Display(Name = "State")]
    [StringLength(255)]
    public string State { get; set; } = string.Empty;

    [Display(Name = "Country")]
    [StringLength(255)]
    public string Country { get; set; } = string.Empty;

    [Display(Name = "Zip Code")]
    [StringLength(20)]
    public string ZipCode { get; set; } = string.Empty;

    [Display(Name = "Phone")]
    [StringLength(50)]
    public string Phone { get; set; } = string.Empty;

    [Display(Name = "Email")]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Website")]
    [StringLength(500)]
    public string Website { get; set; } = string.Empty;

    [Display(Name = "Logo")]
    [StringLength(500)]
    public string Logo { get; set; } = string.Empty;
}