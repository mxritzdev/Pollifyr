using Logging.Net;

namespace Lephu_Umfrage.App.Helpers;

public class ImprintHelper
{
    private readonly string defaultImprint = "# Impressum\r\n---\r\nLorem ipsum dolor sit amet";
    
    public ImprintHelper()
    {
    }

    public Task Perform()
    {
        Logger.Info("Checking for Imprint");
        var path = PathBuilder.File("storage", "imprint.md");

        if (!Directory.Exists(PathBuilder.Dir("storage")))
        {
            Directory.CreateDirectory(PathBuilder.Dir("storage"));
        }
        
        if (File.Exists(path))
        {
            Logger.Info("Config file exists, continuing startup");
            return Task.CompletedTask;
        }

        FileStream fs = File.Create(path);
        fs.Close();
        
        using (StreamWriter writer = new(path))
        {
            writer.WriteLine(defaultImprint);
            writer.Close();
        }
        
        return Task.CompletedTask;
    }
}