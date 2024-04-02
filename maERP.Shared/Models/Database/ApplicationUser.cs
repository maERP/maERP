using Microsoft.AspNetCore.Identity;

namespace maERP.Shared.Models.Database;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}