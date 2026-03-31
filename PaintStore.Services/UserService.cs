using System;
using PaintStore.Models;
using PaintStore.Models.Interfaces.Repositories;
using PaintStore.Models.Interfaces.Services;

namespace PaintStore.Services;

public class UserService : IUserService
{
    private IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateUserAsync(User user)
    {      
        User newUser = await _userRepository.AddUserToDbAsync(user);
        return newUser;
    }
}
