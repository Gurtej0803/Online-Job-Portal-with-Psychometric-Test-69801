using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace JobPortal.Models.Domain
{
    public class TestQuestion
    {
        [System.ComponentModel.DataAnnotations.Key]
        public Guid QuestionID { get; set; }
        public string QuestionDescription { get; set; }
        public string QuestionName { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectOption { get; set; }
        
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("TestQuestionCategory")]
        public Guid CategoryId { get; set; }
        public TestQuestionCategory TestQuestionCategory { get; set; }
    }
}