using maERP.SharedUI.Models.CustomerAddress;

namespace maERP.SharedUI.Models.Customer;

public class CustomerVm
{
    public int Id { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public List<CustomerAddressListVm> CustomerAddresses { get; set; } = new();
    public DateTime DateEnrollment { get; set; }

    public string FullName => $"{Firstname} {Lastname}";
}