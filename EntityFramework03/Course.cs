using EntityFramework03.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using EntityFramework03.Configurations;

namespace EntityFramework03
{
    internal class Courses
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public CourseLevel Level { get; set; }

        public bool IsPublished { get; set; }

        public DateTime? PublishedDate { get; set; }

        // Foreign key
        public int InstructorId { get; set; }

        public Instructor Instructor { get; set; }

        public Categories Category { get; set; }

        public int CategoryId { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
            = new List<Enrollment>();


    }
}
