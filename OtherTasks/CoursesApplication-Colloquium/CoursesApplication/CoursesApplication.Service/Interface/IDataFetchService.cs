using CoursesApplication.Domain.DomainModels;

namespace CoursesApplication.Service.Interface;

public interface IDataFetchService
{
    Task<List<Course>> FetchCoursesFromApi();
}