using MoonCore.Abstractions;
using Pollifyr.App.Database.Models;

namespace Pollifyr.App.Services.Surveys;

public class SurveyService
{
    public Repository<Survey> Surveys;

    private QuestionService QuestionService;

    public SurveyService(Repository<Survey> surveys, QuestionService questionService)
    {
        Surveys = surveys;
        QuestionService = questionService;
    }

    public List<Survey> GetAll()
    {
        return Surveys.Get().ToList();
    }

    public async Task<Survey?> GetById(int id)
    {
        return Surveys.Get().FirstOrDefault(x => x.Id == id);
    }

    public async Task Visibility(Survey survey, bool visible)
    {
        survey.Visible = visible;
        Surveys.Update(survey);
    }

    public async Task New(Survey survey)
    {
        Surveys.Add(survey);
    }

    public async Task Delete(Survey survey)
    {
        // Delete all Questions corresponding to the survey
        await QuestionService.DeleteAllFromSurvey(survey);
        
        // Then delete the survey
        Surveys.Delete(survey);
    }

    public async Task Update(Survey survey)
    {
        Surveys.Update(survey);
    }
    
    
}