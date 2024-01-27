using Choose_Teacher.Models;
using Microsoft.EntityFrameworkCore;

namespace Choose_Teacher.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
                
        }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<TeacherAvailability> TeacherAvailabilities { get; set; }
        public DbSet<TeacherReview> TeacherReviews { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Booking>Bookings { get; set; }


    }
}
