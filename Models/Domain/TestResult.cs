using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Models.Domain;

public class TestResult
{
    [Key]
    public Guid Id { get; set; }
    public ApplicationUser User { get; set; }
    public TestQuestionCategory TestCategory { get; set; }

    public int Score { get; set; } = 0;
    public DateTime DateCreated { get; set; } = DateTime.Now;
}