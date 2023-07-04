using JobPortal.Models.Domain;

namespace JobPortal.Models;

public class ViewApplicationTestResultViewModel
{
    public Guid JobId { get; set; }
    public ApplicationUser User { get; set; }
    public IEnumerable<(TestQuestionCategory, double?)> TestResults { get; set; } = new List<(TestQuestionCategory, double?)>();
}
