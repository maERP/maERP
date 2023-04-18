using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class CustomerAddress : BaseModel
{
    [Column("street")]
    public virtual string Street { get; set; } = String.Empty;

    [Column("house_nr")]
    public virtual string HouseNr { get; set; } = String.Empty;

    [Column("zip")]
    public virtual string Zip { get; set; } = String.Empty;

    [Column("city")]
    public virtual string City { get; set; } = String.Empty;

    [Column("country_id")]
    public virtual Country Country { get; set; } = null!;

    [Column("customer_id")]
    public virtual Customer Customer { get; set; } = null!;
}