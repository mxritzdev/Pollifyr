using Pollifyr.App.Config;
using Pollifyr.App.Models.Forms.Settings;
using Pollifyr.App.Services;

namespace Pollifyr.App.Helpers.Utils;

public class SettingsMapper
{
    public static async Task<SettingsForm> Map(ConfigModel configModel)
    {
        SettingsForm settingsForm = new()
        {
            AppName = configModel.Information.AppName,
            UseImprint = configModel.Information.UseImprint,
            ShowCredits = configModel.Information.ShowCredits,
            AllowRegister = configModel.Properties.AllowRegister,
            VisibleSurveysOnHomePage = configModel.Information.VisibleSurveysOnHomePage
        };

        return settingsForm;
    }

    public static async Task<ConfigModel> ReverseMap(ConfigModel currentConfig, SettingsForm settingsForm)
    {

        currentConfig.Information.AppName = settingsForm.AppName;
        currentConfig.Information.ShowCredits = settingsForm.ShowCredits;
        currentConfig.Information.UseImprint = settingsForm.UseImprint;
        currentConfig.Properties.AllowRegister = settingsForm.AllowRegister;
        currentConfig.Information.VisibleSurveysOnHomePage = settingsForm.VisibleSurveysOnHomePage;
        
        return currentConfig;
    }
}