namespace AthletesApplication.Domain.DomainModels
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }
        public int FoundedYear { get; set; }
        public virtual ICollection<Athlete>? Athletes { get; set; }
    }
}
