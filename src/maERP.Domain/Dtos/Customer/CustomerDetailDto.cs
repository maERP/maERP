using maERP.Domain.Dtos.CustomerAddress;
using maERP.Domain.Enums;

namespace maERP.Domain.Dtos.Customer;

public class CustomerDetailDto
{
    public int Id { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;

    public string CompanyName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string VatNumber { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public CustomerStatus CustomerStatus { get; set; }
    public DateTimeOffset DateEnrollment { get; set; }

    public List<CustomerAddressListDto> CustomerAddresses { get; set; } = new();

    public string FullName => $"{Firstname} {Lastname}";
}