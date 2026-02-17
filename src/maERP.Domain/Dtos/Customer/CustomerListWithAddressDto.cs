namespace maERP.Domain.Dtos.Customer;

public class CustomerListWithAddressDto
{
    public Guid Id { get; set; }
    public int CustomerId { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string InvoiceAddress { get; set; } = string.Empty;
    public DateTimeOffset DateEnrollment { get; set; }

    public string FullName => $"{Firstname} {Lastname}";
}
