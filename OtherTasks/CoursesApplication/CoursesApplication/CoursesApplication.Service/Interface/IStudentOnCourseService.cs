using CoursesApplication.Domain.DomainModels;

namespace CoursesApplication.Service.Interface;

public interface IStudentOnCourseService
{
    StudentOnCourse EnrollOnCourse(string studentId, Guid courseId, Guid semesterId, bool reEnrolled);
    List<StudentOnCourse> GetAll();
    List<StudentOnCourse> GetAllByStudentId(string passengerId);
    StudentOnCourse? GetById(Guid id);
    StudentOnCourse Insert(StudentOnCourse studentOnCourse);
    StudentOnCourse Update(StudentOnCourse studentOnCourse);
    StudentOnCourse DeleteById(Guid id);
}