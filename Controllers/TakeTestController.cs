using JobPortal.Data;
using JobPortal.Models;
using JobPortal.Models.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Controllers;

[Authorize(Roles = "Job Seeker")]
public class TakeTestController : Controller
{
    private readonly ApplicationDbLayer _dbContext;

    public TakeTestController(ApplicationDbLayer dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = User.Identity.GetUserId();
        var userCompletedQuestions = await _dbContext.TestResults
            .Where(result => result.User.Id == userId)
            .OrderByDescending(result => result.DateCreated)
            .ToListAsync();

        var questions = await _dbContext.TestQuestionCategory.ToListAsync();
        var totalScore = await _dbContext.TestQuestion
            .GroupBy(q => q.CategoryId)
            .Select(group => new
            {
                Key = group.Key,
                Count = !group.Any() ? 1 : group.Count()
            })
            .ToListAsync();
        var questionsScore = questions.Select(category =>
        {
            var userScore =
                userCompletedQuestions.FirstOrDefault(result => result.TestCategory.CategoryId == category.CategoryId)?.Score;
            var questionTotalScore = totalScore.FirstOrDefault(s => s.Key == category.CategoryId)?.Count;
            double? percentage = (userScore != null && questionTotalScore != null) ? (double)userScore / (double)questionTotalScore : null;

            return (category, percentage);
        });

        return View(questionsScore);
    }

    [HttpGet]
    public async Task<IActionResult> Test(Guid id)
    {
        var test = await _dbContext.TestQuestionCategory
            .Include(category => category.Questions)
            .FirstOrDefaultAsync(category => category.CategoryId == id);

        if (test == null)
        {
            return RedirectToAction(nameof(Index));
        }
        
        return View("Test", new TakeTestViewModel
        {
            Category = test,
            Questions = test.Questions
        });
    }

    [HttpPost]
    public async Task<IActionResult> SubmitTest(TakeTestSubmitViewModel model)
    {
        var userId = User.Identity.GetUserId();
        var user = await _dbContext.Users.FindAsync(userId);
        var test = await _dbContext.TestQuestionCategory
            .Include(category => category.Questions)
            .FirstOrDefaultAsync(category => category.CategoryId == model.TestId);
        
        if (user == null || test == null || !model.Answers.Any())
        {
            return RedirectToAction("Index");
        }

        var questions = await _dbContext.TestQuestion
            .Where(question => question.TestQuestionCategory.CategoryId == test.CategoryId)
            .ToListAsync();

        // If the amount of answers given is not the same as the amount of questions, skip
        if (model.Answers.Length != questions.Count)
        {
            return RedirectToAction("Index");
        }
        
        // Calculate score
        int score = 0;
        for (int index = 0; index < questions.Count; index++)
        {
            var currentQuestion = questions[index];
            var currentAnswer = model.Answers[index];

            if (currentQuestion.CorrectOption.Equals(currentAnswer))
            {
                score++;
            }
        }

        await _dbContext.TestResults.AddAsync(new TestResult
        {
            Score = score,
            TestCategory = test,
            User = user,
        });
        await _dbContext.SaveChangesAsync();
        
        return RedirectToAction("Index");
    }
}