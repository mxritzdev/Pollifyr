namespace Pollifyr.App.Models.Forms;

public class ChangePasswordForm
{
    public string CurrentPassword { get; set; }

    public string NewPassword { get; set; }

    public string RepeatNewPassword { get; set; }
}