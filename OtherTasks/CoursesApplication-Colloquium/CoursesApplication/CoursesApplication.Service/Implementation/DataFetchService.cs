using CoursesApplication.Domain.DomainModels;
using CoursesApplication.Domain.DTO;
using CoursesApplication.Service.Interface;
using System.Net.Http.Json;

namespace CoursesApplication.Service.Implementation
{
    public class DataFetchService : IDataFetchService
    {
        private readonly HttpClient _httpClient;
        private readonly ICourseService _courseService;

        public DataFetchService(IHttpClientFactory httpClientFactory, ICourseService courseService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _courseService = courseService;
        }

        public async Task<List<Course>> FetchCoursesFromApi()
        {
            var url = "https://localhost:7245/api/courses";

            var response = await _httpClient.GetFromJsonAsync<List<FetchCoursesDTO>>(url);

            if (response == null)
            {
                throw new Exception("Failed to fetch courses from API.");
            }

            // From DTOs To Courses

            var courses = response.Select(dto => new Course
            {
                Id = Guid.NewGuid(),
                Name = dto.Title,
                Credits = dto.ECTS,
                SemesterType = dto.SemesterType,
            }).ToList();

            return _courseService.InsertMany(courses).ToList();
        }
    }
}
