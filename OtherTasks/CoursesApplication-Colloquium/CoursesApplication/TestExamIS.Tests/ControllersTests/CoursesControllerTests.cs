using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using CoursesApplication.Domain.DomainModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using TestExamIS.Tests.Utils;
using CoursesApplication.Web;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace TestExamIS.Tests.ControllersTests;

[Collection("Test Suite")]
public class CoursesControllerTests : LoggedTestBase, IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public CoursesControllerTests(WebApplicationFactory<Program> factory, GlobalTestFixture fixture) : base(fixture)
    {
        _factory = factory.WithTestDatabase();

        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            HandleCookies = true,
            AllowAutoRedirect = false
        });
    }

    [LoggedFact(Category = "CoursesController", Points = 5)]
    public async Task Index_ShouldReturnAllCourses()
    {
        await RunTestAsync(async () =>
        {
            var response = await _client.GetAsync("/Courses");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("Courses", content);
        });
    }

    [LoggedFact(Category = "CoursesController", Points = 5)]
    public async Task Create_ValidCourse_ShouldRedirectToIndex()
    {
        await RunTestAsync(async () =>
        {
            var initialCount = await TestDatabaseHelper.GetCount<Course>(_factory.Services);

            var getResponse = await _client.GetAsync("/Courses/Create");
            var antiForgeryToken = await getResponse.GetAntiForgeryTokenAsync();

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Name", "Test Course"),
                new KeyValuePair<string, string>("Credits", "6"),
                new KeyValuePair<string, string>("SemesterType", "Spring"),
                new KeyValuePair<string, string>("__RequestVerificationToken", antiForgeryToken)
            });

            var response = await _client.PostAsync("/Courses/Create", formContent);

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Courses", response.Headers.Location?.ToString());

            var newCount = await TestDatabaseHelper.GetCount<Course>(_factory.Services);
            Assert.Equal(initialCount + 1, newCount);
        });
    }

    [LoggedFact(Category = "CoursesController", Points = 5)]
    public async Task Create_InvalidCourse_ShouldReturnView()
    {
        await RunTestAsync(async () =>
        {
            var initialCount = await TestDatabaseHelper.GetCount<Course>(_factory.Services);

            var getResponse = await _client.GetAsync("/Courses/Create");
            var antiForgeryToken = await getResponse.GetAntiForgeryTokenAsync();

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Name", ""),
                new KeyValuePair<string, string>("Credits", "6"),
                new KeyValuePair<string, string>("SemesterType", "Spring"),
                new KeyValuePair<string, string>("__RequestVerificationToken", antiForgeryToken)
            });

            var response = await _client.PostAsync("/Courses/Create", formContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var newCount = await TestDatabaseHelper.GetCount<Course>(_factory.Services);
            Assert.Equal(initialCount, newCount);
        });
    }

    [LoggedFact(Category = "CoursesController", Points = 5)]
    public async Task Details_ValidId_ShouldReturnCourse()
    {
        await RunTestAsync(async () =>
        {
            var course = await TestDatabaseHelper.GetFirst<Course>(_factory.Services);

            var response = await _client.GetAsync($"/Courses/Details/{course.Id}");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains(course.Name, content);
        });
    }

    [LoggedFact(Category = "CoursesController", Points = 5)]
    public async Task Details_InvalidId_ShouldReturnNotFound()
    {
        await RunTestAsync(async () =>
        {
            var response = await _client.GetAsync($"/Courses/Details/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        });
    }

    [LoggedFact(Category = "CoursesController", Points = 5)]
    public async Task Edit_ValidCourse_ShouldRedirectToIndex()
    {
        await RunTestAsync(async () =>
        {
            var initialCount = await TestDatabaseHelper.GetCount<Course>(_factory.Services);
            var toEdit = await TestDatabaseHelper.GetFirst<Course>(_factory.Services);
            var editedName = toEdit.Name + " Updated";

            var getResponse = await _client.GetAsync($"/Courses/Edit/{toEdit.Id}");
            var antiForgeryToken = await getResponse.GetAntiForgeryTokenAsync();

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Id", toEdit.Id.ToString()),
                new KeyValuePair<string, string>("Name", editedName),
                new KeyValuePair<string, string>("Credits", toEdit.Credits.ToString()),
                new KeyValuePair<string, string>("SemesterType", toEdit.SemesterType),
                new KeyValuePair<string, string>("__RequestVerificationToken", antiForgeryToken)
            });

            var response = await _client.PostAsync($"/Courses/Edit/{toEdit.Id}", formContent);

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Courses", response.Headers.Location?.ToString());

            var edited = TestDatabaseHelper.GetById<Course>(_factory.Services, x => x.Id == toEdit.Id);
            Assert.NotNull(edited);
            Assert.Equal(editedName, edited.Name);

            var newCount = await TestDatabaseHelper.GetCount<Course>(_factory.Services);
            Assert.Equal(initialCount, newCount);
        });
    }

    [LoggedFact(Category = "CoursesController", Points = 5)]
    public async Task Edit_InvalidCourseName_ShouldReturnView()
    {
        await RunTestAsync(async () =>
        {
            var initialCount = await TestDatabaseHelper.GetCount<Course>(_factory.Services);
            var toEdit = await TestDatabaseHelper.GetFirst<Course>(_factory.Services);

            var getResponse = await _client.GetAsync($"/Courses/Edit/{toEdit.Id}");
            var antiForgeryToken = await getResponse.GetAntiForgeryTokenAsync();

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Id", toEdit.Id.ToString()),
                new KeyValuePair<string, string>("Name", ""),
                new KeyValuePair<string, string>("Credits", toEdit.Credits.ToString()),
                new KeyValuePair<string, string>("SemesterType", toEdit.SemesterType),
                new KeyValuePair<string, string>("__RequestVerificationToken", antiForgeryToken)
            });

            var response = await _client.PostAsync($"/Courses/Edit/{toEdit.Id}", formContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var unchanged = TestDatabaseHelper.GetById<Course>(_factory.Services, x => x.Id == toEdit.Id);
            Assert.NotNull(unchanged);
            Assert.Equal(toEdit.Name, unchanged.Name);

            var newCount = await TestDatabaseHelper.GetCount<Course>(_factory.Services);
            Assert.Equal(initialCount, newCount);
        });
    }

    [LoggedFact(Category = "CoursesController", Points = 5)]
    public async Task Edit_MismatchedId_ShouldReturnNotFound()
    {
        await RunTestAsync(async () =>
        {
            var toEdit = await TestDatabaseHelper.GetFirst<Course>(_factory.Services);
            var wrongId = Guid.NewGuid();

            var getResponse = await _client.GetAsync($"/Courses/Edit/{toEdit.Id}");
            var antiForgeryToken = await getResponse.GetAntiForgeryTokenAsync();

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Id", wrongId.ToString()),
                new KeyValuePair<string, string>("Name", toEdit.Name),
                new KeyValuePair<string, string>("Credits", toEdit.Credits.ToString()),
                new KeyValuePair<string, string>("SemesterType", toEdit.SemesterType),
                new KeyValuePair<string, string>("__RequestVerificationToken", antiForgeryToken)
            });

            var response = await _client.PostAsync($"/Courses/Edit/{toEdit.Id}", formContent);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        });
    }

    [LoggedFact(Category = "CoursesController", Points = 5)]
    public async Task Delete_ValidCourse_ShouldRedirectToIndex()
    {
        await RunTestAsync(async () =>
        {
            var initialCount = await TestDatabaseHelper.GetCount<Course>(_factory.Services);
            var toDelete = await TestDatabaseHelper.GetFirst<Course>(_factory.Services);

            var getResponse = await _client.GetAsync($"/Courses/Delete/{toDelete.Id}");
            var antiForgeryToken = await getResponse.GetAntiForgeryTokenAsync();

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("__RequestVerificationToken", antiForgeryToken)
            });

            var response = await _client.PostAsync($"/Courses/Delete/{toDelete.Id}", formContent);

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Courses", response.Headers.Location?.ToString());

            var deleted = TestDatabaseHelper.GetById<Course>(_factory.Services, x => x.Id == toDelete.Id);
            Assert.Null(deleted);

            var newCount = await TestDatabaseHelper.GetCount<Course>(_factory.Services);
            Assert.Equal(initialCount - 1, newCount);
        });
    }

    [LoggedFact(Category = "CoursesController", Points = 5)]
    public async Task Delete_InvalidId_ShouldReturnNotFound()
    {
        await RunTestAsync(async () =>
        {
            var response = await _client.GetAsync($"/Courses/Delete/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
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