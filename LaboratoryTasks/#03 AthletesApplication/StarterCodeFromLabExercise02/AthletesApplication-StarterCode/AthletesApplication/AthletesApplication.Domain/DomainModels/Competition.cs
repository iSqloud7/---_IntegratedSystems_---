namespace AthletesApplication.Domain.DomainModels
{
    public class Competition : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public ICollection<Participation>? Participations { get; set; }
    }
}
