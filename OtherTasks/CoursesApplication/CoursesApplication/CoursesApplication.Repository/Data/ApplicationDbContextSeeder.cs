using CoursesApplication.Domain.DomainModels;
using Microsoft.AspNetCore.Identity;

namespace CoursesApplication.Repository.Data;

public class ApplicationDbContextSeeder
{
    public static async Task SeedDatabase(ApplicationDbContext context, UserManager<Student> userManager,
        IPasswordHasher<Student> passwordHasher)
    {
        var random = new Random();
        string[] semesterTypes = new[] { "Fall", "Spring", "Summer", "Winter" };

        var semesters = Enumerable.Range(1, 3).Select(x => new Semester()
        {
            Name = $"Semester{x}",
        }).ToList();
        
        context.Semesters.AddRange(semesters);
        context.SaveChanges();
    
        var courses = Enumerable.Range(1, 10).Select(x => {
            var courseId = Guid.NewGuid();
            var course = new Course {
                Id = courseId,
                Name = $"Course{x:D3}",
                Credits = random.Next(1, 7),
                SemesterType = semesterTypes[random.Next(semesterTypes.Length)]
            };
    
            return course;
        }).ToList();


        context.Courses.AddRange(courses);
        context.SaveChanges();
        
        
        var student = new Student()
        {
            Id = "test-user-id",
            UserName = "teststudent@example.com",
            NormalizedUserName = "TESTSTUDENT@EXAMPLE.COM",
            Email = "teststudent@example.com",
            NormalizedEmail = "TESTSTUDENT@EXAMPLE.COM",
            EmailConfirmed = true,
            Address = "Street 1",
            FullName = "Test Student",
            PhoneNumber = "+1234567890",
            BirthDate = new DateOnly(1990, 1, 1),
            SecurityStamp = Guid.NewGuid().ToString()
        };

        student.PasswordHash = passwordHasher.HashPassword(student, "Test123!");

        context.Students.Add(student);
        context.SaveChanges();
        
        foreach (var course in courses)
        {
            for (int i = 0; i < 3; i++)
            {
                var randomSemester = context.Semesters.First();
                var reEnroll = random.Next(0, 2) == 0;

                var studentOnCourse = new StudentOnCourse()
                {
                    Id = Guid.NewGuid(),
                    ReEnrollment = reEnroll,
                    Semester = randomSemester,
                    SemesterId = randomSemester.Id,
                    Student = student,
                    Course = course,
                    CourseId = course.Id,
                };
                

                context.StudentOnCourses.Add(studentOnCourse);
            }
        }
        
        
        context.SaveChanges();
    }
}