using JobPortal.Models;
using JobPortal.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Data
{
    public class ApplicationDbLayer : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbLayer()
        {
        }

        public ApplicationDbLayer(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            
            
            // Role
            var roleAdminId = "ba7ad5c8-3ff8-4cb5-981d-9e8e44492dd3";
            var roleAdmin = new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = roleAdminId,
            };
            var roleJobRecruiter = new IdentityRole
            {
                Name = "Job Recruiter",
                NormalizedName = "JOB RECRUITER",
                Id = "646a8efc-178c-4b19-b6ac-79c7f24ae566"
            };
            var roleJobSeeker = new IdentityRole
            {
                Name = "Job Seeker",
                NormalizedName = "JOB SEEKER",
                Id = "f7874ba3-5b86-4106-b582-3f373d23a18c"
            };
            
            
            
            // User
            var userAdminId = "1d8e2037-10d0-411f-b978-a5868219f1a8";
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var userAdmin = new ApplicationUser()
            {
                Id = userAdminId,
                FirstName = "Admin",
                LastName = "",
                Email = "admin@example.com",
            };
            userAdmin.PasswordHash = passwordHasher.HashPassword(userAdmin, "P@ssword1");

            
            
            // Roles
            builder.Entity<IdentityRole>().HasData(new List<IdentityRole>()
            {
                roleAdmin,
                roleJobRecruiter,
                roleJobSeeker,
            });
            // Users
            builder.Entity<ApplicationUser>().HasData(new List<ApplicationUser>()
            {
                userAdmin,
            });
            // Relationship
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleAdminId,
                UserId = userAdminId,
            });
        }

        public DbSet<JobsEntity> Jobs { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<JobOpenings> JobOpenings { get; set; }
        public DbSet<TestQuestionCategory> TestQuestionCategory { get; set; }
        public DbSet<TestQuestion> TestQuestion { get; set; }
        public DbSet<CompanyProfile> CompanyProfile { get; set; }
        public DbSet<JobSeekerProfile> JobSeekerProfile { get; set; }
        public DbSet<JobSeekerQualification> JobSeekerQualification { get; set; }
        public DbSet<WorkingExperience> WorkingExperience { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
    }
}
