using CommonMark;
using Microsoft.AspNetCore.Components;
using MoonCore.Helpers;
using Pollifyr.App.Helpers;
using Pollifyr.App.Helpers.Utils;

namespace Pollifyr.App.Services.Partials;

public class ImprintService
{
    private MarkupString Imprint;
    private string MarkdownImprint;
    private readonly string Path = PathBuilder.File("storage", "imprint.md");
    
    public ImprintService()
    {
        Imprint = Load();
    }

    public async Task<MarkupString> Get()
    {
        return Imprint;
    }

    public async Task<string> GetMarkdown()
    {
        return MarkdownImprint;
    }
    
    public async Task Set(string newImprintMarkdown)
    {
        await using (StreamWriter writer = new(Path))
        {
            await writer.WriteLineAsync(newImprintMarkdown);
            writer.Close();
        }
        
        Reload();
    }
    
    
    public MarkupString Load()
    {
        string[] lines = File.ReadAllLines(Path);

        MarkdownImprint = string.Join("\r\n", lines);

        MarkupString htmlString = (MarkupString)CommonMarkConverter.Convert(MarkdownImprint);
        
        return htmlString;
    }

    public void Reload()
    {
        Imprint = Load();
    }

    
}