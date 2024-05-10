namespace maERP.SharedUI.Models.Customer;

public class CustomerAddressVM
{
    public int Id { get; set; }
    public string Street { get; set; } = string.Empty;
    public string HouseNr { get; set; } = string.Empty;
    public string Zip { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public CustomerAddressVM? CustomerAddresses { get; set; }
}