#nullable disable

namespace maERP.Shared.Dtos.Customer;

public class CustomerDto : CustomerBaseDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime EnrollmentDate { get; set; }
}