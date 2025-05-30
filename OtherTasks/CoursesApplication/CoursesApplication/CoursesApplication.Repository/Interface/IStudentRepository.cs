using CoursesApplication.Domain.DomainModels;

namespace CoursesApplication.Repository.Interface;

public interface IStudentRepository
{
    public Student? GetById(string id);
}