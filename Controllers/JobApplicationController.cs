using JobPortal.Data;
using JobPortal.Models;
using JobPortal.Models.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Controllers;

[Authorize(Roles = "Job Seeker")]
public class JobApplicationController : Controller
{
    private readonly ApplicationDbLayer _dbContext;

    public JobApplicationController(ApplicationDbLayer dbContext)
    {
        this._dbContext = dbContext;
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var jobs = await _dbContext.JobOpenings.ToListAsync();
        return View(new JobApplicationIndexViewModel
        {
            Jobs = jobs
        });
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var job = await _dbContext
            .JobOpenings
            .Include(jo => jo.User)
            .FirstOrDefaultAsync(jo => jo.JobId == id);
        if (job == null)
        {
            return RedirectToAction("Index");
        }

        return View(job);
    }

    [HttpGet]
    public async Task<IActionResult> Apply(Guid id)
    {
        var userId = User.Identity.GetUserId();
        var job = await _dbContext.JobOpenings.FindAsync(id);
        if (userId == null || job == null)
        {
            return RedirectToAction("Index");
        }

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

        return View("Apply", new JobApplicationApplyViewModel
        {
            JobId = id,
            Questions = questionsScore,
        });
    }

    [HttpPost]
    public async Task<IActionResult> ApplyJob(Guid id, [FromForm] IFormFile? resume)
    {
        var userId = User.Identity.GetUserId();
        var user = await _dbContext.Users.FindAsync(userId);
        var job = await _dbContext.JobOpenings.FindAsync(id);
        if (userId == null || user == null || job == null)
        {
            return RedirectToAction("Index");
        }
        
        // Check if user applied for this job before
        var previouslyApplied = await _dbContext.JobApplications
            .AnyAsync(application => application.User.Id == userId && application.JobOpening.JobId == id &&
                                     application.ApplicationStatus == JobApplicationStatus.Applied);
        if (previouslyApplied)
        {
            TempData["StatusMessage"] = "Error: You have applied to this job before.";
            return RedirectToAction("Index");
        }
        
        // Check if user completed all of the test
        var questions = await _dbContext.TestQuestionCategory.ToListAsync();
        var userCompletedQuestions = await _dbContext.TestResults
            .Where(result => result.User.Id == userId)
            .OrderByDescending(result => result.DateCreated)
            .Select(result => result.TestCategory.CategoryId)
            .ToListAsync();
        var userCompletedAllTests = questions.All(question => userCompletedQuestions.Contains(question.CategoryId));

        if (!userCompletedAllTests)
        {
            return RedirectToAction("Apply", new
            {
                id = id
            });
        }

        using var resumeBytes = new MemoryStream();
        if (resume != null)
        {
            await resume.CopyToAsync(resumeBytes);
        }

        var jobApplication = new JobApplication
        {
            User = user,
            JobOpening = job,
            Resume = resume == null ? null : resumeBytes.ToArray()
        };
        await _dbContext.JobApplications.AddAsync(jobApplication);

        await _dbContext.SaveChangesAsync();

        TempData["StatusMessage"] = "Successfully applied for job.";

        return RedirectToAction("Index");
    }
}
