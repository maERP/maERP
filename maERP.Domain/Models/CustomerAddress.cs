using System.ComponentModel.DataAnnotations;
using maERP.Domain.Models.Common;

namespace maERP.Domain.Models;

public class CustomerAddress : BaseEntity
{
    public string Street { get; set; } = string.Empty;
    public string HouseNr { get; set; } = string.Empty;
    public string Zip { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public required Country Country { get; set; }
    public required Customer Customer { get; set; }
}