using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pollifyr.App.Models.Forms.Users;

public class UpdateUserForm
{
    [Required(ErrorMessage = "You need to provide an email address")]
    [EmailAddress(ErrorMessage = "You need to enter a valid email address")]
    [Description("The user's email")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "You need to provide an username")]
    [MinLength(6, ErrorMessage = "The username is too short")]
    [MaxLength(20, ErrorMessage = "The username cannot be longer than 20 characters")]
    [RegularExpression("^[a-z][a-z0-9]*$", ErrorMessage = "Usernames can only contain lowercase characters and numbers and should not start with a number")]
    [Description("The user's username")]
    public string Username { get; set; }
    
    [Description("This toggles if the user has admin permissions")]
    public bool Admin { get; set; } = false;
    
    [Description("The surveys the user has completed, do not change this unless you know what you are doing")]
    public string CompletedSurveys { get; set; }
}