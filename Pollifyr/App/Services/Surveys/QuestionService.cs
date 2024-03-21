using Pollifyr.App.Database.Models;
using Pollifyr.App.Repository;

namespace Pollifyr.App.Services.Surveys;

public class QuestionService
{
    private Repository<Question> Questions;

    private AnswerService AnswerService;
    public QuestionService(Repository<Question> questions, AnswerService answerService)
    {
        Questions = questions;
        AnswerService = answerService;
    }

    public Question? GetById(int id)
    {
        return Questions.Get().FirstOrDefault(x => x.Id == id);
    }

    private void Delete(Question question)
    {
        // Delete all corresponding Answers
        AnswerService.DeleteAllFromQuestion(question.Id);
        
        Questions.Delete(question);
    }
    
    public void DeleteAllFromSurvey(Survey survey)
    {
        var questions = Questions.Get().Where(x => x.SurveyId == survey.Id).ToList();
        
        foreach (var question in questions)
        {
            Delete(question);
        }
    }
}