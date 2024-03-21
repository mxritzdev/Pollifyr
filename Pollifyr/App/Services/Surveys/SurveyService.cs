using Pollifyr.App.Database.Models;
using Pollifyr.App.Repository;

namespace Pollifyr.App.Services.Surveys;

public class SurveyService
{
    private Repository<Survey> Surveys;

    private QuestionService QuestionService;

    public SurveyService(Repository<Survey> surveys, QuestionService questionService)
    {
        Surveys = surveys;
        QuestionService = questionService;
    }

    public Survey? GetById(int id)
    {
        // Get a survey by id
        
        return Surveys.Get().FirstOrDefault(x => x.Id == id);
    }

    public void Visibility(int id, bool visible)
    {
        // Toggle a surveys visibility
        // Visible   = SurveyStates.Visible
        // Invisible = SurveyStates.Invisible
        var survey = GetById(id);

        if (survey == null) return;
        
        survey.Visible = visible;
        Surveys.Update(survey);
    }

    public void New(Survey survey)
    {
        // Create a new Survey in the Database
        Surveys.Add(survey);
    }

    public void Delete(Survey survey)
    {
        // Delete all Questions corresponding to the survey
        QuestionService.DeleteAllFromSurvey(survey);
        
        // Then delete the survey
        Surveys.Delete(survey);
    }
    
    
}