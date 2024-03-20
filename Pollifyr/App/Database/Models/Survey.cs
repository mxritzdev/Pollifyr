namespace Pollifyr.App.Database.Models;

public class Survey
{
    public int Id { get; set; }

    public string Title { get; set; } = "";

    public bool Visible { get; set; } = false;
}