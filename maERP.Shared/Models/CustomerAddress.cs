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
    public String Street { get; set; } = null!;

    [Required]
    [Column("house_nr")]
    public String HouseNr { get; set; } = null!;

    [Required]
    [Column("zip")]
    public String Zip { get; set; } = null!;

    [Required]
    [Column("city")]
    public String City { get; set; } = null!;

    [Required]
    [Column("country_id")]
    public Country Country { get; set; } = null!;

    [Required]
    [Column("customer_id")]
    public Customer Customer{ get; set; } = null!;
}