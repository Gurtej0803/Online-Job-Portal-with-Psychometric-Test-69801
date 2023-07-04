using JobPortal.Data;
using JobPortal.Models.Domain;
using JobPortal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace JobPortal.Controllers
{
    [Authorize(Roles = "Job Seeker")]
    public class QualificationController : Controller
    {
        private readonly ApplicationDbLayer applicationDBLayer;

        public QualificationController(ApplicationDbLayer applicationDbLayer)
        {
            this.applicationDBLayer = applicationDbLayer;
        }
        [HttpGet]
        public Task<IActionResult> Index()
        {
            return Task.FromResult<IActionResult>(View(applicationDBLayer.JobSeekerQualification.Where(x => x.UserId == User.Identity.GetUserId()).ToList()));
        }
        [HttpGet]
        public IActionResult SetQualification()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetQualification(SetQualificationViewModel SetQualificationRequest)
        {
            var userId = User.Identity.GetUserId();
            var qualifications = new JobSeekerQualification()
            {
                Id = Guid.NewGuid(),
                InstitutionName = SetQualificationRequest.InstitutionName,
                LevelOfEducation = SetQualificationRequest.LevelOfEducation,
                Major = SetQualificationRequest.Major,
                StartDate = SetQualificationRequest.StartDate,
                EndDate = SetQualificationRequest.EndDate,
                GPA = SetQualificationRequest.GPA,
                UserId = userId,
            };

            await applicationDBLayer.JobSeekerQualification.AddAsync(qualifications);
            await applicationDBLayer.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> ViewQualification(Guid Id)
        {
            var userId = User.Identity.GetUserId();
            var qualification = applicationDBLayer.JobSeekerQualification.FirstOrDefault(x => x.Id == Id);
            if (qualification != null)
            {
                var viewQualificationViewModel = new UpdateQualificationViewModelcs()
                {
                    Id = qualification.Id,
                    InstitutionName = qualification.InstitutionName,
                    LevelOfEducation = qualification.LevelOfEducation,
                    Major = qualification.Major,
                    StartDate = qualification.StartDate,
                    EndDate = qualification.EndDate,
                    GPA = qualification.GPA,
                    UserId = userId,
                };
                return View(viewQualificationViewModel);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> ViewQualification(UpdateQualificationViewModelcs model)
        {
            var qualification = applicationDBLayer.JobSeekerQualification.Find(model.Id);
            if (qualification != null)
            {
                qualification.InstitutionName = model.InstitutionName;
                qualification.LevelOfEducation = model.LevelOfEducation;
                qualification.Major = model.Major;
                qualification.StartDate = model.StartDate;
                qualification.EndDate = model.EndDate;
                qualification.GPA = model.GPA;
                

                await applicationDBLayer.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteQualification(UpdateQualificationViewModelcs model)
        {
            var qualification= applicationDBLayer.JobSeekerQualification.Find(model.Id);

            if (qualification != null)
            {
                applicationDBLayer.JobSeekerQualification.Remove(qualification);
                applicationDBLayer.SaveChanges();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
