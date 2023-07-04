using MessagePack;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Models.Domain
{
    public class TestQuestionCategory
    {
        [System.ComponentModel.DataAnnotations.Key]
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual IEnumerable<TestQuestion> Questions { get; set; } = new List<TestQuestion>();
    }
}
