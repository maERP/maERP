﻿using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using maERP.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

    public async Task<ApplicationUser?> GetByIdAsync(string userId)
    {
        return await _userManager.Users
            .Where(x => x.Id == userId)
            .AsNoTracking()
            .FirstAsync();
    }
    
    public async Task<IEnumerable<IdentityError>> CreateAsync(ApplicationUser userToCreate, string password)
    {
        userToCreate.PasswordHash = _userManager.PasswordHasher.HashPassword(userToCreate, password);

        var result = await _userManager.CreateAsync(userToCreate, password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(userToCreate, "User");
        }

        return result.Errors;
    }
    
    public async Task<ApplicationUser> UpdateWithDetailsAsync(ApplicationUser userUpdateDto)
    {
        var localUser = await _userManager.FindByIdAsync(userUpdateDto.Id);

        if (localUser != null)
        {
            userUpdateDto.Email = userUpdateDto.Email?.ToLower();
            
            localUser.Email = userUpdateDto.Email;
            localUser.UserName = userUpdateDto.Email;
            localUser.Firstname = userUpdateDto.Firstname;
            localUser.Lastname = userUpdateDto.Lastname;

            /*
            if (userUpdateDto.Password.Length > 0)
            {
                localUser.PasswordHash = _userManager.PasswordHasher.HashPassword(localUser, userUpdateDto.Password);
            }*/

            await _userManager.UpdateAsync(localUser);
        }
        else
        {
            throw new NotFoundException("User not found", userUpdateDto.Id);
        }

        return userUpdateDto;
    }

    public async Task<bool> Exists(string id)
    {
        var entity = await GetByIdAsync(id);
        return entity != null;
    }
}