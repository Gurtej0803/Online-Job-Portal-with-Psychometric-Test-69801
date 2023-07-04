using System.Collections;
using JobPortal.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace JobPortal.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyLocation { get; set; } = string.Empty;
        public string CompanyType { get; set; } = string.Empty;
        public string CompanyIndustry { get; set; } = string.Empty;
        public DateTime JobSeekerDateofBirth { get; set; }
        public string JobSeekerGender { get; set; } = string.Empty;
        public byte[] ProfilePicture { get; set; } = Array.Empty<byte>();

        public virtual IEnumerable<WorkingExperience> WorkingExperiences { get; } = new List<WorkingExperience>();
        public virtual IEnumerable<JobSeekerQualification> Qualifications { get; } = new List<JobSeekerQualification>();
    }
}
