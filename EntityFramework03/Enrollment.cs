using EntityFramework03.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EntityFramework03
{

    internal class Enrollment
    {
        public DateTime EnrollmentDate { get; set; }

        public decimal ProgressPercentage { get; set; }
        public EnrollmentStatus Status { get; set; } 

        public decimal? FinalGrade { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int CourseId { get; set; }

        public Courses Course { get; set; }
    }
}
