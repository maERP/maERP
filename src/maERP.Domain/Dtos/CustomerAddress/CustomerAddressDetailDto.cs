namespace maERP.Domain.Dtos.CustomerAddress;

public class CustomerAddressDetailDto
{
    public Guid Id { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string HouseNr { get; set; } = string.Empty;
    public string Zip { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public bool DefaultDeliveryAddress { get; set; }
    public bool DefaultInvoiceAddress { get; set; }
    public Guid CountryId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
}