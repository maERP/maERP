namespace maERP.Application.Dtos.Customer;

public class CustomerDetailDto
{
    public virtual int Id { get; set; }
    public virtual string Firstname { get; set; } = string.Empty;
    public virtual string Lastname { get; set; } = string.Empty;
    public virtual DateTime EnrollmentDate { get; set; }
}