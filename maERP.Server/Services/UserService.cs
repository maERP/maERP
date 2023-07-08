using maERP.Server.Contracts;
using maERP.Server.Models;
using Microsoft.AspNetCore.Identity;

namespace maERP.Server.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User> GetUser(string userId)
    {
        var user = await _userManager.FindByEmailAsync(userId);

        return new User
        {
            Email = user.Email,
            Id = user.Id,
            Firstname = user.FirstName,
            Lastname = user.LastName
        };
    }

    public async Task<List<User>> GetUsers()
    {
        // var users = await _userManager.Users.ToListAsync();
        var users = await _userManager.GetUsersInRoleAsync(("User"));

        return users.Select(q => new User
        {
            Id = q.Id,
            Email = q.Email,
            Firstname = q.FirstName,
            Lastname = q.LastName
        }).ToList();
    }
}