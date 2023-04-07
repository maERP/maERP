using System.ComponentModel.DataAnnotations;

namespace maERP.Shared.Models;

public class Address
{
    [Key]
    public int Id { get; set; }

    [Required]
    public String Street { get; set; } = null!;

    [Required]
    public String HouseNr { get; set; } = null!;

    [Required]
    public String Zip { get; set; } = null!;

    [Required]
    public String City { get; set; } = null!;

    // [Required]
    //public Enum Country { get; set; }
}