
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class ShippingProviderRate
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Column("max_length")]
    public decimal MaxLength { get; set; }

    [Required]
    [Column("max_width")]
    public decimal MaxWidth { get; set; }

    [Required]
    [Column("max_height")]
    public decimal MaxHeight { get; set; }

    [Required]
    [Column("max_weight")]
    public decimal MaxWeight { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}