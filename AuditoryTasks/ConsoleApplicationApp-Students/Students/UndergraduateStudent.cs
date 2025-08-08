namespace ConsoleApp1;


public class UndergraduateStudent : Student
{
    private string major;
    private bool isHonorsStudent;

    public UndergraduateStudent(int id, string name, int year, string major) 
        : base(id, name, year)
    {
        if (string.IsNullOrWhiteSpace(major))
            throw new ArgumentException("Major cannot be empty");
        
        this.major = major;
        UpdateHonorsStatus();
    }

    private void UpdateHonorsStatus()
    {
        isHonorsStudent = CalculateGPA() >= 9.5 && Year > 1;
    }

    public override string GetAcademicStatus()
    {
        return $"Undergraduate Student - Major: {major}" +
               (isHonorsStudent ? " (Honors)" : "");
    }
}