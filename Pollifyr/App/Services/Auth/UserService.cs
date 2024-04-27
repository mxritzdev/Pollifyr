using Microsoft.EntityFrameworkCore;
using Pollifyr.App.Database.Models;
using MoonCore.Abstractions;

namespace Pollifyr.App.Services.Auth;

public class UserService
{
    public Repository<User> Users;

    public UserService(Repository<User> users)
    {
        Users = users;
    }

    public async Task<User?> GetById(int id)
    {
        return Users.Get().FirstOrDefault(x => x.Id == id);
    }

    public async Task Update(User user)
    {
        Users.Update(user);
    }

    public async Task Delete(User user)
    {
        Users.Delete(user);
    }
}