using JobPortal.Data;
using JobPortal.Models;
using JobPortal.Models.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TestQuestionsController : Controller
    {
        private readonly ApplicationDbLayer applicationDBLayer;
        

        public TestQuestionsController(ApplicationDbLayer applicationDbLayer)
        {
            this.applicationDBLayer = applicationDbLayer;

        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(
                await applicationDBLayer.TestQuestionCategory
                    .Where(x => x.UserId == User.Identity.GetUserId())
                    .ToListAsync()
            );
        }


        [HttpGet]
        public IActionResult SetCategory()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SetCategory(SetCategoryViewModel SetCategoryRequest)
        {
            var userId = User.Identity.GetUserId();
            var testQuestionCategory = new TestQuestionCategory()
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = SetCategoryRequest.CategoryName,
                UserId = userId
            };

            await applicationDBLayer.TestQuestionCategory.AddAsync(testQuestionCategory);
            await applicationDBLayer.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> ViewCategory(Guid Id)
        {
            var userId = User.Identity.GetUserId();
            var testQuestionsCategory = applicationDBLayer.TestQuestionCategory.FirstOrDefault(x => x.CategoryId == Id);
            if (testQuestionsCategory != null)
            {
                var viewCategoryModel = new UpdateCategoryViewModel()
                {
                    CategoryId = testQuestionsCategory.CategoryId,
                    CategoryName = testQuestionsCategory.CategoryName,
                    UserId = userId
                };
                return View(viewCategoryModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ViewCategory(UpdateCategoryViewModel model)
        {
            var testQuestionCategory = await applicationDBLayer.TestQuestionCategory.FindAsync(model.CategoryId);

            if (testQuestionCategory != null)
            {
                testQuestionCategory.CategoryId = model.CategoryId;
                testQuestionCategory.CategoryName = model.CategoryName;


                await applicationDBLayer.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(UpdateCategoryViewModel model)
        {
            var testQuestionCategory = await applicationDBLayer.TestQuestionCategory.FindAsync(model.CategoryId);

            if (testQuestionCategory != null)
            {
                applicationDBLayer.TestQuestionCategory.Remove(testQuestionCategory);
                await applicationDBLayer.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public Task<IActionResult> IndexQuestions()
        {
            return Task.FromResult<IActionResult>(View(applicationDBLayer
                .TestQuestion
                .Include(q => q.TestQuestionCategory)
                .Where(x => x.UserId == User.Identity.GetUserId()).ToList()));
        }

        [HttpGet]
        public async Task<IActionResult> SetQuestions()
        {
            ViewBag.TestQuestionCategory = await applicationDBLayer.TestQuestionCategory.ToListAsync();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SetQuestions(SetQuestionViewModel SetQuestionsRequest)
        {
            var userId = User.Identity.GetUserId();
            var testQuestion = new TestQuestion()
            {
                CategoryId = SetQuestionsRequest.CategoryId,
                QuestionDescription = SetQuestionsRequest.QuestionDescription,
                QuestionID = Guid.NewGuid(),
                QuestionName = SetQuestionsRequest.QuestionName,
                OptionA = SetQuestionsRequest.OptionA,
                OptionB = SetQuestionsRequest.OptionB,
                OptionC = SetQuestionsRequest.OptionC,
                OptionD = SetQuestionsRequest.OptionD,
                CorrectOption = SetQuestionsRequest.CorrectOption,
                UserId = userId
            };     
            await applicationDBLayer.TestQuestion.AddAsync(testQuestion);
            await applicationDBLayer.SaveChangesAsync();
            return RedirectToAction("IndexQuestions");
        
        }
    

        [HttpGet]
        public async Task<IActionResult> ViewQuestion(Guid Id)
        {
            var userId = User.Identity.GetUserId();
            var testQuestion = applicationDBLayer.TestQuestion.FirstOrDefault(x => x.QuestionID == Id);
            ViewBag.TestQuestionCategory = await applicationDBLayer.TestQuestionCategory.ToListAsync();
            if (testQuestion != null)
            {
                var viewQuestionModel = new UpdateQuestionViewModel()
                {
                    CategoryId = testQuestion.CategoryId,
                    QuestionDescription = testQuestion.QuestionDescription,
                    QuestionID = testQuestion.QuestionID,
                    QuestionName = testQuestion.QuestionName,
                    OptionA = testQuestion.OptionA,
                    OptionB = testQuestion.OptionB,
                    OptionC = testQuestion.OptionC,
                    OptionD = testQuestion.OptionD,
                    CorrectOption = testQuestion.CorrectOption,
                    UserId = userId
                };
                return View(viewQuestionModel);
            }
            return RedirectToAction("IndexQuestions");
        }
        
        [HttpPost]
        public async Task<IActionResult> ViewQuestion(UpdateQuestionViewModel model)
        {
            var testQuestion = await applicationDBLayer.TestQuestion.FindAsync(model.QuestionID);

            if (testQuestion != null)
            {
                testQuestion.CategoryId = model.CategoryId;
                testQuestion.QuestionDescription = model.QuestionDescription;
                testQuestion.QuestionID = model.QuestionID;
                testQuestion.QuestionName = model.QuestionName;
                testQuestion.OptionA = model.OptionA;
                testQuestion.OptionB = model.OptionB;
                testQuestion.OptionC = model.OptionC;
                testQuestion.OptionD = model.OptionD;
                testQuestion.CorrectOption = model.CorrectOption;


                await applicationDBLayer.SaveChangesAsync();

                return RedirectToAction("IndexQuestions");
            }

            return RedirectToAction("IndexQuestions");
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteQuestion(UpdateQuestionViewModel model)
        {
            var testQuestion = await applicationDBLayer.TestQuestion.FindAsync(model.QuestionID);

            if (testQuestion != null)
            {
                applicationDBLayer.TestQuestion.Remove(testQuestion);
                await applicationDBLayer.SaveChangesAsync();

                return RedirectToAction("IndexQuestions");
            }
            return RedirectToAction("IndexQuestions");
        }
    }
}
    

