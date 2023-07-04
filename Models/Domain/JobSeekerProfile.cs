using MessagePack;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Models.Domain
{
    public class JobSeekerProfile
    {
        
        public  Guid? Id { get; set; }
        public string? JobSeekerName { get; set; }
        public DateTime? JobSeekerDateofBirth { get; set; }
        public string? JobSeekerGender { get; set; }
        public string? JobSeekerContact { get; set; }
        [NotMapped]
        public IFormFile? JobSeekerResume { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public virtual ApplicationUser User { get; set; }


    }
}
