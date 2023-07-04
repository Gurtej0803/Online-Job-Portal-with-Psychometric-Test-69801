using JobPortal.Data;
using JobPortal.Models;
using JobPortal.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.Identity;


namespace JobPortal.Controllers
{

    [Authorize(Roles = "Job Recruiter")]
    public class JobOpeningsController : Controller
    {
        private readonly ApplicationDbLayer applicationDBLayer;

        public JobOpeningsController(ApplicationDbLayer applicationDbLayer) 
        {
            this.applicationDBLayer = applicationDbLayer;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(applicationDBLayer.JobOpenings.Where(x => x.UserId == User.Identity.GetUserId()).ToList());
        }

        [HttpGet]
        public IActionResult SetJobOpening()
        {
            ViewBag.TestCategoryList = applicationDBLayer.TestQuestionCategory
                .Where(category => category.UserId == User.Identity.GetUserId())
                .Select(category => new
                {
                    ID = category.CategoryId,
                    Name = category.CategoryName,
                });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetJobOpening(SetJobOpeningViewModel SetJobOpeningRequest)
        {
            var userId = User.Identity.GetUserId();
            var jobOpenings = new JobOpenings()
            {
                JobId = Guid.NewGuid(),
                JobTitle = SetJobOpeningRequest.JobTitle,
                JobDescription = SetJobOpeningRequest.JobDescription,
                JobRequirement = SetJobOpeningRequest.JobRequirement,
                JobOpeningLocation = SetJobOpeningRequest.JobOpeningLocation,
                JobOpeningType = SetJobOpeningRequest.JobOpeningType,
                OfferedSalary = SetJobOpeningRequest.OfferedSalary,
                PreferedStartingDate = SetJobOpeningRequest.PreferedStartingDate,
                JobOpeningStatus = SetJobOpeningRequest.JobOpeningStatus,
                UserId = userId,
            };

            await applicationDBLayer.JobOpenings.AddAsync(jobOpenings);
            await applicationDBLayer.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ViewJobOpening(Guid Id)
        {
            var userId = User.Identity.GetUserId();
            var jobOpenings = await applicationDBLayer.JobOpenings
                .FirstOrDefaultAsync(x => x.JobId == Id);
            if (jobOpenings != null)
            {
                var viewJobOpeningModel = new UpdateJobOpeningViewModel()
                {
                    JobId = jobOpenings.JobId,
                    JobTitle = jobOpenings.JobTitle,
                    JobDescription = jobOpenings.JobDescription,
                    JobRequirement = jobOpenings.JobRequirement,
                    JobOpeningLocation = jobOpenings.JobOpeningLocation,
                    JobOpeningType = jobOpenings.JobOpeningType,
                    OfferedSalary = jobOpenings.OfferedSalary,
                    PreferedStartingDate = jobOpenings.PreferedStartingDate,
                    JobOpeningStatus = jobOpenings.JobOpeningStatus,
                    UserId = userId,
                };
                return View(viewJobOpeningModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ViewJobOpening(UpdateJobOpeningViewModel model)
        {
            var jobOpenings = await applicationDBLayer.JobOpenings
                .FirstOrDefaultAsync(jo => jo.JobId == model.JobId);
            if (jobOpenings != null)
            {
                jobOpenings.JobTitle = model.JobTitle;
                jobOpenings.JobDescription = model.JobDescription;
                jobOpenings.JobRequirement = model.JobRequirement;
                jobOpenings.JobOpeningLocation = model.JobOpeningLocation;
                jobOpenings.JobOpeningType = model.JobOpeningType;
                jobOpenings.OfferedSalary = model.OfferedSalary;
                jobOpenings.PreferedStartingDate = model.PreferedStartingDate;
                jobOpenings.JobOpeningStatus = model.JobOpeningStatus;
                
                await applicationDBLayer.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteJobOpening(UpdateJobOpeningViewModel model)
        {
            var jobOpenings = await applicationDBLayer.JobOpenings.FindAsync(model.JobId);

            if (jobOpenings != null)
            {
                applicationDBLayer.JobOpenings.Remove(jobOpenings);
                await applicationDBLayer.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ViewApplications(Guid id)
        {
            var job = await applicationDBLayer.JobOpenings
                .Include(job => job.JobApplications)
                    .ThenInclude(application => application.User)
                .FirstOrDefaultAsync(job => job.JobId == id);
            if (job == null)
            {
                return RedirectToAction("Index");
            }

            return View("ViewApplications", job);
        }

        [HttpGet]
        public async Task<IActionResult> ViewApplicationTestResult(Guid jobId, string applicantId)
        {
            var jobApplication = await applicationDBLayer.JobApplications
                .Include(application => application.JobOpening)
                .Include(application => application.User)
                .FirstOrDefaultAsync(application =>
                    application.User.Id == applicantId && application.JobOpening.JobId == jobId);
            if (jobApplication == null)
            {
                return RedirectToAction("Index");
            }
            
            var userCompletedQuestions = await applicationDBLayer.TestResults
                .Where(result => result.User.Id == applicantId)
                .Where(result => result.DateCreated < jobApplication.CreatedDate)
                .OrderByDescending(result => result.DateCreated)
                .ToListAsync();
            var questions = await applicationDBLayer.TestQuestionCategory.ToListAsync();
            var totalScore = await applicationDBLayer.TestQuestion
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

            return View("ViewApplicationTestResult", new ViewApplicationTestResultViewModel
            {
                JobId = jobId,
                User = jobApplication.User,
                TestResults = questionsScore
            });
        }

        [HttpGet]
        public async Task<IActionResult> ViewApplicationProfile(Guid jobId, string applicantId)
        {
            var jobApplication = await applicationDBLayer.JobApplications
                // .Include(application => application.JobOpening)
                .Include(application => application.User)
                    .ThenInclude(user => user.WorkingExperiences)
                .Include(application => application.User)
                    .ThenInclude(user => user.Qualifications)
                .FirstOrDefaultAsync(application =>
                    application.User.Id == applicantId && application.JobOpening.JobId == jobId);
            if (jobApplication == null)
            {
                return RedirectToAction("Index");
            }

            return View("ViewApplicationProfile", new ViewApplicationProfileViewModel
            {
                JobId = jobId,
                User = jobApplication.User,
            });
        }

        [HttpGet]
        public async Task<IActionResult> DownloadResume(Guid jobId, string applicantId)
        {
            var jobApplication = await applicationDBLayer.JobApplications
                // .Include(application => application.JobOpening)
                .Include(application => application.User)
                .ThenInclude(user => user.WorkingExperiences)
                .Include(application => application.User)
                .ThenInclude(user => user.Qualifications)
                .FirstOrDefaultAsync(application =>
                    application.User.Id == applicantId && application.JobOpening.JobId == jobId);
            if (jobApplication == null)
            {
                return RedirectToAction("Index");
            }

            if (jobApplication.Resume == null)
            {
                return NotFound();
            }

            return File(jobApplication.Resume, "application/pdf", $"{jobApplication.User.FirstName} {jobApplication.User.LastName} Resume.pdf");
        }
    }
}
