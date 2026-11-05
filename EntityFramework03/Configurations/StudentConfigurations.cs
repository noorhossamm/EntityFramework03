using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework03.Configurations
{
    internal class StudentConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(s => s.DateOfBirth)
                .IsRequired();
        }
    }
}
