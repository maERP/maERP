using maERP.Domain.Dtos.CustomerAddress;

namespace maERP.Domain.Dtos.Customer;

public class CustomerDetailDto
{
    public int Id { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public List<CustomerAddressListDto> CustomerAddresses { get; set; } = new();
    public DateTime DateEnrollment { get; set; }

    public string FullName => $"{Firstname} {Lastname}";
}