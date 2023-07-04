using JobPortal.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Models;

public class JobApplicationIndexViewModel
{
    public IEnumerable<JobOpenings> Jobs { get; set; } = new List<JobOpenings>();
}
