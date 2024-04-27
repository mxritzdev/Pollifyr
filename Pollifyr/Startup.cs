using Logging.Net;
using MoonCore.Abstractions;
using MoonCoreUI.Services;
using Pollifyr.App.Database;
using Pollifyr.App.Helpers.Startup;
using Pollifyr.App.Repository;
using Pollifyr.App.Services;
using Pollifyr.App.Services.Auth;
using Pollifyr.App.Services.Partials;
using Pollifyr.App.Services.Surveys;
using CookieService = Pollifyr.App.Services.Auth.CookieService;

namespace Pollifyr;

public class Startup
{
    private WebApplicationBuilder WebApplicationBuilder;
    private WebApplication WebApplication;
    private ConfigService configService;
    
    public async Task Init(string[] args)
    {
        WebApplicationBuilder = WebApplication.CreateBuilder(args);
        
        await SetupLogger();
        
        Logger.Info("Initializing application [0/7]");
        
        Logger.Info("Set up logger [1/7]");
        
        await PerformHelpers();
        
        Logger.Info("Performed helpers [2/7]");

        await AddDatabase();
        
        Logger.Info("Added database [3/7]");

        await AddServices();
        
        Logger.Info("Added services [4/7]");

        await Configure();
        
        Logger.Info("Configured kestrel [5/7]");

        WebApplication = WebApplicationBuilder.Build();
        
        Logger.Info("Built webapplication [6/7]");

        await ConfigurePipeline();
        
        Logger.Info("Configured pipeline [7/7]");

    }

    public async Task Start()
    {
        
        Logger.Info("Starting application...");
        Logger.Info("Running on http://localhost:" + configService.Get().Properties.Port);
        await WebApplication.RunAsync();
    }
    
    private async Task AddServices()
    {
        await AddSingletonServices();
        
        await AddScopedServices();

        await AddOtherServices();
    }

    private async Task AddSingletonServices()
    {
        WebApplicationBuilder.Services.AddSingleton<ConfigService>();
        WebApplicationBuilder.Services.AddSingleton<DateTimeService>();
        WebApplicationBuilder.Services.AddSingleton<ImprintService>();
    }
    
    private async Task AddScopedServices()
    {
        WebApplicationBuilder.Services.AddScoped<CookieService>();
        WebApplicationBuilder.Services.AddScoped<IdentityService>();
        WebApplicationBuilder.Services.AddScoped<AuthService>();
        WebApplicationBuilder.Services.AddScoped<UserService>();
        
        WebApplicationBuilder.Services.AddScoped<SurveyService>();
        WebApplicationBuilder.Services.AddScoped<QuestionService>();  
        WebApplicationBuilder.Services.AddScoped<AnswerService>();
        
        ToastService.Prefix = "pollifyr.toasts";
        ModalService.Prefix = "pollifyr.modals";
        AlertService.Prefix = "pollifyr.alerts";
        ClipboardService.Prefix = "pollifyr.clipboard";
        
        WebApplicationBuilder.Services.AddScoped<AlertService>();
        WebApplicationBuilder.Services.AddScoped<ClipboardService>();
        WebApplicationBuilder.Services.AddScoped<ModalService>();
        WebApplicationBuilder.Services.AddScoped<ToastService>();

    }
    
    private async Task AddOtherServices()
    {
        WebApplicationBuilder.Services.AddHttpContextAccessor();
        WebApplicationBuilder.Services.AddRazorPages();
        WebApplicationBuilder.Services.AddServerSideBlazor();
    }
    
    private async Task ConfigurePipeline()
    {
        if (configService.Get().Properties.UseHsts)
        {
            WebApplication.UseHsts();
        }

        WebApplication.UseHttpsRedirection();
        WebApplication.UseStaticFiles();
        WebApplication.UseRouting();
        WebApplication.MapBlazorHub();
        WebApplication.MapFallbackToPage("/_Host");
    }

    private async Task Configure()
    {
        WebApplicationBuilder.WebHost.ConfigureKestrel(serverOptions =>
        {
            serverOptions.ListenAnyIP(configService.Get().Properties.Port);
        });
    } 

    private async Task SetupLogger()
    {
        Logger.UseSBLogger();
    }
    
    private async Task AddDatabase()
    {
        WebApplicationBuilder.Services.AddDbContext<DatabaseContext>();
        WebApplicationBuilder.Services.AddScoped(typeof(Repository<>), typeof(GenericRepository<>));
    }

    private async Task PerformHelpers()
    {
        ConfigHelper configHelper = new();
        await configHelper.Perform();
        
        configService = new ConfigService();
        
        DbHelper databaseCheckup = new (configService);
        await databaseCheckup.Perform();
        
        ImprintHelper imprintHelper = new();
        await imprintHelper.Perform();
    }
}