using EduPlatform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.Data
{
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options
        ) : base(options)
        {
        }

        // ================= DbSets =================
        public DbSet<StudentTeacher> StudentTeachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        // ================= Model Creating =================
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ConfigureIdentityTables(builder);
            ConfigureStudentTeacherRelation(builder);
            ConfigureEnrollmentRelation(builder);
        }

        // ================= Configurations =================
        private static void ConfigureIdentityTables(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                   .ToTable("Users", "Security");

            builder.Entity<IdentityRole>()
                   .ToTable("Roles", "Security");

            builder.Entity<IdentityUserRole<string>>()
                   .ToTable("UserRoles", "Security");

            builder.Entity<IdentityUserClaim<string>>()
                   .ToTable("UserClaims", "Security");

            builder.Entity<IdentityUserLogin<string>>()
                   .ToTable("UserLogins", "Security");

            builder.Entity<IdentityUserToken<string>>()
                   .ToTable("UserTokens", "Security");

            builder.Entity<IdentityRoleClaim<string>>()
                   .ToTable("RoleClaims", "Security");
        }

        private static void ConfigureStudentTeacherRelation(ModelBuilder builder)
        {
            builder.Entity<StudentTeacher>()
                   .HasKey(st => new { st.StudentId, st.TeacherId });

            builder.Entity<StudentTeacher>()
                   .HasOne(st => st.Student)
                   .WithMany()
                   .HasForeignKey(st => st.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<StudentTeacher>()
                   .HasOne(st => st.Teacher)
                   .WithMany()
                   .HasForeignKey(st => st.TeacherId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

        private static void ConfigureEnrollmentRelation(ModelBuilder builder)
        {
            builder.Entity<Enrollment>()
                   .HasKey(e => e.Id);

            builder.Entity<Enrollment>()
                   .HasOne(e => e.Course)
                   .WithMany(c => c.Enrollments)
                   .HasForeignKey(e => e.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Enrollment>()
                   .HasOne(e => e.Student)
                   .WithMany(s => s.Enrollments)
                   .HasForeignKey(e => e.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Prevent duplicate enrollments
            builder.Entity<Enrollment>()
                   .HasIndex(e => new { e.CourseId, e.StudentId })
                   .IsUnique();
        }
    }
}