using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MoonCoreUI.Attributes;

namespace Pollifyr.App.Models.Forms.Settings;

public class SettingsForm
{
    [Required(ErrorMessage = "You need to specify a name")]
    [Section("Information")]
    public string AppName { get; set; }
    
    [Description("Use an imprint for this app")]
    [Section("Information")]
    public bool UseImprint { get; set; }
    
    [Description("Show the developer credits for this app")]
    [Section("Information")]
    public bool ShowCredits { get; set; }
    
    [Description("Allow Registration for new accounts")]
    [Section("Properties")]
    public bool AllowRegister { get; set; }
    
    [Description("The amount of surveys visible on the homepage.")]
    [Required(ErrorMessage = "Please add a number for visible surveys on home page")]
    [Section("Properties")]
    public int VisibleSurveysOnHomePage { get; set; }
}