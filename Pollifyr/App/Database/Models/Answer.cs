namespace Pollifyr.App.Database.Models;

public class Answer
{
    public int Id { get; set; }

    public string Text { get; set; } = "";

    public int QuestionId { get; set; }

    public int Answers { get; set; } = 0;

    public int SortingId { get; set; } = 0;
}