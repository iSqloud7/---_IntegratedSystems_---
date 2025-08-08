namespace Athletes.Domain.Models.Domain
{
    public class Team
    {
        public Guid ID { get; set; }

        public string? Name { get; set; }

        public DateOnly FoundedYear { get; set; }

        public virtual ICollection<Athlete>? Athletes { get; set; }
    }
}
