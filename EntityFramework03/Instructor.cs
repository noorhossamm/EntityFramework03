using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework03
{
    internal class Instructor
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string? Bio { get; set; }

        public DateTime JoinedDate { get; set; }

        public ICollection<Courses> PublishedCourses { get; set; } = new List<Courses>();
    }
}
