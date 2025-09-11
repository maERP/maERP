namespace maERP.Domain.Dtos.Customer;

public class CustomerListDto
{
    public Guid Id { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public DateTimeOffset DateEnrollment { get; set; }

    public string FullName => $"{Firstname} {Lastname}";
}