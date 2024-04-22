using Pollifyr.App.Database.Models;

namespace Pollifyr.App.Models.Survey;

public class QuestionAndAnswer
{
    public required Question Question { get; set; }

    public Answer? Answer { get; set; } = null;
}