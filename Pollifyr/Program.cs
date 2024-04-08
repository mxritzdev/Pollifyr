using Logging.Net;
using Microsoft.AspNetCore.Components;
using MoonCore.Abstractions;
using MoonCoreUI.Services;
using Pollifyr.App.Services.Partials;
using Pollifyr.App.Database;
using Pollifyr.App.Helpers.Startup;
using Pollifyr.App.Helpers.Identity;
using Pollifyr.App.Repository;
using Pollifyr.App.Services;
using Pollifyr.App.Services.Auth;
using CookieService = Pollifyr.App.Services.Auth.CookieService;

Console.WriteLine();
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
builder.Services.AddScoped(typeof(Repository<>), typeof(GenericRepository<>));

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

ToastService.Prefix = "pollifyr.toasts";
ModalService.Prefix = "pollifyr.modals";
AlertService.Prefix = "pollifyr.alerts";
ClipboardService.Prefix = "pollifyr.clipboard";
FileDownloadService.Prefix = "pollifyr.utils";

// Services / Alerts
builder.Services.AddScoped<AlertService>();

// Mooncore
builder.Services.AddScoped<ClipboardService>();
builder.Services.AddScoped<ModalService>();
builder.Services.AddScoped<ToastService>();

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(configService.Get().Information.Port);
});

Logger.Info("Running on http://localhost:"+configService.Get().Information.Port);
    
// Required Services
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
