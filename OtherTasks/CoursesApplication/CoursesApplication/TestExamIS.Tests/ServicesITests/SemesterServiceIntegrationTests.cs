using CoursesApplication.Domain.DomainModels;
using CoursesApplication.Repository.Data;
using CoursesApplication.Service.Interface;
using Microsoft.Extensions.DependencyInjection;
using TestExamIS.Tests.Utils;

namespace TestExamIS.Tests.ServicesITests;

[Collection("Test Suite")]
public class SemesterServiceIntegrationTests : LoggedTestBase, IAsyncLifetime
{
    private readonly ISemesterService _service;
    private readonly ApplicationDbContext _context;
    private readonly IServiceProvider _serviceProvider;

    public SemesterServiceIntegrationTests(GlobalTestFixture fixture) : base(fixture)
    {
        var serviceCollection = new ServiceCollection();

        TestDatabaseHelper.SetupDI(serviceCollection);

        _serviceProvider = serviceCollection.BuildServiceProvider();
        _context = _serviceProvider.GetRequiredService<ApplicationDbContext>();
        _service = _serviceProvider.GetRequiredService<ISemesterService>();
    }

    public async Task InitializeAsync()
    {
        await TestDatabaseHelper.ResetDatabaseAsync(_serviceProvider);
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [LoggedFact(Category = "SemesterService", Points = 5)]
    public void Service_Should_CreateSemester()
    {
        RunTest(() =>
        {
            var countBefore = _context.Semesters.Count();
            
            var semester = new Semester
            {
                Name = "Fall 2025"
            };

            var result = _service.Insert(semester);

            Assert.NotNull(result);
            Assert.IsType<Semester>(result);
            Assert.Equal("Fall 2025", result.Name);
            
            var countAfter = _context.Semesters.Count();
            Assert.Equal(countBefore + 1, countAfter);
        });
    }

    [LoggedFact(Category = "SemesterService", Points = 5)]
    public void Service_Should_GetAllSemesters()
    {
        RunTest(() =>
        {
            var semesters = _service.GetAll();

            Assert.NotEmpty(semesters);
            Assert.True(semesters.Count > 0, "Should return at least one semester.");
        });
    }

    [LoggedFact(Category = "SemesterService", Points = 5)]
    public void Service_Should_GetSemesterById()
    {
        RunTest(() =>
        {
            var knownId = _context.Semesters.First().Id;

            var semester = _service.GetById(knownId);

            Assert.NotNull(semester);
            Assert.Equal(knownId, semester?.Id);
        });
    }

    [LoggedFact(Category = "SemesterService", Points = 5)]
    public void Service_Should_InsertManySemesters()
    {
        RunTest(() =>
        {
            var countBefore = _context.Semesters.Count();
            
            var semesters = new List<Semester>
            {
                new Semester { Name = "Spring 2026" },
                new Semester { Name = "Summer 2026" }
            };

            var result = _service.InsertMany(semesters);

            Assert.Equal(2, result.Count);
            Assert.All(result, semester => Assert.IsType<Semester>(semester));
            Assert.Contains(result, s => s.Name == "Spring 2026");
            Assert.Contains(result, s => s.Name == "Summer 2026");
            
            var countAfter = _context.Semesters.Count();
            Assert.Equal(countBefore + 2, countAfter);
        });
    }

    [LoggedFact(Category = "SemesterService", Points = 5)]
    public void Service_Should_UpdateSemester()
    {
        RunTest(() =>
        {
            var countBefore = _context.Semesters.Count();
            
            var semester = _context.Semesters.First();
            var originalName = semester.Name;
            var semesterId = semester.Id;
            semester.Name = "Updated Semester Name";

            var updatedSemester = _service.Update(semester);

            Assert.NotNull(updatedSemester);
            Assert.Equal("Updated Semester Name", updatedSemester.Name);
            Assert.NotEqual(originalName, updatedSemester.Name);
            Assert.Equal(semesterId, updatedSemester.Id);
            
            var countAfter = _context.Semesters.Count();
            Assert.Equal(countBefore, countAfter);
            
            var semesterFromDb = _context.Semesters.Find(semesterId);
            Assert.Equal("Updated Semester Name", semesterFromDb?.Name);
        });
    }

    [LoggedFact(Category = "SemesterService", Points = 5)]
    public void Service_Should_DeleteSemester()
    {
        RunTest(() =>
        {
            var countBefore = _context.Semesters.Count();
            
            var semester = _context.Semesters.First();
            var semesterId = semester.Id;

            var deletedSemester = _service.DeleteById(semesterId);

            Assert.NotNull(deletedSemester);
            Assert.Equal(semesterId, deletedSemester.Id);
            
            var deletedSemesterFromDb = _context.Semesters.Find(semesterId);
            Assert.Null(deletedSemesterFromDb);
            
            var countAfter = _context.Semesters.Count();
            Assert.Equal(countBefore - 1, countAfter);
        });
    }
}