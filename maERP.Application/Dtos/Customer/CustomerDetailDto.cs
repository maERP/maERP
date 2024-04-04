namespace maERP.Shared.Dtos.Customer;

public class CustomerDetailDto
{
    public virtual int Id { get; set; } 
    public virtual string FirstName { get; set; } = string.Empty;
    public virtual string LastName { get; set; } = string.Empty;
    public virtual DateTime EnrollmentDate { get; set; }
}