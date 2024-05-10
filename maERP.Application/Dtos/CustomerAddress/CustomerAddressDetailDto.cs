namespace maERP.Application.Dtos.CustomerAddress;

public class CustomerAddressDetailDto
{
    public int Id { get; set; }
    public string Street { get; set; } = string.Empty;
    public string HouseNr { get; set; } = string.Empty;
    public string Zip { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}