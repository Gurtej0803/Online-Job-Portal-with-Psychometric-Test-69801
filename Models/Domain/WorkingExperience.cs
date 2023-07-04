using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Models.Domain
{
    public class WorkingExperience
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set;}
        public string JobDescription { get; set;}
        public string JobField { get; set;}
        public string JobLocation { get; set;}
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
