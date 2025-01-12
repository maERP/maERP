namespace maERP.Application.Features.Customer.Commands.CustomerCreate;

public class CustomerCreateResponse
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public DateTime DateEnrollment { get; set; }
}