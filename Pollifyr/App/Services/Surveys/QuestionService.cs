using MoonCore.Abstractions;
using Pollifyr.App.Database.Models;

namespace Pollifyr.App.Services.Surveys;

public class QuestionService
{
    public Repository<Question> Questions;

    private AnswerService AnswerService;
    public QuestionService(Repository<Question> questions, AnswerService answerService)
    {
        Questions = questions;
        AnswerService = answerService;
    }

    public async Task<Question?> GetById(int id)
    {
        return Questions.Get().FirstOrDefault(x => x.Id == id);
    }

    public async Task Add(Question question)
    {
        Questions.Add(question);
    }
    
    public async Task<List<Question>> GetAllFromSurvey(Survey survey)
    {
        return Questions.Get().Where(x => x.SurveyId == survey.Id).ToList();
    }

    public async Task Delete(Question question)
    {
        // Delete all corresponding Answers
        await AnswerService.DeleteAllFromQuestion(question);
        
        Questions.Delete(question);
    }

    public async Task Update(Question question)
    {
        Questions.Update(question);
    }
    
    public async Task DeleteAllFromSurvey(Survey survey)
    {
        var questions = Questions.Get().Where(x => x.SurveyId == survey.Id).ToList();
        
        foreach (var question in questions)
        {
            Delete(question);
        }
    }
}