using learnpoint_2._0.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace learnpoint_2._0.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<SchoolClass> SchoolClasses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<StudentGrade> Grades { get; set; }
        public DbSet<learnpoint_2._0.Models.ClassCourses> ClassCourses { get; set; } = default!;
        public DbSet<learnpoint_2._0.Models.TeacherCourses> TeacherCourses { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentGrade>()
                .HasOne(sg => sg.GradingTeacher)
                .WithMany()
                .HasForeignKey(sg => sg.FkTeacherId)
                .OnDelete(DeleteBehavior.Restrict); // Disable cascading delete
        }
    }
}
