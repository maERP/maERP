namespace maERP.SharedUI.Models.CustomerAddress;

public class CustomerAddressListVm
{
    public int Id { get; set; }
    public string Street { get; set; } = string.Empty;
    public string HouseNr { get; set; } = string.Empty;
    public string Zip { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public bool DefaultDeliveryAddress { get; set; }
    public bool DefaultInvoiceAddress { get; set; }
    public string FullAddress => $"{Street} {HouseNr}, {Zip} {City}";
}