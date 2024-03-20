using Pollifyr.App.Database.Models;
using Pollifyr.App.Repository;

namespace Pollifyr.App.Services.Surveys;

public class QuestionService
{
    public Repository<Question> Questions;

    public QuestionService(Repository<Question> questions)
    {
        Questions = questions;
    }

    public Question? GetById(int id)
    {
        return Questions.Get().FirstOrDefault(x => x.Id == id);
    }

    public void DeleteAllFromSurvey(int surveyId)
    {
        var questions = Questions.Get().Where(x => x.SurveyId == surveyId).ToList();
        
        foreach (var question in questions)
        {
            Questions.Delete(question);
        }
    }
}