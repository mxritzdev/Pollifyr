using JWT.Algorithms;
using JWT.Builder;
using MoonCore.Abstractions;
using Pollifyr.App.Database.Models;
using Pollifyr.App.Exceptions;
using Pollifyr.App.Helpers.Utils;
using Pollifyr.App.Services.Partials;

namespace Pollifyr.App.Services.Auth;

public class AuthService
{
    private readonly Repository<User> Users;
    private readonly CookieService CookieService;
    private readonly DateTimeService DateTimeService;

    private readonly string Secret;

    public AuthService(Repository<User> users, CookieService cookieService, DateTimeService dateTimeService, ConfigService configService)
    {
        Users = users;
        CookieService = cookieService;
        DateTimeService = dateTimeService;
        Secret = configService.Get().Security.Secret;
    }


    public async Task<string> Register(string email, string password, string username)
    {
        
        // Check if the email is already taken
        var emailTaken = Users.Get().FirstOrDefault(x => x.Email == email) != null;

        if (emailTaken)
        {
            throw new DisplayException("The email is already in use");
        }
        
        var user = Users.Add(new()
        {
            Email = email.ToLower(),
            Password = BCrypt.Net.BCrypt.HashPassword(password),
            Username = username,
            TokenValidTimestamp = DateTimeService.GetCurrent().AddDays(-5),
        });
        

        return await GenerateToken(user);
    }
    
    public async Task<string> Login(string email, string password, string totpCode = "")
    {
      
        var user = Users.Get()
            .FirstOrDefault(
                x => x.Email.Equals(
                    email
                )
            );
        
        return await GenerateToken(user!);
    }
    
    
    public async Task<string> GenerateToken(User user)
    {
        var token = JwtBuilder.Create()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret(Secret)
            .AddClaim("exp", new DateTimeOffset(DateTimeService.GetCurrent().AddDays(10)).ToUnixTimeSeconds())
            .AddClaim("iat", DateTimeService.GetCurrentUnixSeconds())
            .AddClaim("userid", user.Id)
            .Encode();

        return token;
    }
    
    public Task ChangePassword(User user, string password, bool isSystemAction = false)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(password);
        user.TokenValidTimestamp = DateTimeService.GetCurrent();
        Users.Update(user);

        return Task.CompletedTask;
    }
    
    public async Task ResetPassword(string email)
    {
        email = email.ToLower();
        
        var user = Users
            .Get()
            .FirstOrDefault(x => x.Email == email);

        if (user == null)
            throw new DisplayException("A user with this email can not be found");

        var newPassword = StringHelper.GenerateString(16);
        await ChangePassword(user, newPassword, true);
        
    }

    public async Task ChangeDetails(User user, string email, string username, bool admin)
    {
        user.Email = email;

        user.Username = username;

        user.Admin = admin;
        
        Users.Update(user);
    }
    
    
}

