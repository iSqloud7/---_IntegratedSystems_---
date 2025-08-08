namespace ConsoleApp1;


public abstract class Student : IStudentEnrollment 
{
    public int Id { get; }
    public string Name { get; }

    private int? _year;

    public int? Year
    {
        get
        {
            return _year ?? -1;
        }
        private set
        {
            _year = value;
        }
    }

    public List<Course> EnrolledCourses { get; }
    public List<Grade> Grades { get; }

    public Student(int id, string name, int year)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty");
        if (year < 1 || year > 4)
            throw new ArgumentException("Year must be between 1 and 4");

        Id = id;
        Name = name;
        Year = year;
        EnrolledCourses = new List<Course>();
        Grades = new List<Grade>();
    }

    public void EnrollInCourse(Course course)
    {
        if (course == null)
            throw new ArgumentNullException(nameof(course));
        
        if (!EnrolledCourses.Contains(course))
        {
            EnrolledCourses.Add(course);
            course.EnrollStudent(this);
        }
    }

    public void UnenrollFromCourse(Course course)
    {
        if (course == null)
            throw new ArgumentNullException(nameof(course));

        if (EnrolledCourses.Contains(course))
        {
            EnrolledCourses.Remove(course);
            course.UnenrollStudent(this);
        }
    }

    public List<Course> GetEnrolledCourses()
    {
        throw new NotImplementedException();
    }

    public bool IsEnrolledIn(Course course)
    {
        return EnrolledCourses.Where(courseEnrolled => courseEnrolled == course).Any();
    }

    public abstract string GetAcademicStatus();

    public double CalculateGPA()
    {
        if (!Grades.Any())
            return 0.0;
        
        return Grades.Average(x => x.NumericGrade);
    }

    public IEnumerable<Course> GetCoursesWithGradeAbove(double threshold)
    {
        return Grades
            .Where(g => g.NumericGrade > threshold)
            .Select(g => g.Course)
            .Distinct();
    }

    public override string ToString()
    {
        return $"Student {Id}: {Name} (Year {Year}) - GPA: {CalculateGPA()}";
    }
}