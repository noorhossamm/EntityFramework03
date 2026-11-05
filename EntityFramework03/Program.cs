using EntityFramework03.Enums;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using LearningDbContext Dbcontext = new LearningDbContext();


            Instructor FirstInstructor = new Instructor
            {
                FullName = "Ahmed Mohamed",
                Email = "ahmed@gmail.com",
                JoinedDate = DateTime.Now
            };

           Dbcontext.Instructors.Add(FirstInstructor);
           Dbcontext.SaveChanges();


            int instructorId = FirstInstructor.Id;
            int categoryId = 1;

            if (Dbcontext.Instructors.Any(i => i.Id == instructorId) &&
                 Dbcontext.Categories.Any(c => c.Id == categoryId))
            {
                Courses course1 = new Courses
                {
                    Title = "C# Fundamentals",
                    Description = "C# Course",
                    Price = 500,
                    Level = CourseLevel.Beginner,
                    IsPublished = false,
                    InstructorId = instructorId,
                    CategoryId = categoryId
                };

                Dbcontext.Courses.Add(course1);
                Dbcontext.SaveChanges();

                Console.WriteLine($"Course ID: {course1.Id}");
            }
            else
            {
                Console.WriteLine("Instructor or Category not found.");
            }


            Student s1 = new Student
            {
                FullName = "Sara Ali",
                Email = "sara@gmail.com",
                DateOfBirth = new DateTime(2005, 3, 15)
            };

            Student s2 = new Student
            {
                FullName = "Omar Ahmed",
                Email = "omar@gmail.com",
                DateOfBirth = new DateTime(2004, 7, 20)
            };

            Student s3 = new Student
            {
                FullName = "Mona Hassan",
                Email = "mona@gmail.com",
                DateOfBirth = new DateTime(2005, 1, 10)
            };

            Dbcontext.Students.AddRange(s1, s2, s3);
            Dbcontext.SaveChanges();


            int courseId = 1;
            int studentId = s1.Id;

            var student = Dbcontext.Students.Find(studentId);
            var course = Dbcontext.Courses.Find(courseId);

            if (student == null)
            {
                Console.WriteLine("Student not found.");
            }
            else if (course == null)
            {
                Console.WriteLine("Course not found.");
            }
            else if (!course.IsPublished)
            {
                Console.WriteLine("Course is not published.");
            }
            else if (Dbcontext.Enrollments.Any(e =>
                e.StudentId == studentId && e.CourseId == courseId))
            {
                Console.WriteLine("Student already enrolled.");
            }
            else
            {
                Enrollment enrollment = new Enrollment
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    EnrollmentDate = DateTime.Now,
                    ProgressPercentage = 0,
                    Status = EnrollmentStatus.Active,
                    FinalGrade = null
                };

                Dbcontext.Enrollments.Add(enrollment);
                Dbcontext.SaveChanges();

            }


            var foundCourse = Dbcontext.Courses.Find(courseId);

            if (foundCourse == null)
                Console.WriteLine("Course not found.");
            else
                Console.WriteLine(foundCourse.Title);


 

            var courseByTitle = Dbcontext.Courses.FirstOrDefault(
                c => c.Title == "C# Fundamentals");

            if (courseByTitle == null)
                Console.WriteLine("Course not found.");
            else
                Console.WriteLine(courseByTitle.Title);


            var filteredCourses = Dbcontext.Courses.Where(c => c.Price > 200 && c.Price < 600)
                            .OrderByDescending(c => c.Price)
                            .Select(c => new { c.Title, c.Price, c.Level });


            foreach (var c in filteredCourses)
            {
                Console.WriteLine($"{c.Title} - {c.Level} - {c.Price}");
            }


            int parentId = 1;

            var subCategories = Dbcontext.Categories
                                         .Where(c => c.ParentCategoryId == parentId);

            foreach (var c in subCategories)
                Console.WriteLine(c.Name);


            var topCategories = Dbcontext.Categories
                       .Where(c => c.ParentCategoryId == null);

            foreach (var c in topCategories)
                Console.WriteLine(c.Name);


            var studentIds = Dbcontext.Enrollments.Where(e => e.CourseId == courseId)
                .Select(e => e.StudentId)
                .ToList();

            Console.WriteLine($"Number of students: {studentIds.Count}");

            var students = Dbcontext.Students
                .Where(s => studentIds.Contains(s.Id));

            foreach (var s in students)
                Console.WriteLine(s.FullName);


            var courseToUpdate = Dbcontext.Courses.Find(courseId);

             courseToUpdate.Price = 600;
             Dbcontext.SaveChanges();

            var courseToPublish = Dbcontext.Courses.Find(courseId);
            courseToPublish.IsPublished = true;
            courseToPublish.PublishedDate = DateTime.Now;

            Dbcontext.SaveChanges();


            var enrollmentToDelete = Dbcontext.Enrollments.Find(studentId, courseId);

            if (enrollmentToDelete != null)
            {
                Dbcontext.Enrollments.Remove(enrollmentToDelete);
                Dbcontext.SaveChanges();

                Console.WriteLine("Student unenrolled.");
            }
            else
            {
                Console.WriteLine("Enrollment not found.");
            }




            var categoryToDelete = Dbcontext.Categories.Find(parentId);

            if (categoryToDelete != null)
            {
                Dbcontext.Categories.Remove(categoryToDelete);

                try
                {
                    Dbcontext.SaveChanges();
                    Console.WriteLine("Category deleted.");
                }
                catch (DbUpdateException)
                {
                    Console.WriteLine("Cannot delete category");
                }
            }
            else
            {
                Console.WriteLine("Category not found.");
            }




            var safeCategory = Dbcontext.Categories.Find(parentId);

            if (safeCategory == null)
            {
                Console.WriteLine("Category not found.");
            }
            else if (Dbcontext.Categories.Any(c => c.ParentCategoryId == parentId))
            {
                Console.WriteLine("Cannot delete category because it has subcategories.");
            }
            else
            {
                Dbcontext.Categories.Remove(safeCategory);

                try
                {
                    Dbcontext.SaveChanges();
                    Console.WriteLine("Category deleted.");
                }
                catch (DbUpdateException)
                {
                    Console.WriteLine("Category cannot be deleted because it has related courses.");
                }
            }



            var courseToDelete = Dbcontext.Courses.Find(courseId);

            if (courseToDelete != null)
            {
                int before = Dbcontext.Enrollments.Count(e => e.CourseId == courseId);

                Console.WriteLine($"Enrollments before deletion: {before}");

                Dbcontext.Courses.Remove(courseToDelete);

                try
                {
                    Dbcontext.SaveChanges();

                    int after = Dbcontext.Enrollments.Count(e => e.CourseId == courseId);

                    Console.WriteLine($"Enrollments after deletion: {after}");
                }
                catch (DbUpdateException)
                {
                    // Restrict prevents deleting a course while enrollments still reference it.
                    Console.WriteLine("Cannot delete course because it has enrollments.");
                }
            }
            else
            {
                Console.WriteLine("Course not found.");
            }


        }
    }
}
