namespace maERP.SharedUI.Models.Customer;

public class CustomerVM
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime EnrollmentDate { get; set; }
}