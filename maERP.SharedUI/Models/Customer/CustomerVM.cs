namespace maERP.SharedUI.Models.Customer;

public class CustomerVM
{
    public int Id { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public List<CustomerAddressVM> CustomerAddresses { get; set; } = new();
    public DateTime DateEnrollment { get; set; }
}