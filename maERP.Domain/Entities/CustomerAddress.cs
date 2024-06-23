using maERP.Domain.Entities.Common;

namespace maERP.Domain.Entities;

public class CustomerAddress : BaseEntity, IBaseEntity
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string HouseNr { get; set; } = string.Empty;
    public string Zip { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public bool DefaultDeliveryAddress { get; set; }
    public bool DefaultInvoiceAddress { get; set; }
    public Country? Country { get; set; }
    public int CountryId { get; set; }
    public Customer? Customer { get; set; }
    public int CustomerId { get; set; }
}