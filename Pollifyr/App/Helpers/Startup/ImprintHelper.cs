using MoonCore.Helpers;

namespace Pollifyr.App.Helpers.Startup;

public class ImprintHelper
{
    private readonly string defaultImprint = "# Impressum\r\n---\r\nLorem ipsum dolor sit amet";
    
    public ImprintHelper()
    {
    }

    public Task Perform()
    {
        var path = PathBuilder.File("storage", "imprint.md");

        if (!Directory.Exists(PathBuilder.Dir("storage")))
        {
            Directory.CreateDirectory(PathBuilder.Dir("storage"));
        }
        
        if (File.Exists(path))
        {
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