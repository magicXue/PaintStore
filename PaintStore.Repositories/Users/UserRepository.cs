using System;
using Microsoft.EntityFrameworkCore;
using PaintStore.DataAccess;
using PaintStore.Models;
using PaintStore.Models.Interfaces.Repositories;

namespace PaintStore.Repositories.Users;

public class UserRepository:IUserRepository
{
    private PaintStoreDbContext _dbContext;

    public UserRepository(PaintStoreDbContext paintStoreDb)
    {
        _dbContext = paintStoreDb;
    }
    public async Task<User> AddUserToDbAsync(User user)
    {
        //Add only mark user to be added
        await _dbContext.Users.AddAsync(user);

        //save to db
        int changes = await _dbContext.SaveChangesAsync();
        if (changes > 0)
        {
            return user;
        }

        else
        {
            throw new DbUpdateException("Add User failed");
        }

    }
}
