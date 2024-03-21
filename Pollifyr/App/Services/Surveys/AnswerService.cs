using Pollifyr.App.Database.Models;
using Pollifyr.App.Repository;

namespace Pollifyr.App.Services.Surveys;

public class AnswerService
{
    public Repository<Answer> Answers;

    public AnswerService(Repository<Answer> answers)
    {
        Answers = answers;
    }

    public Answer? GetById(int id)
    {
        return Answers.Get().FirstOrDefault(x => x.Id == id);
    }

    public List<Answer> GetAllFromQuestion(Question question)
    {
        return Answers.Get().Where(x => x.QuestionId == question.Id).ToList();
    }

    public void Delete(Answer answer)
    {
        Answers.Delete(answer);
    }

    public void DeleteAllFromQuestion(int questionId)
    {
        var answers = Answers.Get().Where(x => x.QuestionId == questionId).ToList();

        foreach (var answer in answers)
        {
            Delete(answer);
        }
    }
}