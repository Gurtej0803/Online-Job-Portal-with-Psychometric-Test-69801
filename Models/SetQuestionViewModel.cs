using JobPortal.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Models
{
    public class SetQuestionViewModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public Guid QuestionID { get; set; }
        public string QuestionDescription { get; set; }
        public string QuestionName { get; set; }
        [NotMapped]
        public IFormFile? ImagePathQuestion { get; set; }
        public string OptionA { get; set; }
        [NotMapped]
        public IFormFile? ImagePathOptionA { get; set; }
        public string OptionB { get; set; }
        [NotMapped]
        public IFormFile? ImagePathOptionB { get; set; }
        public string OptionC { get; set; }
        [NotMapped]
        public IFormFile? ImagePathOptionC { get; set; }
        public string OptionD { get; set; }
        [NotMapped]
        public IFormFile? ImagePathOptionD { get; set; }
        public string CorrectOption { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("TestQuestionCategory")]
        public Guid CategoryId { get; set; }
        public TestQuestionCategory TestQuestionCategory { get; set; }
    }
}
