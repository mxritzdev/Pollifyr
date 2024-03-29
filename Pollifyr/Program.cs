using Logging.Net;
using Microsoft.AspNetCore.Components;
using Pollifyr.App.Services.Partials;
using Pollifyr.App.Database;
using Pollifyr.App.Helpers.Startup;
using Pollifyr.App.Helpers.Identity;
using Pollifyr.App.Repository;
using Pollifyr.App.Services;
using Pollifyr.App.Services.Auth;


Console.WriteLine("Pollifyr");
Console.WriteLine($"Copyright © {DateTime.UtcNow.Year} mxritz.xyz");
Console.WriteLine();

// Configure Logger
Logger.UseSBLogger();



var builder = WebApplication.CreateBuilder(args);

// Services / Config

ConfigHelper configHelper = new();
await configHelper.Perform();
ConfigService configService = new();
builder.Services.AddSingleton<ConfigService>();

// Services / Database

DbHelper databaseCheckup = new (configService);
await databaseCheckup.Perform();
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddScoped(typeof(Repository<>));

// Services / Imprint

ImprintHelper imprintHelper = new();
await imprintHelper.Perform();
builder.Services.AddSingleton<ImprintService>();

// Services /  Identity
builder.Services.AddSingleton<DateTimeService>();
builder.Services.AddScoped<CookieService>();
builder.Services.AddScoped<IdentityService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("Ztirom221008"));
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
