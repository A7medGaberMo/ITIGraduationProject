using ITIGraduationProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ITIGraduationProject.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Data: Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Ahmed Ali", Age = 22, Role = Models.Enums.UserRole.Instructor },
                new User { Id = 2, Name = "Sara Mohamed", Age = 23, Role = Models.Enums.UserRole.Instructor },
                new User { Id = 3, Name = "Omar Hassan", Age = 20, Role = Models.Enums.UserRole.Trainee },
                new User { Id = 4, Name = "Mona Adel", Age = 19, Role = Models.Enums.UserRole.Trainee }
            );

            // Seed Data: Courses
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "C# Basics", Category = "Programming", InstructorId = 1 },
                new Course { Id = 2, Name = "ASP.NET Core", Category = "Web", InstructorId = 2 }
            );

            // Seed Data: Sessions
            modelBuilder.Entity<Session>().HasData(
                new Session { Id = 1, CourseId = 1, StartDate = DateTime.Today.AddDays(1), EndDate = DateTime.Today.AddDays(30) },
                new Session { Id = 2, CourseId = 2, StartDate = DateTime.Today.AddDays(5), EndDate = DateTime.Today.AddDays(35) }
            );

            // Seed Data: Grades
            modelBuilder.Entity<Grade>().HasData(
                new Grade { Id = 1, SessionId = 1, TraineeId = 3, Value = 85 },
                new Grade { Id = 2, SessionId = 2, TraineeId = 4, Value = 92 }
            );
        }
    }
}
