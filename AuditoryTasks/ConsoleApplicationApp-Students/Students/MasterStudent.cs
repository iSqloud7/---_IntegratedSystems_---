namespace ConsoleApp1;


public class MasterStudent : Student
{
    private string supervisor;
    private string thesisTopic;
    private bool hasCompletedThesis;
    private string researchArea;

    public MasterStudent(int id, string name, int year, string researchArea) 
        : base(id, name, year)
    {
        if (string.IsNullOrWhiteSpace(researchArea))
            throw new ArgumentException("Research area cannot be empty");
        
        this.researchArea = researchArea;
    }

    public void AssignSupervisor(string newSupervisor)
    {
        if (string.IsNullOrWhiteSpace(newSupervisor))
            throw new ArgumentException("Supervisor name cannot be empty");
        supervisor = newSupervisor;
    }

    public void SubmitThesis(string topic)
    {
        if (string.IsNullOrWhiteSpace(topic))
            throw new ArgumentException("Thesis topic cannot be empty");
        if (string.IsNullOrWhiteSpace(supervisor))
            throw new InvalidOperationException("Cannot submit thesis without a supervisor");
        
        thesisTopic = topic;
        hasCompletedThesis = true;
    }

    public bool CheckThesisStatus()
    {
        return hasCompletedThesis;
    }

    public void AddResearchPublication(string publication)
    {
        throw new NotSupportedException("Master students are not required to publish");
    }

    public override string GetAcademicStatus()
    {
        return $"Master Student - Research Area: {researchArea}, " +
               $"Thesis: {(hasCompletedThesis ? "Completed" : "In Progress")}";
    }
}
