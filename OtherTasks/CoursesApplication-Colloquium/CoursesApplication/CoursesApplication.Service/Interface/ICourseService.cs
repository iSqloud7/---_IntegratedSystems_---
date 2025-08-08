using CoursesApplication.Domain.DomainModels;

namespace CoursesApplication.Service.Interface;

public interface ICourseService
{
    List<Course> GetAll();
    Course? GetById(Guid id);
    Course Insert(Course course);
    ICollection<Course> InsertMany(ICollection<Course> courses);
    Course Update(Course course);
    Course DeleteById(Guid id);
}