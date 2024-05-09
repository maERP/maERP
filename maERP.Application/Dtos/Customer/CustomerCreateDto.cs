namespace maERP.Application.Dtos.Customer;

public class CustomerCreateDto
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public DateTime DateEnrollment { get; set; }
}