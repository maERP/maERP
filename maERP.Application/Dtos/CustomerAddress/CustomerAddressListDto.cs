namespace maERP.Application.Dtos.CustomerAddress;

public class CustomerAddressListDto
{
    public int Id { get; set; }
    public string Street { get; set; } = string.Empty;
    public string HouseNr { get; set; } = string.Empty;
    public string Zip { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}