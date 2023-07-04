namespace JobPortal.Models;

public class TakeTestSubmitViewModel
{
    public Guid TestId { get; set; }
    public string[] Answers { get; set; } = Array.Empty<string>();
}