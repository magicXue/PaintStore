using System;
using PaintStore.Models;

namespace PaintStore.Models.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> AddUserToDbAsync(User user);
}
