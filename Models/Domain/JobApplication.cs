using System.ComponentModel.DataAnnotations;

namespace JobPortal.Models.Domain;

public class JobApplication
{
    [Key]
    public Guid Id { get; set; }
    
    public JobOpenings JobOpening { get; set; }
    public ApplicationUser User { get; set; }
    
    public byte[]? Resume { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public JobApplicationStatus ApplicationStatus { get; set; } = JobApplicationStatus.Applied;
}
