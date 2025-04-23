namespace Athletes.Domain.Models.Domain
{
    public class Participation
    {
        public Guid ID { get; set; }

        public DateOnly DateRegistered { get; set; }

        public int Result { get; set; }

        public string? Performance { get; set; }

        public Guid CompetitionID { get; set; }

        public virtual Competition? Competition { get; set; }

        public Guid AthleteID { get; set; }

        public virtual Athlete? Athlete { get; set; }
    }
}
