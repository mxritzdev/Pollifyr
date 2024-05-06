using Pollifyr.App.Database.Models;

namespace Pollifyr.App.Models.Forms;

public class DetailsForm
{
    public User User { get; set; }
    
    public string NewEmail { get; set; }
    
    public string NewUsername { get; set; }
}