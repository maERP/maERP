#nullable disable

using Microsoft.AspNetCore.Identity;
using AutoMapper;
using maERP.Shared.Dtos.User;
using maERP.Server.Models;
using maERP.Shared.Pages;
using Microsoft.EntityFrameworkCore;

namespace maERP.Server.Repository;

public interface IUserRepository
{
    Task<List<UserListDto>> GetAllAsync();
    Task<UserDetailDto> GetByIdAsync(string userId);
    Task<IEnumerable<IdentityError>> AddAsync(UserCreateDto userDto);
    Task<ApplicationUser> UpdateAsync(ApplicationUser userDto);
}

public class UserRepository : IUserRepository
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        this._mapper = mapper;
        this._userManager = userManager;
    }

    public async Task<List<UserListDto>> GetAllAsync()
    {
        var users = await _userManager.Users.AsNoTracking().ToListAsync();
        return _mapper.Map<List<UserListDto>>(users);
    }

    public async Task<UserDetailDto> GetByIdAsync(string userId)
    {
        var user = await _userManager.Users
            .Where(x => x.Id == userId)
            .AsNoTracking()
            .FirstAsync<ApplicationUser>();

        var userDto = _mapper.Map<UserDetailDto>(user);
        return userDto;
    }

    public async Task<IEnumerable<IdentityError>> AddAsync(UserCreateDto userCreateDto)
    {
        var _user = _mapper.Map<ApplicationUser>(userCreateDto);
        _user.Email = userCreateDto.Email;

        var result = await _userManager.CreateAsync(_user, userCreateDto.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(_user, "User");
        }

        return result.Errors;
    }

    public async Task<ApplicationUser> UpdateAsync(ApplicationUser updateUser)
    {
        var localUser = await _userManager.FindByIdAsync(updateUser.Email);

        if (localUser.Id is not null)
        {
            await _userManager.UpdateAsync(updateUser);

            return updateUser;
        }

        throw new Exceptions.NotFoundException("User not found", "User not found");
    }
}