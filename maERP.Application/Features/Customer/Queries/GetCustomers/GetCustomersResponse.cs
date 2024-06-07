namespace maERP.Application.Features.Customer.Queries.GetCustomers;

public class GetCustomersResponse
{
    public int Id { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public DateTime DateEnrollment { get; set; }
}