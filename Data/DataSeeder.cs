using learnpoint_2._0.Models;

namespace learnpoint_2._0.Data
{
    public class DataSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Teachers.Any())
            {
                // Seed Teachers
                var teachers = new List<Teacher>
                {
                    new Teacher { TeacherName = "Reidar" },
                    new Teacher { TeacherName = "Tobias" },
                    new Teacher { TeacherName = "Aldor" },
                    new Teacher { TeacherName = "Anas" }
                };
                context.Teachers.AddRange(teachers);
                context.SaveChanges();
            }

            if (!context.Courses.Any())
            {
                // Seed Courses
                var courses = new List<Course>
                {
                    new Course { CourseName = "Objektorienterad Programmering" },
                    new Course { CourseName = "Programmering av databaser och SQL" },
                    new Course { CourseName = "Projektledning och Agila Metoder" },
                    new Course { CourseName = "Webbutv. frontend" },
                    new Course { CourseName = "Webbappplikationer i C#, ASP.NET" },
                    new Course { CourseName = "Designmönster och Arkitektur" },
                    new Course { CourseName = "AI-komponenter och M.L i MS Azure" },
                    new Course { CourseName = "Devops" }
                };
                context.Courses.AddRange(courses);
                context.SaveChanges();
            }

            if (!context.SchoolClasses.Any())
            {
                // Check if there are any teachers available
                if (!context.Teachers.Any())
                {
                    throw new InvalidOperationException("No teachers available to assign as ClassSuperintendent.");
                }

                // Seed SchoolClasses with ClassSuperintendent
                var rand = new Random();
                foreach (var className in new[] { ".NET21", ".NET22", ".NET23" })
                {
                    var randomTeacher = context.Teachers.OrderBy(t => Guid.NewGuid()).FirstOrDefault(); // Select random teacher
                    if (randomTeacher != null)
                    {
                        var schoolClass = new SchoolClass
                        {
                            ClassTitle = className,
                            FkTeacherId = randomTeacher.TeacherId, // Assign random teacher as ClassSuperintendent
                            ClassSuperintendent = randomTeacher // Set the navigation property
                        };
                        context.SchoolClasses.Add(schoolClass);
                    }
                }
                context.SaveChanges();
            }

            if (!context.Students.Any())
            {
                // Seed Students
                var students = new List<Student>();
                foreach (var schoolClass in context.SchoolClasses)
                {
                    for (int i = 0; i < 15; i++)
                    {
                        students.Add(new Student { StudentName = $"Student{i + 1}", FKSchoolClassId = schoolClass.SchoolClassId });
                    }
                }
                context.Students.AddRange(students);
                context.SaveChanges();
            }

            if (!context.ClassCourses.Any())
            {
                // Connect Courses to SchoolClasses
                foreach (var schoolClass in context.SchoolClasses)
                {
                    foreach (var course in context.Courses)
                    {
                        context.ClassCourses.Add(new ClassCourses { FKSchoolClassId = schoolClass.SchoolClassId, FKCourseId = course.CourseId });
                    }
                }
                context.SaveChanges();
            }

            if (!context.TeacherCourses.Any())
            {
                // Connect Teachers to SchoolClasses
                foreach (var schoolClass in context.SchoolClasses)
                {
                    var teachers = context.Teachers.ToList();
                    for (int i = 0; i < 2; i++)
                    {
                        var randomTeacher = teachers[new Random().Next(teachers.Count)];
                        context.TeacherCourses.Add(new TeacherCourses { FKTeacherId = randomTeacher.TeacherId, FKCourseId = schoolClass.SchoolClassId });
                    }
                }
                context.SaveChanges();
            }

            //if(!context.Grades.Any())
            //{
            //    // Set random grades for most students
            //    var rand = new Random();
            //    foreach (var student in context.Students)
            //    {
            //        var studentCourses = context.ClassCourses.Where(cc => cc.FKSchoolClassId == student.FKSchoolClassId).Select(cc => cc.Course).ToList();
            //        foreach (var course in studentCourses)
            //        {
            //            if (rand.Next(10) < 8) // 80% chance of getting a grade
            //            {
            //                var randomTeacher = context.TeacherCourses
            //                    .Where(tc => tc.FKCourseId == course.CourseId)
            //                    .Select(tc => tc.Teacher)
            //                    .OrderBy(t => rand.Next())
            //                    .FirstOrDefault(); // Select random teacher associated with the course via TeacherCourses
            //                if (randomTeacher != null)
            //                {
            //                    var randomGrade = (Grade)rand.Next(Enum.GetValues(typeof(Grade)).Length);
            //                    context.Grades.Add(new StudentGrade
            //                    {
            //                        FkStudentId = student.StudentId,
            //                        FkCourseId = course.CourseId,
            //                        FkTeacherId = randomTeacher.TeacherId, // Assign random teacher's ID
            //                        Grade = randomGrade
            //                    });
            //                }
            //            }
            //        }
            //    }
            //    context.SaveChanges();
            //}
        }
    }
}
