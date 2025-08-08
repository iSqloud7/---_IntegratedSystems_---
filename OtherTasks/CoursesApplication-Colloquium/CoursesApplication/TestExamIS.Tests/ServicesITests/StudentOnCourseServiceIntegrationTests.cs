using CoursesApplication.Domain.DomainModels;
using CoursesApplication.Repository.Data;
using CoursesApplication.Service.Interface;
using Microsoft.Extensions.DependencyInjection;
using TestExamIS.Tests.Utils;

namespace TestExamIS.Tests.ServicesITests;

[Collection("Test Suite")]
public class StudentOnCourseServiceIntegrationTests : LoggedTestBase, IAsyncLifetime
{
    private readonly IStudentOnCourseService _service;
    private readonly ApplicationDbContext _context;
    private readonly IServiceProvider _serviceProvider;

    public StudentOnCourseServiceIntegrationTests(GlobalTestFixture fixture) : base(fixture)
    {
        var serviceCollection = new ServiceCollection();

        TestDatabaseHelper.SetupDI(serviceCollection);

        _serviceProvider = serviceCollection.BuildServiceProvider();
        _context = _serviceProvider.GetRequiredService<ApplicationDbContext>();
        _service = _serviceProvider.GetRequiredService<IStudentOnCourseService>();
    }

    public async Task InitializeAsync()
    {
        await TestDatabaseHelper.ResetDatabaseAsync(_serviceProvider);
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [LoggedFact(Category = "StudentOnCourseService", Points = 5)]
    public void Service_Should_EnrollStudentOnCourse()
    {
        RunTest(() =>
        {
            var countBefore = _context.StudentOnCourses.Count();
            
            var studentId = _context.Students.FirstOrDefault()?.Id;
            
            var courseId = _context.Courses.FirstOrDefault()?.Id ?? Guid.NewGuid();
            
            var semesterId = _context.Semesters.FirstOrDefault()?.Id ?? Guid.NewGuid();
            
            var result = _service.EnrollOnCourse(studentId, courseId, semesterId, false);

            Assert.NotNull(result);
            Assert.IsType<StudentOnCourse>(result);
            Assert.Equal(studentId, result.StudentId);
            Assert.Equal(courseId, result.CourseId);
            Assert.Equal(semesterId, result.SemesterId);
            Assert.False(result.ReEnrollment);
            
            var countAfter = _context.StudentOnCourses.Count();
            Assert.Equal(countBefore + 1, countAfter);
        });
    }

    [LoggedFact(Category = "StudentOnCourseService", Points = 5)]
    public void Service_Should_GetAllStudentOnCourses()
    {
        RunTest(() =>
        {
            var studentOnCourses = _service.GetAll();

            Assert.NotEmpty(studentOnCourses);
            Assert.True(studentOnCourses.Count > 0, "Should return at least one student on course record.");
        });
    }

    [LoggedFact(Category = "StudentOnCourseService", Points = 5)]
    public void Service_Should_GetAllByStudentId()
    {
        RunTest(() =>
        {
            var studentId = _context.StudentOnCourses.FirstOrDefault()?.StudentId;
            
            if (studentId == null)
            {
                return;
            }

            var studentOnCourses = _service.GetAllByStudentId(studentId);

            Assert.NotEmpty(studentOnCourses);
            Assert.All(studentOnCourses, enrollment => Assert.Equal(studentId, enrollment.StudentId));
        });
    }

    [LoggedFact(Category = "StudentOnCourseService", Points = 5)]
    public void Service_Should_GetStudentOnCourseById()
    {
        RunTest(() =>
        {
            if (!_context.StudentOnCourses.Any())
            {
                return;
            }
            
            var knownId = _context.StudentOnCourses.First().Id;

            var studentOnCourse = _service.GetById(knownId);

            Assert.NotNull(studentOnCourse);
            Assert.Equal(knownId, studentOnCourse?.Id);
        });
    }

    [LoggedFact(Category = "StudentOnCourseService", Points = 5)]
    public void Service_Should_InsertStudentOnCourse()
    {
        RunTest(() =>
        {
            var countBefore = _context.StudentOnCourses.Count();
            
            var studentId = _context.Students.FirstOrDefault().Id;
            var courseId = _context.Courses.FirstOrDefault().Id;
            var semesterId = _context.Semesters.FirstOrDefault().Id;
            
            var studentOnCourse = new StudentOnCourse
            {
                StudentId = studentId,
                CourseId = courseId,
                SemesterId = semesterId,
                ReEnrollment = true,
            };

            var result = _service.Insert(studentOnCourse);

            Assert.NotNull(result);
            Assert.IsType<StudentOnCourse>(result);
            Assert.Equal(studentId, result.StudentId);
            Assert.Equal(courseId, result.CourseId);
            Assert.Equal(semesterId, result.SemesterId);
            Assert.True(result.ReEnrollment);
            
            var countAfter = _context.StudentOnCourses.Count();
            Assert.Equal(countBefore + 1, countAfter);
        });
    }

    [LoggedFact(Category = "StudentOnCourseService", Points = 5)]
    public void Service_Should_UpdateStudentOnCourse()
    {
        RunTest(() =>
        {
            if (!_context.StudentOnCourses.Any())
            {
                return;
            }
            
            var countBefore = _context.StudentOnCourses.Count();
            
            var studentOnCourse = _context.StudentOnCourses.First();
            var id = studentOnCourse.Id;
            
            studentOnCourse.ReEnrollment = !studentOnCourse.ReEnrollment;

            var updatedStudentOnCourse = _service.Update(studentOnCourse);

            Assert.NotNull(updatedStudentOnCourse);
            Assert.Equal(id, updatedStudentOnCourse.Id);
            
            var countAfter = _context.StudentOnCourses.Count();
            Assert.Equal(countBefore, countAfter);
        });
    }

    [LoggedFact(Category = "StudentOnCourseService", Points = 5)]
    public void Service_Should_DeleteStudentOnCourse()
    {
        RunTest(() =>
        {
            var countBefore = _context.StudentOnCourses.Count();
            
            var studentOnCourse = _context.StudentOnCourses.First();
            var id = studentOnCourse.Id;

            var deletedStudentOnCourse = _service.DeleteById(id);

            Assert.NotNull(deletedStudentOnCourse);
            Assert.Equal(id, deletedStudentOnCourse.Id);
            
            var deletedStudentOnCourseFromDb = _context.StudentOnCourses.Find(id);
            Assert.Null(deletedStudentOnCourseFromDb);
            
            var countAfter = _context.StudentOnCourses.Count();
            Assert.Equal(countBefore - 1, countAfter);
        });
    }
}