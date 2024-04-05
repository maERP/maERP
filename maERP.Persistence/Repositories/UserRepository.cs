using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using maERP.Application.Dtos.User;
using maERP.Application.Contracts.Persistence;
using maERP.Domain;

namespace maERP.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<ApplicationUser>> GetAllAsync()
    {
        return await _userManager.Users.AsNoTracking().ToListAsync();
    }

    public async Task<ApplicationUser> GetByIdAsync(string userId)
    {
        return await _userManager.Users
            .Where(x => x.Id == userId)
            .AsNoTracking()
            .FirstAsync<ApplicationUser>();
    }

    /*
    public async Task<IEnumerable<IdentityError>> AddAsync(ApplicationUser userCreateDto)
    {
        var _user = _mapper.Map<ApplicationUser>(userCreateDto);

        _user.Email = userCreateDto.Email;
        _user.UserName = userCreateDto.Email;
        _user.FirstName = userCreateDto.FirstName;
        _user.LastName = userCreateDto.LastName;
        _user.PasswordHash = _userManager.PasswordHasher.HashPassword(_user, userCreateDto.Password);

        var result = await _userManager.CreateAsync(_user, userCreateDto.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(_user, "User");
        }

        return result.Errors;
    }
    */
    public async Task UpdateWithDetailsAsync(string userId, UserUpdateDto userUpdateDto)
    {
        var localUser = await _userManager.FindByIdAsync(userId);

        if (localUser.Id is not null)
        {
            localUser.Email = userUpdateDto.Email;
            localUser.UserName = userUpdateDto.Email.ToLower();
            localUser.FirstName = userUpdateDto.FirstName;
            localUser.LastName = userUpdateDto.LastName;

            if (userUpdateDto.Password.Length > 0)
            {
                localUser.PasswordHash = _userManager.PasswordHasher.HashPassword(localUser, userUpdateDto.Password);
            }

            await _userManager.UpdateAsync(localUser);
        }
        else
        {
            throw new Application.Exceptions.NotFoundException("User not found", userId);
        }
    }

    public async Task<bool> Exists(string id)
    {
        var entity = await GetByIdAsync(id);
        return entity != null;
    }
}