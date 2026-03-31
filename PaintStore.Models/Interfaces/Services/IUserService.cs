using System;
using PaintStore.Models;

namespace PaintStore.Models.Interfaces.Services;

public interface IUserService
{
    Task<User> CreateUserAsync(User user);
}
