namespace maERP.Shared.Dtos.Customer;

public class CustomerUpdateDto : CustomerBaseDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}