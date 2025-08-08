namespace ConsoleApp1;


public class PhDStudent : Student
{
    private List<string> publications;


    public PhDStudent(int id, string name, int year) 
        : base(id, name, year)
    {
        publications = new List<string>();
    }
    
    public override string GetAcademicStatus()
    {
        return $"PhD Student - " +
               $"Publications: {publications.Count}, ";
    }
}