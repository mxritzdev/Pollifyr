using System.Text;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;
using MoonCore.Helpers;
using MoonCore.Abstractions;
using MoonCoreUI.Services;
using Pollifyr.App.Database.Models;

namespace Pollifyr.App.Services.Auth;

public class IdentityService
{
    private readonly Repository<User> Users;
    private readonly CookieService CookieService;
    private readonly IHttpContextAccessor HttpContextAccessor;
    
    public User User { get; private set; }
    public string Ip { get; private set; } = "N/A";
    
    public bool LoggedIn { get; private set; } = false;
    
    public bool IsAdmin { get; set; } = false;
    
    private readonly string Secret;

    public IdentityService(Repository<User> users, CookieService cookieService, ConfigService configService, IHttpContextAccessor httpContextAccessor)
    {
        Users = users;
        CookieService = cookieService;
        HttpContextAccessor = httpContextAccessor;
        Secret = configService.Get().Security.Secret;
        _ = Load();
    }
    
    public async Task Load()
    {
        await LoadIp();
        await LoadUser();
    }

    
    private async Task LoadUser(){
        try
        {
            var token = "none";

            // Load token via http context if available
            if (HttpContextAccessor.HttpContext != null)
            {
                var request = HttpContextAccessor.HttpContext.Request;

                if (request.Cookies.ContainsKey("token"))
                {
                    token = request.Cookies["token"];
                }
            }
            else // if not we check the cookies manually via js. this may not often work
            {
                token = await CookieService.GetValue("token", "none");
            }

            if (token == "none")
            {
                LoggedIn = false;
            }

            if (string.IsNullOrEmpty(token))
                return;

            string json;

            try
            {
                json = JwtBuilder.Create()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret(Secret)
                    .Decode(token);
            }
            catch (TokenExpiredException)
            {
                return;
            }
            catch (SignatureVerificationException)
            {
                Logger.Warn($"Detected a manipulated JWT: {token}", "security");
                return;
            }
            catch (Exception e)
            {
                Logger.Error("Error reading jwt");
                Logger.Error(e);
                return;
            }

            // To make it easier to use the json data
            var data = new ConfigurationBuilder().AddJsonStream(
                new MemoryStream(Encoding.ASCII.GetBytes(json))
            ).Build();

            var userid = data.GetValue<int>("userid");
            var user = Users.Get().FirstOrDefault(x => x.Id == userid);

            if (user == null)
            {
                Logger.Warn(
                    $"Cannot find user with the id '{userid}' in the database. Maybe the user has been deleted or a token has been successfully faked by a hacker", "security");
                return;
            }

            var iat = data.GetValue<long>("iat", -1);

            if (iat == -1)
            {
                Logger.Debug("Legacy token found (without the time the token has been issued at)");
                return;
            }

            var iatD = DateTimeOffset.FromUnixTimeSeconds(iat).ToUniversalTime().DateTime;

            if (iatD < user.TokenValidTimestamp)
                return;

            User = user;

            ConstructPermissions();

            
            User.LastIp = Ip;
            Users.Update(User);

            LoggedIn = true;
        }
        catch (Exception e)
        {
            Logger.Error("Unexpected error while processing token");
            Logger.Error(e);
            return;
        }
    }
    
    private Task LoadIp()
    {
        if (HttpContextAccessor.HttpContext == null)
        {
            Ip = "N/A";
            return Task.CompletedTask;
        }

        if (HttpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Real-IP"))
        {
            Ip = HttpContextAccessor.HttpContext.Request.Headers["X-Real-IP"]!;
            return Task.CompletedTask;
        }

        Ip = HttpContextAccessor.HttpContext.Connection.RemoteIpAddress!.ToString();
        return Task.CompletedTask;
    }

    private void ConstructPermissions()
    {
        IsAdmin = User.Admin;
    }
    
    
}