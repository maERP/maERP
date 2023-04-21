using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace maERP.Shared.Models;

public class ApiUser : IdentityUser
{
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;
}