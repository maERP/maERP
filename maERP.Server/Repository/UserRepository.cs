#nullable disable

using Microsoft.AspNetCore.Identity;
using AutoMapper;
using maERP.Shared.Dtos.User;
using maERP.Server.Models;

namespace maERP.Server.Repository;

public interface IUserRepository
{
    Task<IQueryable<ApplicationUser>> GetAllAsync();
    Task<UserDetailDto> GetByIdAsync(string userId);
    Task<IEnumerable<IdentityError>> AddAsync(UserCreateDto userDto);
    Task<ApplicationUser> UpdateAsync(UserUpdateDto userDto);
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

    public async Task<IQueryable<ApplicationUser>> GetAllAsync()
    {
        await Task.CompletedTask;
        return _userManager.Users;
    }

    public async Task<UserDetailDto> GetByIdAsync(string userId)
    {
        await Task.CompletedTask;
        var user = _userManager.Users.Where(x => x.Id == userId).First<ApplicationUser>();
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

    public async Task<ApplicationUser> UpdateAsync(UserUpdateDto userUpdateDto)
    {
        var updateUser = _mapper.Map<ApplicationUser>(userUpdateDto);
        updateUser.Email = userUpdateDto.Email;

        var localUser = await _userManager.FindByEmailAsync(updateUser.Email);

        if (localUser.Id is not null)
        {
            await _userManager.UpdateAsync(updateUser);

            return updateUser;
        }

        throw new Exceptions.NotFoundException("User not found", "User not found");
    }
}