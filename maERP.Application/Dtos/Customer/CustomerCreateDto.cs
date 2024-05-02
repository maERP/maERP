namespace maERP.Application.Dtos.Customer;

public class CustomerCreateDto
{
    public virtual string Firstname { get; set; } = string.Empty;
    public virtual string Lastname { get; set; } = string.Empty;
    public virtual DateTime EnrollmentDate { get; set; }
}