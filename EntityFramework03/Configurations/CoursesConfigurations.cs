using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace EntityFramework03.Configurations
{
    internal class CoursesConfigurations : IEntityTypeConfiguration<Courses>
    {
        public void Configure(EntityTypeBuilder <Courses> builder)
        {
           builder.HasKey(c => c.Id);
           builder.Property(c=> c.Title).IsRequired().HasMaxLength(200);
           builder.Property(c => c.Price).IsRequired().HasPrecision(10, 2);

            builder.Property(c => c.Level).IsRequired();

            builder.Property(c => c.IsPublished)
                .IsRequired();

            builder.Property(c => c.PublishedDate)
                .IsRequired(false);

            builder.HasOne(c => c.Instructor)
                    .WithMany(i => i.PublishedCourses)
                    .HasForeignKey(c => c.InstructorId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Category)
                .WithMany(c => c.Courses)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
