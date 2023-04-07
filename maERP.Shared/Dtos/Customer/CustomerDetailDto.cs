#nullable disable

namespace maERP.Shared.Dtos.Customer;

public class CustomerDetailDto : CustomerBaseDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime EnrollmentDate { get; set; }
}