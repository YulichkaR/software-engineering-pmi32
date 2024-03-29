﻿using AutoMapper;
using EShop.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.User;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly UserManager<Domain.Models.User> _userManager;

    public UserService( IMapper mapper, UserManager<Domain.Models.User> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<UserDto> GetUserById(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            throw new Exception("User not found");
        }
        return _mapper.Map<UserDto>(user);
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var userRolesDictionary = new Dictionary<Guid, string>();
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userRolesDictionary.Add(user.Id, roles.First() );
        }
        var result = users.ConvertAll(u => new UserDto
        {
            Id = u.Id,
            UserName = u.UserName,
            Email = u.Email,
            UserType = userRolesDictionary[u.Id]
        });
        return result;
    }

    public async Task DeleteUser(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception("Failed to delete user");
        }
    }
}