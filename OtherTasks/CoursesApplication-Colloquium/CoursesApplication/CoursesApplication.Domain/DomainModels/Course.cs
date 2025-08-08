namespace CoursesApplication.Domain.DomainModels;

public class Course : BaseEntity
{ 
    public string Name { get; set; }
    public int Credits { get; set; }
    public string SemesterType { get; set; }
    public ICollection<StudentOnCourse>? EnrolledStudents { get; set; }
}