using Microsoft.EntityFrameworkCore;
using MoonCore.Abstractions;
using Pollifyr.App.Database.Models;

namespace Pollifyr.App.Services.Surveys;

public class AnswerService
{
    public Repository<Answer> Answers;

    public Repository<Question> Questions;

    public AnswerService(Repository<Answer> answers, Repository<Question> questions)
    {
        Answers = answers;
        Questions = questions;
    }

    public async Task<Answer?> GetById(int id)
    {
        return Answers.Get().FirstOrDefault(x => x.Id == id);
    }

    public async Task<List<Answer>> GetAllFromSurvey(Survey survey)
    {
        List<Answer> answers = new List<Answer>();
        
        List<Question> questions = Questions.Get().Where(x => x.SurveyId == survey.Id).ToList();

        foreach (var question in questions)
        {
            var answersFromQuestion = await GetAllFromQuestion(question);
            
            answers.AddRange(answersFromQuestion);
        }

        return answers;
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

    public async Task<DbSet<Answer>> Get()
    {
        return Answers.Get();
    }
    
    public async Task DeleteAllFromQuestion(Question question)
    {
        var answers = Answers.Get().Where(x => x.QuestionId == question.Id).ToList();

        foreach (var answer in answers)
        {
            await Delete(answer);
        }
    }
}