using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using CoursesApplication.Domain.DomainModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using TestExamIS.Tests.Utils;
using CoursesApplication.Web;

namespace TestExamIS.Tests.ControllersTests;

[Collection("Test Suite")]
public class StudentOnCourseControllerTests : LoggedTestBase, IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _userClient;
    private readonly HttpClient _anonymousClient;

    public StudentOnCourseControllerTests(WebApplicationFactory<Program> factory, GlobalTestFixture fixture) : base(fixture)
    {
        _factory = factory
            .WithTestDatabase()
            .WithTestAuth();

        _userClient = _factory.CreateAuthenticatedClient("user");
        _anonymousClient = _factory.CreateAnonymousClient();
    }

    [LoggedFact(Category = "StudentOnCourse", Points = 5)]
    public async Task Index_Authenticated_ShouldReturnUserEnrollments()
    {
        await RunTestAsync(async () =>
        {
            var response = await _userClient.GetAsync("/StudentOnCourse");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("Course", content);
            Assert.Contains("Semester", content);
        });
    }

    [LoggedFact(Category = "StudentOnCourse", Points = 5)]
    public async Task Index_Unauthenticated_ShouldReturnUnauthorized()
    {
        await RunTestAsync(async () =>
        {
            var response = await _anonymousClient.GetAsync("/StudentOnCourse");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        });
    }

    [LoggedFact(Category = "StudentOnCourse", Points = 5)]
    public async Task EnrollOnCourse_Authenticated_ShouldDisplayForm()
    {
        await RunTestAsync(async () =>
        {
            var response = await _userClient.GetAsync("/StudentOnCourse/EnrollOnCourse");
            
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("input", content);
            Assert.Contains("CourseId", content);
            Assert.Contains("SemesterId", content);
        });
    }

    [LoggedFact(Category = "StudentOnCourse", Points = 5)]
    public async Task EnrollOnCourse_Unauthenticated_ShouldReturnUnauthorized()
    {
        await RunTestAsync(async () =>
        {
            var response = await _anonymousClient.GetAsync("/StudentOnCourse/EnrollOnCourse");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        });
    }

    [LoggedFact(Category = "StudentOnCourse", Points = 5)]
    public async Task SubmitCourseEnrollment_Authenticated_ShouldSucceed()
    {
        await RunTestAsync(async () =>
        {
            var initialCount = await TestDatabaseHelper.GetCount<StudentOnCourse>(_factory.Services);

            
            var course = await TestDatabaseHelper.GetFirst<Course>(_factory.Services);
            var semester = await TestDatabaseHelper.GetFirst<Semester>(_factory.Services);
            
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("CourseId", course.Id.ToString()),
                new KeyValuePair<string, string>("SemesterId", semester.Id.ToString()),
                new KeyValuePair<string, string>("ReEnrolled", "false"),
                new KeyValuePair<string, string>("__RequestVerificationToken", "dummy-token")
            });

            var response = await _userClient.PostAsync("/StudentOnCourse/SubmitCourseEnrollment", formContent);
            
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/StudentOnCourse", response.Headers.Location?.ToString());
            
            var newCount = await TestDatabaseHelper.GetCount<StudentOnCourse>(_factory.Services);
            Assert.Equal(initialCount + 1, newCount);
        });
    }

    [LoggedFact(Category = "StudentOnCourse", Points = 5)]
    public async Task SubmitCourseEnrollment_Unauthenticated_ShouldReturnUnauthorized()
    {
        await RunTestAsync(async () =>
        {
            var course = await TestDatabaseHelper.GetFirst<Course>(_factory.Services);
            var semester = await TestDatabaseHelper.GetFirst<Semester>(_factory.Services);
            
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("CourseId", course.Id.ToString()),
                new KeyValuePair<string, string>("SemesterId", semester.Id.ToString()),
                new KeyValuePair<string, string>("ReEnrolled", "false"),
                new KeyValuePair<string, string>("__RequestVerificationToken", "dummy-token")
            });

            var response = await _anonymousClient.PostAsync("/StudentOnCourse/SubmitCourseEnrollment", formContent);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        });
    }

    private async Task<StudentOnCourse> CreateTestEnrollment()
    {
        var course = await TestDatabaseHelper.GetFirst<Course>(_factory.Services);
        var semester = await TestDatabaseHelper.GetFirst<Semester>(_factory.Services);
        
        return await TestDatabaseHelper.CreateEntity<StudentOnCourse>(_factory.Services,
            new StudentOnCourse
            {
                Id = Guid.NewGuid(),
                StudentId = TestAuthHandler.TestUsers.UserId,
                CourseId = course.Id,
                SemesterId = semester.Id,
                ReEnrollment = false
            });
    }

    public async Task InitializeAsync()
    {
        await TestDatabaseHelper.ResetDatabaseAsync(_factory.Services);
    }

    public async Task DisposeAsync()
    {
        await TestDatabaseHelper.ResetDatabaseAsync(_factory.Services);
    }
}