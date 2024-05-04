using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pollifyr.App.Models.Forms.Surveys;

public class CreateSurveyForm
{
    [Required(ErrorMessage = "You need to provide a name")]
    [MinLength(2, ErrorMessage = "The name is too short")]
    [MaxLength(40, ErrorMessage = "The name cannot be longer than 40 characters")]
    [Description("The name of your survey")]
    public string Name { get; set; }

    [Required(ErrorMessage = "You need to provide a description")]
    [MinLength(5, ErrorMessage = "Your description is too short")]
    [MaxLength(200, ErrorMessage = "Your description cannot be longer than 200 characters")]
    [Description("A description for your survey")]
    public string Description { get; set; }
    
    [Description("This toggles if the survey's results are visible to the public")]
    public bool Visible { get; set; } = false;
    
    [Description("This toggles if the survey is attendable")]
    public bool Attendable { get; set; } = false;
}