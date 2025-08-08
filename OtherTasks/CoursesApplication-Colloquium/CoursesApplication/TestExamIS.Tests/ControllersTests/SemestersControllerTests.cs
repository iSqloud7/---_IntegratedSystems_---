using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using CoursesApplication.Domain.DomainModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using TestExamIS.Tests.Utils;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace TestExamIS.Tests.ControllersTests;

[Collection("Test Suite")]
public class SemestersControllerTests : LoggedTestBase, IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public SemestersControllerTests(WebApplicationFactory<Program> factory, GlobalTestFixture fixture) : base(fixture)
    {
        _factory = factory.WithTestDatabase();

        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            HandleCookies = true,
            AllowAutoRedirect = false
        });
    }

    [LoggedFact(Category = "SemestersController", Points = 5)]
    public async Task Index_ShouldReturnAllSemesters()
    {
        await RunTestAsync(async () =>
        {
            var response = await _client.GetAsync("/Semesters");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("Semesters", content);
        });
    }

    [LoggedFact(Category = "SemestersController", Points = 5)]
    public async Task Create_ValidSemester_ShouldRedirectToIndex()
    {
        await RunTestAsync(async () =>
        {
            var initialCount = await TestDatabaseHelper.GetCount<Semester>(_factory.Services);

            var getResponse = await _client.GetAsync("/Semesters/Create");
            var antiForgeryToken = await getResponse.GetAntiForgeryTokenAsync();

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Id", Guid.NewGuid().ToString()),
                new KeyValuePair<string, string>("Name", "Spring 2026"),
                new KeyValuePair<string, string>("__RequestVerificationToken", antiForgeryToken)
            });

            var response = await _client.PostAsync("/Semesters/Create", formContent);

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Semesters", response.Headers.Location?.ToString());

            var newCount = await TestDatabaseHelper.GetCount<Semester>(_factory.Services);
            Assert.Equal(initialCount + 1, newCount);
        });
    }

    [LoggedFact(Category = "SemestersController", Points = 5)]
    public async Task Create_InvalidSemester_ShouldReturnView()
    {
        await RunTestAsync(async () =>
        {
            var initialCount = await TestDatabaseHelper.GetCount<Semester>(_factory.Services);

            var getResponse = await _client.GetAsync("/Semesters/Create");
            var antiForgeryToken = await getResponse.GetAntiForgeryTokenAsync();

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Id", Guid.NewGuid().ToString()),
                new KeyValuePair<string, string>("Name", ""),
                new KeyValuePair<string, string>("__RequestVerificationToken", antiForgeryToken)
            });

            var response = await _client.PostAsync("/Semesters/Create", formContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var newCount = await TestDatabaseHelper.GetCount<Semester>(_factory.Services);
            Assert.Equal(initialCount, newCount);
        });
    }

    [LoggedFact(Category = "SemestersController", Points = 5)]
    public async Task Details_ValidId_ShouldReturnSemester()
    {
        await RunTestAsync(async () =>
        {
            var semester = await TestDatabaseHelper.GetFirst<Semester>(_factory.Services);

            var response = await _client.GetAsync($"/Semesters/Details/{semester.Id}");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains(semester.Name, content);
        });
    }

    [LoggedFact(Category = "SemestersController", Points = 5)]
    public async Task Details_InvalidId_ShouldReturnNotFound()
    {
        await RunTestAsync(async () =>
        {
            var response = await _client.GetAsync($"/Semesters/Details/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        });
    }

    [LoggedFact(Category = "SemestersController", Points = 5)]
    public async Task Edit_ValidSemester_ShouldRedirectToIndex()
    {
        await RunTestAsync(async () =>
        {
            var initialCount = await TestDatabaseHelper.GetCount<Semester>(_factory.Services);
            var toEdit = await TestDatabaseHelper.GetFirst<Semester>(_factory.Services);
            var editedName = toEdit.Name + " Updated";

            var getResponse = await _client.GetAsync($"/Semesters/Edit/{toEdit.Id}");
            var antiForgeryToken = await getResponse.GetAntiForgeryTokenAsync();

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Id", toEdit.Id.ToString()),
                new KeyValuePair<string, string>("Name", editedName),
                new KeyValuePair<string, string>("__RequestVerificationToken", antiForgeryToken)
            });

            var response = await _client.PostAsync($"/Semesters/Edit/{toEdit.Id}", formContent);

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Semesters", response.Headers.Location?.ToString());

            var edited = TestDatabaseHelper.GetById<Semester>(_factory.Services, x => x.Id == toEdit.Id);
            Assert.NotNull(edited);
            Assert.Equal(editedName, edited.Name);

            var newCount = await TestDatabaseHelper.GetCount<Semester>(_factory.Services);
            Assert.Equal(initialCount, newCount);
        });
    }

    [LoggedFact(Category = "SemestersController", Points = 5)]
    public async Task Edit_InvalidSemesterName_ShouldReturnView()
    {
        await RunTestAsync(async () =>
        {
            var initialCount = await TestDatabaseHelper.GetCount<Semester>(_factory.Services);
            var toEdit = await TestDatabaseHelper.GetFirst<Semester>(_factory.Services);

            var getResponse = await _client.GetAsync($"/Semesters/Edit/{toEdit.Id}");
            var antiForgeryToken = await getResponse.GetAntiForgeryTokenAsync();

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Id", toEdit.Id.ToString()),
                new KeyValuePair<string, string>("Name", ""),
                new KeyValuePair<string, string>("__RequestVerificationToken", antiForgeryToken)
            });

            var response = await _client.PostAsync($"/Semesters/Edit/{toEdit.Id}", formContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var unchanged = TestDatabaseHelper.GetById<Semester>(_factory.Services, x => x.Id == toEdit.Id);
            Assert.NotNull(unchanged);
            Assert.Equal(toEdit.Name, unchanged.Name);

            var newCount = await TestDatabaseHelper.GetCount<Semester>(_factory.Services);
            Assert.Equal(initialCount, newCount);
        });
    }

    [LoggedFact(Category = "SemestersController", Points = 5)]
    public async Task Edit_MismatchedId_ShouldReturnNotFound()
    {
        await RunTestAsync(async () =>
        {
            var toEdit = await TestDatabaseHelper.GetFirst<Semester>(_factory.Services);
            var wrongId = Guid.NewGuid();

            var getResponse = await _client.GetAsync($"/Semesters/Edit/{toEdit.Id}");
            var antiForgeryToken = await getResponse.GetAntiForgeryTokenAsync();

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Id", wrongId.ToString()),
                new KeyValuePair<string, string>("Name", toEdit.Name),
                new KeyValuePair<string, string>("__RequestVerificationToken", antiForgeryToken)
            });

            var response = await _client.PostAsync($"/Semesters/Edit/{toEdit.Id}", formContent);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        });
    }

    [LoggedFact(Category = "SemestersController", Points = 5)]
    public async Task Delete_ValidSemester_ShouldRedirectToIndex()
    {
        await RunTestAsync(async () =>
        {
            var initialCount = await TestDatabaseHelper.GetCount<Semester>(_factory.Services);
            var toDelete = await TestDatabaseHelper.GetFirst<Semester>(_factory.Services);

            var getResponse = await _client.GetAsync($"/Semesters/Delete/{toDelete.Id}");
            var antiForgeryToken = await getResponse.GetAntiForgeryTokenAsync();

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("__RequestVerificationToken", antiForgeryToken)
            });

            var response = await _client.PostAsync($"/Semesters/Delete/{toDelete.Id}", formContent);

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Semesters", response.Headers.Location?.ToString());

            var deleted = TestDatabaseHelper.GetById<Semester>(_factory.Services, x => x.Id == toDelete.Id);
            Assert.Null(deleted);

            var newCount = await TestDatabaseHelper.GetCount<Semester>(_factory.Services);
            Assert.Equal(initialCount - 1, newCount);
        });
    }

    [LoggedFact(Category = "SemestersController", Points = 5)]
    public async Task Delete_InvalidId_ShouldReturnNotFound()
    {
        await RunTestAsync(async () =>
        {
            var response = await _client.GetAsync($"/Semesters/Delete/{Guid.NewGuid()}");

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