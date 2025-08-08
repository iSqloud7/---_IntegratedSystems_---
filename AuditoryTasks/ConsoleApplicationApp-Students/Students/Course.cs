using System.Text.RegularExpressions;

namespace ConsoleApp1;


public class Course : ICourseEnrollment
{
    public string Code { get; }
    public string Name { get; }
    public int Credits { get; }
    public List<Student> EnrolledStudents { get; }
    public List<Grade> Grades { get; }

    public Course(string code, string name, int credits)
    {
        if (!Regex.IsMatch(code, @"^[A-Z]{3}\d{3}$"))
            throw new ArgumentException("Course code must follow format XXX123");
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Course name cannot be empty");
        if (credits <= 0)
            throw new ArgumentException("Credits must be positive");

        Code = code;
        Name = name;
        Credits = credits;
        EnrolledStudents = new List<Student>();
        Grades = new List<Grade>();
    }

    public void EnrollStudent(Student student)
    {
        if (student == null)
            throw new ArgumentNullException(nameof(student));

        if (!EnrolledStudents.Contains(student))
        {
            EnrolledStudents.Add(student);
            if (!student.EnrolledCourses.Contains(this))
                student.EnrollInCourse(this);
        }
    }

    public void UnenrollStudent(Student student)
    {
        if (student == null)
            throw new ArgumentNullException(nameof(student));

        if (EnrolledStudents.Contains(student))
        {
            EnrolledStudents.Remove(student);
            if (student.EnrolledCourses.Contains(this))
                student.UnenrollFromCourse(this);
        }
    }

    public double GetAverageGrade()
    {
        return Grades.Any() ? Math.Round(Grades.Average(g => g.NumericGrade), 2) : 0.0;
    }

    public Student GetTopPerformingStudent()
    {
        return Grades
            .GroupBy(g => g.Student)
            .OrderByDescending(g => g.Average(grade => grade.NumericGrade))
            .Select(g => g.Key)
            .FirstOrDefault();
    }

    public IEnumerable<Student> GetFailingStudents()
    {
        return Grades
            .Where(g => g.NumericGrade == 5)
            .Select(g => g.Student)
            .Distinct();
    }

    public override string ToString()
    {
        return $"Course {Code}: {Name} ({Credits} credits) - Average: {GetAverageGrade()}";
    }
}
