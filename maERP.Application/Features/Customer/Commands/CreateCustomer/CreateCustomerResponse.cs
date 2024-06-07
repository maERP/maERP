namespace maERP.Application.Features.Customer.Commands.CreateCustomer;

public class CreateCustomerResponse
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public DateTime DateEnrollment { get; set; }
}