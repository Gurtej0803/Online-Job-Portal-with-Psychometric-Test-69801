using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Models
{
    public class UpdateQualificationViewModelcs
    {
        public Guid Id { get; set; }
        public string InstitutionName { get; set; }
        public string LevelOfEducation { get; set; }
        public string Major { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double? GPA { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
