using System.Linq.Expressions;
using CoursesApplication.Domain.DomainModels;
using CoursesApplication.Repository.Data;
using CoursesApplication.Repository.Implementation;
using CoursesApplication.Repository.Interface;
using CoursesApplication.Service.Implementation;
using CoursesApplication.Service.Interface;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TestExamIS.Tests.Utils;

public static class TestDatabaseHelper
{
    public static void SeedDatabase(ApplicationDbContext context, IPasswordHasher<Student> passwordHasher)
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
            UserName = "teststudent",
            NormalizedUserName = "TESTSTUDENT",
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

        context.Users.Add(student);
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


    public static async Task ResetDatabaseAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<Student>>();
        
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        
        SeedDatabase(context, hasher);
    }

    public static async Task<int> GetCount<T>(IServiceProvider serviceProvider) where T : class
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        return await context.Set<T>().CountAsync();
    }
    
    public static async Task<T> GetFirst<T>(IServiceProvider serviceProvider) where T : class
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        return await context.Set<T>().FirstAsync();
    }
    public static async Task<List<T>> GetAllWhere<T>(IServiceProvider services, Expression<Func<T, bool>> predicate) where T : class
    {
        using var scope = services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        return await dbContext.Set<T>().Where(predicate).ToListAsync();
    }
    
    public static T? GetById<T>(IServiceProvider serviceProvider, Func<T, bool> predicate) where T : class
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        return context.Set<T>().Where(predicate).FirstOrDefault();
    }
    
    public static async Task<T> CreateEntity<T>(IServiceProvider services, T entity) where T : class
    {
        using var scope = services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Set<T>().AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public static void SetupDI(ServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("TestDatabase"));
        
        serviceCollection.AddIdentity<Student, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        serviceCollection.AddHttpClient();
        serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        serviceCollection.AddScoped<ICourseService, CourseService>();
        serviceCollection.AddScoped<IDataFetchService, DataFetchService>();
        serviceCollection.AddScoped<ISemesterService, SemesterService>();
        serviceCollection.AddScoped<IStudentRepository, StudentRepository>();
        serviceCollection.AddScoped<IStudentOnCourseService, StudentOnCourseService>();
    }
}