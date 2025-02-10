using maERP.Domain.Dtos.CustomerAddress;

namespace maERP.Domain.Dtos.Customer;

public class CustomerListDto
{
    public int Id { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public DateTime DateEnrollment { get; set; }

    public string FullName => $"{Firstname} {Lastname}";
}