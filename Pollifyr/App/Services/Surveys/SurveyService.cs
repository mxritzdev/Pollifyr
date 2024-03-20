using Pollifyr.App.Database.Models;
using Pollifyr.App.Repository;

namespace Pollifyr.App.Services.Surveys;

public class SurveyService
{
    private Repository<Survey> Surveys;

    public SurveyService(Repository<Survey> surveys)
    {
        Surveys = surveys;
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

    public void New(string title)
    {
        // Create a new Survey in the Database
        Survey survey = new();

        survey.Title = title;

        Surveys.Add(survey);
    }

    public void Delete(int id)
    {
        // Delete all Questions corresponding to the survey
        
        // Then delete the survey
        Survey survey = new();

        survey.Id = id;
        
        Surveys.Delete(survey);
    }
    
    
}