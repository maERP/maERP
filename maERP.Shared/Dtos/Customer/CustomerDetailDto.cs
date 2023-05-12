namespace maERP.Shared.Dtos.Customer;

public class CustomerDetailDto
{
    public virtual uint Id { get; set; } 
    public virtual string FirstName { get; set; } = string.Empty;
    public virtual string LastName { get; set; } = string.Empty;
    public virtual DateTime EnrollmentDate { get; set; }
}