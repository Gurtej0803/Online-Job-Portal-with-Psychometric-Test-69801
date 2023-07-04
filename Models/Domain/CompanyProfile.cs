using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Models.Domain
{
    public class CompanyProfile
    {
        [System.ComponentModel.DataAnnotations.Key]

        public Guid? Id { get; set;}
        public string? CompanyName { get; set;}  
        public string? CompanyEmail { get; set;}
        public string? CompanyContact { get; set;}
        public string? CompanyLocation { get; set;}
        public string? CompanyType { get; set;}
        public string? CompanyIndustry { get; set;}

        [ForeignKey("User")]
        public string? UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
