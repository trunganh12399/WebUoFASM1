using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace WebUoFASM1.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Detail> Details { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Trainee> Trainees { get; set; }

        public DbSet<Trainer> Trainers { get; set; }

        public DbSet<RoleViewModel> RoleViewModels { get; set; }
        public DbSet<AssignTrainerToTopic> AssignTrainerToTopics { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
    }
}