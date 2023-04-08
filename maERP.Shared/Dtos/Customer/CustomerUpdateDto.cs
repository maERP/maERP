#nullable disable

namespace maERP.Shared.Dtos.Customer;

public class CustomerUpdateDto : CustomerBaseDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}