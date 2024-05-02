#nullable disable

using maERP.Application.Contracts.Identity;
using maERP.Application.Models.Identity;
using maERP.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace maERP.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    
    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Employee> GetEmployee(string userId)
    {
        var employee = await _userManager.FindByIdAsync(userId);
        
        return new Employee
        {
            Email = employee.Email,
            Id = employee.Id,
            Firstname = employee.Firstname,
            Lastname = employee.Lastname
        };
    }
    
    public async Task<List<Employee>> GetEmployees()
    {
        var employees = await _userManager.GetUsersInRoleAsync("Employee");
        
        return employees.Select(q => new Employee
        {
            Id = q.Id,
            Email = q.Email,
            Firstname = q.Firstname,
            Lastname = q.Lastname
        }).ToList();
    }
}