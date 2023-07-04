using JobPortal.Models.Domain;

namespace JobPortal.Models;

public class ViewApplicationProfileViewModel
{
    public Guid JobId { get; set; }
    public ApplicationUser User { get; set; }
}
