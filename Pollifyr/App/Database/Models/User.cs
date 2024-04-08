namespace Pollifyr.App.Database.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";

    public string CompletedSurveys { get; set; } = "";
    
    public bool Admin { get; set; } = false;
    public DateTime TokenValidTimestamp { get; set; } = DateTime.UtcNow.AddMinutes(-5);
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public string LastIp { get; set; } = "";
}