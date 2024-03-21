using Logging.Net;
using Pollifyr.App.Services.Partials;
using Pollifyr.App.Database;
using Pollifyr.App.Helpers;
using Pollifyr.App.Repository;
using Pollifyr.App.Services;

// Configure Logger
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

// Imprint
ImprintHelper imprintHelper = new();
await imprintHelper.Perform();

builder.Services.AddSingleton<ImprintService>();

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

app.Services.GetRequiredService<ConfigService>();

app.Run();