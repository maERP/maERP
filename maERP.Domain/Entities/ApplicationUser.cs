using Microsoft.AspNetCore.Identity;

namespace maERP.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
}