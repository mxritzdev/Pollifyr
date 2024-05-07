using System.Diagnostics.Contracts;

namespace Pollifyr.App.Models.Forms;

public class RegisterForm
{
    public string Username { get; set; } = "";
    
    public string Email { get; set; } = "";
    
    public string Password { get; set; } = "";
}