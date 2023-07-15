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
    // Task<ApplicationUser> UpdateAsync(ApplicationUser userDto);
    Task<bool> Exists(string id);
    Task UpdateWithDetailsAsync(string id, UserUpdateDto userUpdateDto);
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

    public async Task UpdateWithDetailsAsync(string userId, UserUpdateDto userUpdateDto)
    {
        var localUser = await _userManager.FindByIdAsync(userId);

        if (localUser.Id is not null)
        {
            localUser.Email = userUpdateDto.Email;
            localUser.UserName = userUpdateDto.Email.ToLower();
            localUser.FirstName = userUpdateDto.FirstName;
            localUser.LastName = userUpdateDto.LastName;

            if(userUpdateDto.Password.Length > 0)
            {
                localUser.PasswordHash = _userManager.PasswordHasher.HashPassword(localUser, userUpdateDto.Password);
            }

            await _userManager.UpdateAsync(localUser);
        }
        else
        {
            throw new Exceptions.NotFoundException("User not found", userId);
        }
    }

    public async Task<bool> Exists(string id)
    {
        var entity = await GetByIdAsync(id);
        return entity != null;
    }
}