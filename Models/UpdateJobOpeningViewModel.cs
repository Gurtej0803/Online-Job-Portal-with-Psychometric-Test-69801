using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Models
{
    public class UpdateJobOpeningViewModel
    {
        public Guid JobId { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobRequirement { get; set; }
        public string JobOpeningLocation { get; set; }
        public string JobOpeningType { get; set; }
        public long OfferedSalary { get; set; }
        public DateTime PreferedStartingDate { get; set; }
        public string JobOpeningStatus { get; set; }
        
        public Guid? TestCategory { get; set; }
        
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
