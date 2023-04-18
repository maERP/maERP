using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class CustomerAddress
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("street")]
    public String Street { get; set; } = String.Empty;

    [Required]
    [Column("house_nr")]
    public String HouseNr { get; set; } = String.Empty;

        [Required]
    [Column("zip")]
    public String Zip { get; set; } = String.Empty;

    [Required]
    [Column("city")]
    public String City { get; set; } = String.Empty;

    [Required]
    [Column("country_id")]
    public virtual Country Country { get; set; } = null!;

    [Required]
    [Column("customer_id")]
    public virtual Customer Customer { get; set; } = null!;
}