using JobPortal.Models.Domain;

namespace JobPortal.Models;

public class JobApplicationApplyViewModel
{
    public Guid JobId { get; set; }

    public IEnumerable<(TestQuestionCategory, double?)> Questions { get; set; } =
        new List<(TestQuestionCategory, double?)>();
}