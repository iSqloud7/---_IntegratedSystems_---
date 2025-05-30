using CoursesApplication.Repository.Data;
using CoursesApplication.Service.Interface;
using Microsoft.Extensions.DependencyInjection;
using TestExamIS.Tests.Utils;

namespace TestExamIS.Tests.ServicesITests;

[Collection("Test Suite")]
public class DataFetchServiceIntegrationTests : LoggedTestBase, IAsyncLifetime
{
    private readonly IDataFetchService _service;
    private readonly ApplicationDbContext _context;
    private readonly IServiceProvider _serviceProvider;
    
    public async Task InitializeAsync()
    {
        await TestDatabaseHelper.ResetDatabaseAsync(_serviceProvider);
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public DataFetchServiceIntegrationTests(GlobalTestFixture fixture) : base(fixture)
    {
        var serviceCollection = new ServiceCollection();

        TestDatabaseHelper.SetupDI(serviceCollection);

        _serviceProvider = serviceCollection.BuildServiceProvider();
        _context = _serviceProvider.GetRequiredService<ApplicationDbContext>();
        _service = _serviceProvider.GetRequiredService<IDataFetchService>();
    }

    [LoggedFact(Category = "DataFetchService", Points = 5)]
    public async Task FetchCoursesFromApi_Should_Return_Valid_Courses()
    {
        await RunTestAsync( async () =>
        {
            var courses = await _service.FetchCoursesFromApi();

            Assert.NotNull(courses);
            Assert.NotEmpty(courses);
            Assert.All(courses, course =>
            {
                Assert.NotNull(course.Name);
                Assert.NotEqual(Guid.Empty, course.Id);
            }); 
        });
    }
}