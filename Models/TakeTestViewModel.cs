using JobPortal.Models.Domain;

namespace JobPortal.Models;

public class TakeTestViewModel
{
    public TestQuestionCategory Category { get; set; }
    public IEnumerable<TestQuestion> Questions { get; set; }
}