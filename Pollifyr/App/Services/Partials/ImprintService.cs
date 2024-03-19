using CommonMark;
using Microsoft.AspNetCore.Components;
using Pollifyr.App.Helpers;

namespace Pollifyr.App.Services.Partials;

public class ImprintService
{
    private MarkupString Imprint;
    private readonly string Path = PathBuilder.File(PathBuilder.File("storage", "imprint.md"));
    
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
        using (StreamWriter writer = new(Path))
        {
            writer.WriteLine(newImprintMarkdown);
            writer.Close();
        }
        
        
        
        Imprint = Reload();
    }
    
    
    public MarkupString Reload()
    {
        string[] lines = File.ReadAllLines(Path);

        string markdownImprint = string.Join("\r\n", lines);

        MarkupString htmlString = (MarkupString)CommonMarkConverter.Convert(markdownImprint);
        
        return htmlString;
    }

    
}