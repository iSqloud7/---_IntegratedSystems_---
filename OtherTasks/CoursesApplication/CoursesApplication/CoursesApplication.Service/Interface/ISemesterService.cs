using CoursesApplication.Domain.DomainModels;

namespace CoursesApplication.Service.Interface;

public interface ISemesterService
{
    List<Semester> GetAll();
    Semester? GetById(Guid id);
    Semester Insert(Semester semester);
    ICollection<Semester> InsertMany(ICollection<Semester> semesters);
    Semester Update(Semester semester);
    Semester DeleteById(Guid id);
}