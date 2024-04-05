namespace maERP.Application.Dtos.Customer;

public class CustomerCreateDto
{
    public virtual string FirstName { get; set; } = string.Empty;
    public virtual string LastName { get; set; } = string.Empty;
    public virtual DateTime EnrollmentDate { get; set; }
}