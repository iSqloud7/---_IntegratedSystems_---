namespace Athletes.Domain.Models.Domain
{
    public class Competition
    {
        public Guid ID { get; set; }

        public string? Name { get; set; }

        public DateOnly StartDate { get; set; }

        public virtual ICollection<Participation>? Participations { get; set; }
    }
}
