using EntityFramework03.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework03
{
    internal class LearningDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=LearningDb;Trusted_Connection=true;TrustServerCertificate=true;");

        }

       public  DbSet<Instructor> Instructors { get; set; }
       public DbSet<Courses> Courses { get; set; }
       public DbSet<Enrollment> Enrollments { get; set; }
       public DbSet<Categories> Categories { get; set; }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new InstructorConfigurations());
            modelBuilder.ApplyConfiguration(new CoursesConfigurations());
            modelBuilder.ApplyConfiguration(new CategoriesConfigurations());
            modelBuilder.ApplyConfiguration(new EnrollmentConfigurations());
            modelBuilder.ApplyConfiguration(new StudentConfigurations());
        }

        

       
    }
}
