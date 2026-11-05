using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework03.Configurations
{
    internal class EnrollmentConfigurations : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {

            builder.HasKey(e => new { e.StudentId, e.CourseId });
            builder.Property(e => e.FinalGrade).HasPrecision(5, 2);


            builder.Property(e => e.ProgressPercentage)
                .HasPrecision(5, 2)
                .IsRequired();

            builder.HasOne(e => e.Student)
                 .WithMany(s => s.Enrollments)
                 .HasForeignKey(e => e.StudentId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e=>e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
