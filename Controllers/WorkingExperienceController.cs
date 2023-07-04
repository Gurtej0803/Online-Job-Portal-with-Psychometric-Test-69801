using JobPortal.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using JobPortal.Models;
using JobPortal.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobPortal.Controllers
{
    public class WorkingExperienceController : Controller
    {
        private readonly ApplicationDbLayer applicationDBLayer;
        public WorkingExperienceController(ApplicationDbLayer applicationDBLayer)
        {
            this.applicationDBLayer = applicationDBLayer;
        }

        [HttpGet]
        public Task<IActionResult> Index()
        {
            return Task.FromResult<IActionResult>(View(applicationDBLayer.WorkingExperience.Where(x => x.UserId == User.Identity.GetUserId()).ToList()));
        }

        [HttpGet]
        public IActionResult SetWorkingExperience()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetWorkingExperience(SetWorkingExperienceViewModel SetWorkingExperienceRequest)
        {
            var userId = User.Identity.GetUserId();
            var workingExperience = new WorkingExperience()
            {
                Id = Guid.NewGuid(),
                CompanyName = SetWorkingExperienceRequest.CompanyName,
                JobTitle = SetWorkingExperienceRequest.JobTitle,
                JobDescription = SetWorkingExperienceRequest.JobDescription,
                JobField = SetWorkingExperienceRequest.JobField,
                JobLocation = SetWorkingExperienceRequest.JobLocation,
                StartDate = SetWorkingExperienceRequest.StartDate,
                EndDate = SetWorkingExperienceRequest.EndDate,
                UserId = userId
            };

            await applicationDBLayer.WorkingExperience.AddAsync(workingExperience);
            await applicationDBLayer.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ViewWorkingExperience(Guid Id)
        {
            var userId = User.Identity.GetUserId();
            var workingExperience = applicationDBLayer.WorkingExperience.FirstOrDefault(x => x.Id == Id);
            if (workingExperience != null)
            {
                var viewWorkingExperienceModel = new UpdateWorkingExperienceViewModel()
                {
                    Id = workingExperience.Id,
                    CompanyName = workingExperience.CompanyName,
                    JobTitle = workingExperience.JobTitle,
                    JobDescription = workingExperience.JobDescription,
                    JobField = workingExperience.JobField,
                    JobLocation = workingExperience.JobLocation,
                    StartDate = workingExperience.StartDate,
                    EndDate = workingExperience.EndDate,
                    UserId = userId
                };
                return View(viewWorkingExperienceModel);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> ViewWorkingExperience(UpdateWorkingExperienceViewModel model)
        {
            var workingExperience = await applicationDBLayer.WorkingExperience.FindAsync(model.Id);

            if (workingExperience != null)
            {
                workingExperience.Id = model.Id;
                workingExperience.CompanyName = model.CompanyName;
                workingExperience.JobTitle = model.JobTitle;
                workingExperience.JobDescription = model.JobDescription;
                workingExperience.JobField = model.JobField;
                workingExperience.JobLocation = model.JobLocation;
                workingExperience.StartDate = model.StartDate;
                workingExperience.EndDate = model.EndDate;

                await applicationDBLayer.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteWorkingExperience(UpdateWorkingExperienceViewModel model)
        {
            var workingExperience = await applicationDBLayer.WorkingExperience.FindAsync(model.Id);

            if (workingExperience != null)
            {
                applicationDBLayer.WorkingExperience.Remove(workingExperience);
                await applicationDBLayer.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}


