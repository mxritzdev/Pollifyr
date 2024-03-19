namespace Pollifyr.App.Database.Models;

public class Question
{
    public int Id { get; set; }

    public string Text { get; set; } = "";

    public int NoAnswers { get; set; } = 0;
}