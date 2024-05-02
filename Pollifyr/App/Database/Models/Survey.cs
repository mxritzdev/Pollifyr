namespace Pollifyr.App.Database.Models;

public class Survey
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public bool Attendable { get; set; } = false;
    
    public bool Visible { get; set; } = false;

    public int Attends { get; set; } = 0;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}