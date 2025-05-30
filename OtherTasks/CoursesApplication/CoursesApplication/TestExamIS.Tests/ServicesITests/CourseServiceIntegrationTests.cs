using CoursesApplication.Domain.DomainModels;
using CoursesApplication.Repository.Data;
using CoursesApplication.Repository.Interface;
using CoursesApplication.Service.Implementation;
using CoursesApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestExamIS.Tests.Utils;

namespace TestExamIS.Tests.ServicesITests;

[Collection("Test Suite")]
public class CourseServiceIntegrationTests : LoggedTestBase, IAsyncLifetime
{
    private readonly ICourseService _service;
    private readonly ApplicationDbContext _context;
    private readonly IServiceProvider _serviceProvider;

    public CourseServiceIntegrationTests(GlobalTestFixture fixture) : base(fixture)
    {
        var serviceCollection = new ServiceCollection();

        TestDatabaseHelper.SetupDI(serviceCollection);

        _serviceProvider = serviceCollection.BuildServiceProvider();
        _context = _serviceProvider.GetRequiredService<ApplicationDbContext>();
        _service = _serviceProvider.GetRequiredService<ICourseService>();
    }

    public async Task InitializeAsync()
    {
        await TestDatabaseHelper.ResetDatabaseAsync(_serviceProvider);
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [LoggedFact(Category = "CourseService", Points = 5)]
    public void Service_Should_CreateCourse()
    {
        RunTest(() =>
        {
            var countBefore = _context.Courses.Count();
            
            var course = new Course
            {
                Name = "Introduction to Programming",
                Credits = 6,
                SemesterType = "Winter"
            };

            var result = _service.Insert(course);

            Assert.NotNull(result);
            Assert.Equal(countBefore + 1, _context.Courses.Count());
            Assert.Equal("Introduction to Programming", result.Name);
            Assert.Equal(6, result.Credits);
        });
    }

    [LoggedFact(Category = "CourseService", Points = 5)]
    public void Service_Should_GetAllCourses()
    {
        RunTest(() =>
        {
            var courses = _service.GetAll();

            Assert.NotEmpty(courses);
            Assert.True(courses.Count > 0, "Should return at least one course.");
        });
    }

    [LoggedFact(Category = "CourseService", Points = 5)]
    public void Service_Should_GetCourseById()
    {
        RunTest(() =>
        {
            var knownId = _context.Courses.First().Id;

            var course = _service.GetById(knownId);

            Assert.NotNull(course);
            Assert.Equal(knownId, course?.Id);
        });
    }

    [LoggedFact(Category = "CourseService", Points = 5)]
    public void Service_Should_InsertManyCourses()
    {
        RunTest(() =>
        {
            var countBefore = _context.Courses.Count();
            
            var courses = new List<Course>
            {
                new Course { Name = "Advanced Programming", Credits = 6, SemesterType = "Winter" },
                new Course { Name = "Data Structures", Credits = 5, SemesterType = "Summer"}
            };

            var result = _service.InsertMany(courses);

            Assert.Equal(2, result.Count);
            Assert.Equal(countBefore + 2, _context.Courses.Count());
            Assert.All(result, course => Assert.IsType<Course>(course));
        });
    }

    [LoggedFact(Category = "CourseService", Points = 5)]
    public void Service_Should_UpdateCourse()
    {
        RunTest(() =>
        {
            var countBefore = _context.Courses.Count();
            
            var course = _context.Courses.First();
            course.Name = "Updated Course Name";

            var updatedCourse = _service.Update(course);

            Assert.NotNull(updatedCourse);
            Assert.Equal(countBefore, _context.Courses.Count());
            Assert.Equal("Updated Course Name", updatedCourse.Name);
        });
    }

    [LoggedFact(Category = "CourseService", Points = 5)]
    public void Service_Should_DeleteCourse()
    {
        RunTest(() =>
        {
            var countBefore = _context.Courses.Count();
            var course = _context.Courses.First();
            var courseId = course.Id;

            var deletedCourse = _service.DeleteById(courseId);

            Assert.NotNull(deletedCourse);
            Assert.Equal(courseId, deletedCourse.Id);
            Assert.Equal(countBefore - 1, _context.Courses.Count());

            var deletedCourseFromDb = _context.Courses.Find(courseId);
            Assert.Null(deletedCourseFromDb);
        });
    }
}