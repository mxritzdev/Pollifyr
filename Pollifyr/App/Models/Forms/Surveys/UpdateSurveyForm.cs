using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pollifyr.App.Models.Forms.Surveys;

public class UpdateSurveyForm
{
    [Required(ErrorMessage = "You need to provide a title")]
    [MinLength(2, ErrorMessage = "The title is too short")]
    [MaxLength(40, ErrorMessage = "The title cannot be longer than 40 characters")]
    [Description("The name of your survey")]
    public string Name { get; set; }

    [Description("This toggles if the survey is visible to the public")]
    public bool Visible { get; set; } = false;
}