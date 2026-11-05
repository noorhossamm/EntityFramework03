using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework03.Configurations
{
    internal class InstructorConfigurations : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasKey(I=>I.Id);

            builder.Property(I => I.FullName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(I=>I.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(i => i.Bio)
                .HasMaxLength(500);
        }
    }
}
