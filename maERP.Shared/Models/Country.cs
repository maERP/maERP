using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class Country
{
    [Key]
    [Column("id")]
    public int CountryId { get; set; }

    [Required]
    [Column("name")]
    [Display(Name = "Name")]
    public String Name { get; set; } = null!;

    [Required]
    [Column("country_code")]
    [Display(Name = "Ländercode")]
    public String CountryCode { get; set; } = null!;

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Column("created_at")]
    [Display]
    public DateTime CreatedAt { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}