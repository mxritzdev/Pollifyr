namespace Pollifyr.App.Database.Models;

public class Survey
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public bool Visible { get; set; } = false;
}