using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class Country
{
    [Key]
    public int CountryId { get; set; }

    [Required]
    [Display(Name = "Name")]
    public String Name { get; set; } = null!;

    [Required]
    [Display(Name = "Ländercode")]
    public String CountryCode { get; set; } = null!;

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}