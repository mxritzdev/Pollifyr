using MoonCore.Helpers;
using Pollifyr.App.Config;
using Pollifyr.App.Helpers;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;


namespace Pollifyr.App.Services;

public class ConfigService
{
    private readonly string Path = PathBuilder.File("storage", "config.json");
    private ConfigModel? Data;
    
    public ConfigService()
    {
        Reload();
    }
    
    public void Reload()
    {
        if(!File.Exists(Path))
            File.WriteAllText(Path, "{}");

        var text = File.ReadAllText(Path);
        Data = JsonConvert.DeserializeObject<ConfigModel>(text) ?? new();
        Save();
    }

    public void Set(ConfigModel model)
    {
        Data = model;
        Save();
    }

    public void Save()
    {
        var text = JsonConvert.SerializeObject(Data, Formatting.Indented);
        File.WriteAllText(Path, text);
    }

    public ConfigModel Get()
    {
        return Data!;
    }
    
    
}