using MoonCore.Abstractions;
using Pollifyr.App.Database.Models;

namespace Pollifyr.App.Services.Surveys;

public class AnswerService
{
    public Repository<Answer> Answers;

    public AnswerService(Repository<Answer> answers)
    {
        Answers = answers;
    }

    public async Task<Answer?> GetById(int id)
    {
        return Answers.Get().FirstOrDefault(x => x.Id == id);
    }

    public async Task<List<Answer>> GetAllFromQuestion(Question question)
    {
        return Answers.Get().Where(x => x.QuestionId == question.Id).ToList();
    }

    public async Task Delete(Answer answer)
    {
        Answers.Delete(answer);
    }
    
    public async Task Update(Answer answer)
    {
        Answers.Update(answer);
    }

    public async Task Add(Answer answer)
    {
        Answers.Add(answer);
    }

    public async Task DeleteAllFromQuestion(int questionId)
    {
        var answers = Answers.Get().Where(x => x.QuestionId == questionId).ToList();

        foreach (var answer in answers)
        {
            Delete(answer);
        }
    }
}