using Lephu_Umfrage.App.Database;
using Lephu_Umfrage.App.Helpers;
using Lephu_Umfrage.App.Repository;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Logging.Net;
using Lephu_Umfrage.App.Services;

// Configurate Logger
Logger.UseSBLogger();


// Initialize Config
ConfigHelper configHelper = new();
await configHelper.Perform();
ConfigService configService = new();


var builder = WebApplication.CreateBuilder(args);

// Services / Config
builder.Services.AddSingleton<ConfigService>();

// Services / Database
DbHelper databaseCheckup = new (configService);
await databaseCheckup.Perform();

builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddScoped(typeof(Repository<>));


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.Services.GetRequiredService<ConfigService>();

app.Run();