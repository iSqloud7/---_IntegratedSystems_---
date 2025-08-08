namespace ConsoleApp1;


public class Grade 
{
    public Student Student { get; }
    public Course Course { get; }
    public double NumericGrade { get; }
    public DateTime DateAdded { get; }

    public Grade(Student student, Course course, double numericGrade)
    {
        if (student == null)
            throw new ArgumentNullException(nameof(student));
        if (course == null)
            throw new ArgumentNullException(nameof(course));
        if (numericGrade < 0 || numericGrade > 100)
            throw new ArgumentException("Grade must be between 0 and 100");

        Student = student;
        Course = course;
        NumericGrade = numericGrade;
        DateAdded = DateTime.Now;

        student.Grades.Add(this);
        course.Grades.Add(this);
    }

    public string GetLetterGrade()
    {
        return NumericGrade switch
        {
            >= 10 => "A",
            >= 9 => "B",
            >= 8 => "C",
            >= 7 => "D",
            >= 6 => "F",
            _ => "E"
        };
    }

    public override string ToString()
    {
        return $"{Student.Name} - {Course.Code}: {NumericGrade} ({GetLetterGrade()})";
    }
}