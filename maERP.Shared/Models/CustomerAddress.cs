using System.ComponentModel.DataAnnotations;

namespace maERP.Server.Models;

public class CustomerAddress
{
    [Key]
    public int Id { get; set; }

    // [Required]
    //public Enum Type { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public String Street { get; set; } = null!;

    [Required]
    public String HouseNr { get; set; } = null!;

    [Required]
    public String Zip { get; set; } = null!;

    [Required]
    public String City { get; set; } = null!;

    [Required]
    public int CountryId { get; set; }
}

