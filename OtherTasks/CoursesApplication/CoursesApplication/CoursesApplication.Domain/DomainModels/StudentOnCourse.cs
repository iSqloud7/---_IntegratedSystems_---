namespace CoursesApplication.Domain.DomainModels;

public class StudentOnCourse : BaseEntity
{
    public bool ReEnrollment { get; set; }
    public Semester Semester { get; set; }
    public Guid SemesterId { get; set; }
    public Course Course { get; set; }
    public Guid CourseId { get; set; }
    public Student Student { get; set; }
    public string StudentId { get; set; }
}