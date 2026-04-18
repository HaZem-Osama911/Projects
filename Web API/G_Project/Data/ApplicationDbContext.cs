using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using G_Project.Models;

namespace G_Project.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<StudentTranscript> StudentTranscripts { get; set; }
    public DbSet<SystemRegulation> SystemRegulations { get; set; }
    public DbSet<AiAnalysisResult> AiAnalysisResults { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CoursePrerequisite> CoursePrerequisites { get; set; }
    public DbSet<StudentCompletedCourse> StudentCompletedCourses { get; set; }
    public DbSet<StudentCourseSelection> StudentCourseSelections { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Composite PK for the prerequisite join table
        builder.Entity<CoursePrerequisite>()
            .HasKey(cp => new { cp.CourseId, cp.PrerequisiteId });

        builder.Entity<CoursePrerequisite>()
            .HasOne(cp => cp.Course)
            .WithMany(c => c.Prerequisites)
            .HasForeignKey(cp => cp.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<CoursePrerequisite>()
            .HasOne(cp => cp.Prerequisite)
            .WithMany(c => c.IsPrerequisiteFor)
            .HasForeignKey(cp => cp.PrerequisiteId)
            .OnDelete(DeleteBehavior.Restrict);

        // Prevent cascade delete cycles for student-related entities
        builder.Entity<StudentTranscript>()
            .HasOne(st => st.Student)
            .WithMany()
            .HasForeignKey(st => st.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<AiAnalysisResult>()
            .HasOne(a => a.Student)
            .WithMany()
            .HasForeignKey(a => a.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<StudentCompletedCourse>()
            .HasOne(s => s.Student)
            .WithMany()
            .HasForeignKey(s => s.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<StudentCourseSelection>()
            .HasOne(s => s.Student)
            .WithMany()
            .HasForeignKey(s => s.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        // Unique index: a student can't select same course twice
        builder.Entity<StudentCourseSelection>()
            .HasIndex(s => new { s.StudentId, s.CourseId })
            .IsUnique();

        // Unique index: a completed course is recorded once per student
        builder.Entity<StudentCompletedCourse>()
            .HasIndex(s => new { s.StudentId, s.CourseId })
            .IsUnique();
    }
}
