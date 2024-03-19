using Pollifyr.App.Helpers;
using CommonMark;
using Microsoft.AspNetCore.Components;

namespace Pollifyr.App.Services.Partials;

public class ImprintService
{
    private MarkupString Imprint;
    private string path = PathBuilder.File(PathBuilder.File("storage", "imprint.md"));
    
    public ImprintService()
    {
        Imprint = Reload();
    }

    public MarkupString Get()
    {
        return Imprint;
    }
    
    
    public void Set(string newImprintMarkdown)
    {
        using (StreamWriter writer = new(path))
        {
            writer.WriteLine(newImprintMarkdown);
            writer.Close();
        }
        
        
        
        Imprint = Reload();
    }
    
    
    public MarkupString Reload()
    {
        string[] lines = File.ReadAllLines(path);

        string markdownImprint = string.Join("\r\n", lines);

        MarkupString htmlString = (MarkupString)CommonMarkConverter.Convert(markdownImprint);
        
        return htmlString;
    }

    
}